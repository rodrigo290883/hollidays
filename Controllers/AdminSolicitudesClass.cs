﻿using System;
using System.Collections.Generic;
using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using desconectate.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Net;
using System.Net.Mail;

namespace desconectate.Controllers
{

    public class AdminSolicitudesController : Controller
    {
        private readonly IConfiguration _configuration;

        public AdminSolicitudesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            string usuario = HttpContext.Session.GetString("usuario");
            if (usuario != null)
            {
                ViewBag.tipo = HttpContext.Session.GetString("tipo");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public List<Solicitud> SolicitudesPendientes()
        {
            List<Solicitud> lst = new List<Solicitud>();

            string connString = _configuration.GetConnectionString("MyConnection");
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select s.folio, e.nombre, ts.solicitud, s.fecha_inicio,s.fecha_fin,es.descripcion,s.nombre_aprobador,e.area from solicitudes s inner join empleados e on s.idsap = e.idsap and e.estatus != 2 inner join ctipos_solicitud ts on s.tipo_solicitud = ts.id_tipo_solicitud inner join cestatus es on es.estatus = s.estatus  where s.estatus = 0 ORDER BY s.fecha_solicitud DESC", conn);

                SqlDataReader sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    lst.Add(new Solicitud { folio = sqlReader.GetInt32(0), nombre = sqlReader[1].ToString(), solicitudName = sqlReader[2].ToString(), fecha_inicio = Convert.ToDateTime(sqlReader.IsDBNull(3) ? null : sqlReader[3]), fecha_fin = Convert.ToDateTime(sqlReader.IsDBNull(4) ? null : sqlReader[4]), estatusDescripcion = sqlReader[5].ToString(), nombre_aprobador = sqlReader[6].ToString(), area = sqlReader[7].ToString() });
                }

                conn.Close();
                return lst;
            }
        }

        public IActionResult ActualizarSolicitud(int id_folio, int s_estatus, string comentarios, string con_goce)
        {
            string connString = _configuration.GetConnectionString("MyConnection");
            var aux = 0;
            string usuario = HttpContext.Session.GetString("usuario");

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT sol.dias,(SELECT ISNULL(SUM(reg.disponibles),0) FROM registros_dias reg WHERE reg.idsap = sol.idsap  AND reg.registro_padre =0 and reg.caducidad >= GETDATE()) as disponibles,tip.consume_vacaciones FROM solicitudes sol left join ctipos_solicitud tip on sol.tipo_solicitud = tip.id_tipo_solicitud  WHERE sol.folio = @folio", conn);
                cmd.Parameters.AddWithValue("@folio", id_folio);
                SqlDataReader sqlReader = cmd.ExecuteReader();

                sqlReader.Read();

                var dias_sol = sqlReader.GetInt32(0);
                var dias_disp = sqlReader.GetInt32(1);
                var consume_vacaciones = sqlReader[2].ToString();

                sqlReader.Close();

                if (consume_vacaciones == "N")
                {
                    //No consume Vacaciones, proceso para las demas tipos de solicitudes, Se genera registros_dias

                    cmd = new SqlCommand("SELECT TOP 1 sol.idsap,sol.dias as dias_solicitados,reg.registro as registro_padre,reg.disponibles,reg.caducidad,reg.periodo,sol.tipo_solicitud " +
                                        "from solicitudes sol left join empleados emp on sol.idsap = emp.idsap " +
                                        "left join registros_dias reg on sol.idsap = reg.idsap and caducidad >= GETDATE() and reg.registro_padre = 0 WHERE sol.folio = @folio ORDER BY caducidad DESC", conn);
                    cmd.Parameters.AddWithValue("@folio", id_folio);

                    SqlDataReader sqlReader2 = cmd.ExecuteReader();

                    sqlReader2.Read();

                    int idsap = sqlReader2.GetInt32(0);
                    int dias = sqlReader2.GetInt32(1);
                    int registro_padre = sqlReader2.GetInt32(2);
                    int disponibles = sqlReader2.GetInt32(3);
                    DateTime caducidad = Convert.ToDateTime(sqlReader2.IsDBNull(4) ? null : sqlReader2[4]);
                    string periodo = sqlReader2[5].ToString();
                    int id_tipo_solicitud = sqlReader2.GetInt32(6);

                    sqlReader2.Close();

                    SqlCommand cmd2 = new SqlCommand("INSERT INTO registros_dias(registro_padre,id_tipo_solicitud,dias, disponibles,fecha_creacion,caducidad,idsap,periodo,folio_solicitud) VALUES " +
                                        "(@registro_padre,@id_tipo_solicitud,@dias,@disponibles,GETDATE(),@caducidad,@idsap,@periodo,@folio)", conn);
                    cmd2.Parameters.AddWithValue("@registro_padre", registro_padre);
                    cmd2.Parameters.AddWithValue("@id_tipo_solicitud", id_tipo_solicitud);
                    cmd2.Parameters.AddWithValue("@dias", dias);
                    cmd2.Parameters.AddWithValue("@disponibles", disponibles);
                    cmd2.Parameters.AddWithValue("@caducidad", caducidad);
                    cmd2.Parameters.AddWithValue("@idsap", idsap);
                    cmd2.Parameters.AddWithValue("@periodo", periodo);
                    cmd2.Parameters.AddWithValue("@folio", id_folio);

                    try
                    {
                        aux = cmd2.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        return Content(ex.ToString());
                    }


                    if (aux == 1)
                    {
                        //Se actualiza el registro de solicitud
                        cmd = new SqlCommand("update solicitudes SET estatus = @status, observacion_aprobador = @coments, con_goce = @con_goce, fecha_aprobacion = GETDATE(),ultima_notificacion=GETDATE(),idsap_aprobo = @usuario where folio = @folio", conn);
                        cmd.Parameters.AddWithValue("@folio", id_folio);
                        cmd.Parameters.AddWithValue("@status", s_estatus);
                        cmd.Parameters.AddWithValue("@coments", comentarios);
                        cmd.Parameters.AddWithValue("@con_goce", con_goce);
                        cmd.Parameters.AddWithValue("@usuario", usuario);

                        aux = cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        conn.Close();
                        return Content("Error en el proceso de Registro de Dias");
                    }

                    conn.Close();
                    return Content(Convert.ToString(aux));
                }
                else
                {
                    // Solicitudes que consumen saldo de vacaciones, Se generara registros_dias y se actualiza saldo de dias disponibles del periodo
                    if (dias_sol <= dias_disp)
                    {
                        //Dispone con los dias suficientes para procesar la solicitud

                        if (s_estatus == 2)
                        {
                            //Se rechazo la solicitud
                            //Se actualiza el registro de solicitud
                            cmd = new SqlCommand("update solicitudes SET estatus = @status, observacion_aprobador = @coments, con_goce = @con_goce, fecha_aprobacion = GETDATE(),ultima_notificacion=GETDATE(), idsap_aprobo = @usuario where folio = @folio", conn);
                            cmd.Parameters.AddWithValue("@folio", id_folio);
                            cmd.Parameters.AddWithValue("@status", s_estatus);
                            cmd.Parameters.AddWithValue("@coments", comentarios);
                            cmd.Parameters.AddWithValue("@con_goce", con_goce);
                            cmd.Parameters.AddWithValue("@usuario", usuario);

                            aux = cmd.ExecuteNonQuery();

                        }
                        else
                        {
                            //Se aprobo la solicitud

                            //Se obtienen los datos para registros_registros
                            cmd = new SqlCommand("SELECT sol.idsap,sol.dias as dias_solicitados,sol.tipo_solicitud FROM solicitudes sol WHERE sol.folio = @folio", conn);
                            cmd.Parameters.AddWithValue("@folio", id_folio);

                            SqlDataReader sqlReader2 = cmd.ExecuteReader();

                            sqlReader2.Read();

                            int idsap = sqlReader2.GetInt32(0);
                            int dias = sqlReader2.GetInt32(1);
                            int id_tipo_solicitud = sqlReader2.GetInt32(2);

                            sqlReader2.Close();

                            //Se inicializan las variables para la creacion de registros_dias
                            var restantes = dias; //Variable para el control de dias restantes por asignar
                            var diasUpdate = 0; //Variable que guarda los dias a actualizar como saldo del registro padre
                            var diasRegistrar = 0; //Variable para guardar los dias que se insertan en registro_dias

                            //Se obtienen los registros padre con dias disponibles
                            SqlCommand cmd3 = new SqlCommand("SELECT registro,dias ,disponibles,caducidad,periodo FROM registros_dias WHERE idsap = @idsap and caducidad >= GETDATE() and registro_padre = 0 AND disponibles > 0 ORDER BY caducidad ASC", conn);
                            cmd3.Parameters.AddWithValue("@idsap", idsap);

                            SqlDataReader sqlReader3 = cmd3.ExecuteReader();

                            List<RegistroDias> registros = new List<RegistroDias>();

                            //Se genera un Listado de Registros Padre

                            while (sqlReader3.Read())
                            {
                                registros.Add(new RegistroDias { registro = sqlReader3.GetInt32(0), disponibles = sqlReader3.GetInt32(2), caducidad = Convert.ToDateTime(sqlReader3.IsDBNull(3) ? null : sqlReader3[3]), periodo = sqlReader3[4].ToString() });
                            }

                            sqlReader3.Close();

                            //Se recorren los registros para hacer el sonsumo de dias necesarios por cada registro Padre
                            foreach (RegistroDias registro in registros)
                            {
                                if (restantes == 0) break;

                                var disponibles = registro.disponibles;

                                if (disponibles > 0)
                                {
                                    int reg = registro.registro;
                                    if (restantes >= disponibles)
                                    {
                                        diasRegistrar = disponibles;
                                        restantes = restantes - disponibles;
                                        diasUpdate = 0;
                                    }
                                    else
                                    {
                                        diasRegistrar = restantes;
                                        diasUpdate = disponibles - restantes;
                                        restantes = 0;
                                    }

                                    SqlCommand cmd4 = new SqlCommand("INSERT INTO registros_dias(registro_padre,id_tipo_solicitud,dias, disponibles,fecha_creacion,caducidad,idsap,periodo,folio_solicitud) VALUES " +
                                        "(@registro_padre,@id_tipo_solicitud,@dias,@disponibles,GETDATE(),@caducidad,@idsap,@periodo,@folio)", conn);
                                    cmd4.Parameters.AddWithValue("@registro_padre", reg);
                                    cmd4.Parameters.AddWithValue("@id_tipo_solicitud", id_tipo_solicitud);
                                    cmd4.Parameters.AddWithValue("@dias", diasRegistrar);
                                    cmd4.Parameters.AddWithValue("@disponibles", diasUpdate);
                                    cmd4.Parameters.AddWithValue("@caducidad", registro.caducidad);
                                    cmd4.Parameters.AddWithValue("@idsap", idsap);
                                    cmd4.Parameters.AddWithValue("@periodo", registro.periodo);
                                    cmd4.Parameters.AddWithValue("@folio", id_folio);

                                    aux = cmd4.ExecuteNonQuery();

                                    if (aux == 1)
                                    {

                                        cmd = new SqlCommand("UPDATE registros_dias SET disponibles = @disponibles WHERE registro = @registro_padre", conn);
                                        cmd.Parameters.AddWithValue("@registro_padre", reg);
                                        cmd.Parameters.AddWithValue("@disponibles", diasUpdate);

                                        aux = cmd.ExecuteNonQuery();
                                    }
                                }
                            }

                            if (aux == 1) //Se logro Insertar Registros
                            {
                                //Se actualiza la solicitud a aprobada
                                cmd = new SqlCommand("update solicitudes SET estatus = @status, observacion_aprobador = @coments, con_goce = @con_goce, fecha_aprobacion = GETDATE(),ultima_notificacion=GETDATE(),idsap_aprobo = @usuario where folio = @folio", conn);
                                cmd.Parameters.AddWithValue("@folio", id_folio);
                                cmd.Parameters.AddWithValue("@status", s_estatus);
                                cmd.Parameters.AddWithValue("@coments", comentarios);
                                cmd.Parameters.AddWithValue("@con_goce", con_goce);
                                cmd.Parameters.AddWithValue("@usuario", usuario);

                                aux = cmd.ExecuteNonQuery();

                            }
                            else
                            {
                                conn.Close();
                                return Content("Error en el proceso de Registro de Dias");
                            }
                        }

                        conn.Close();

                        return Content(Convert.ToString(aux));
                    }
                    else
                    {
                        return Content("El empleado no cuenta con los suficientes dias para realizar la aprobacion.");
                    }
                }

            }
        }

        public class RegistroDias
        {
            public int registro { get; set; }
            public string periodo { get; set; }
            public int disponibles { get; set; }
            public DateTime caducidad { get; set; }
        };

        public IActionResult EnviaCorreo(int folio)
        {
            string connString = _configuration.GetConnectionString("MyConnection"); // Read the connection string from the web.config file
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select sol.idsap, emp.nombre as solicitante,sol.idsap_aprobador,sol.nombre_aprobador as aprobador, emp.email as email_solicitante,tip.solicitud ,sol.fecha_inicio ," +
                    "sol.fecha_fin ,sol.fecha_solicitud,sol.observacion_solicitante,sol.observacion_aprobador,est.descripcion from solicitudes sol left join empleados ap on sol.idsap_aprobador = ap.idsap left join empleados emp on sol.idsap = emp.idsap" +
                    " left join ctipos_solicitud tip on sol.tipo_solicitud = tip.id_tipo_solicitud left join cestatus est on sol.estatus = est.estatus where sol.folio = @folio ", conn);

                cmd.Parameters.AddWithValue("@folio", folio);

                SqlDataReader sqlReader = cmd.ExecuteReader();

                sqlReader.Read();

                string empleado = sqlReader[1].ToString();
                string id_empleado = sqlReader[0].ToString();
                string destino = sqlReader[4].ToString();
                string solicitud = sqlReader[5].ToString();
                string fecha_inicio = sqlReader[6].ToString();
                string fecha_fin = sqlReader[7].ToString();
                string fecha_solicitud = sqlReader[8].ToString();
                string observacion = sqlReader[9].ToString();
                string observacion_aprobador = sqlReader[10].ToString();
                string estatus = sqlReader[11].ToString();

                try
                {
                    string origen = "envio.correos.sistemas@gmail.com";

                    string pass = "244466666";
                    string asunto = "Cambio de Estatus de la Solicitud Folio:" + folio;
                    string mensage = "<head><style>img{width:100%;padding:0px;margin:0px;}tr{background-image:url('https://i.postimg.cc/FzTgvcWz/cuerpo-mail.png'); background-repeat: repeat-y;background-size:100% 100%; padding:0px; margin:0px;}td{padding:0px; margin:0px;}</style></head><table style='padding:0px;marging:0px;border:0px;border-collapse: collapse;border-spacing:0px;'><tr><td><img src='https://i.postimg.cc/1319y6Dv/encabezado-mail.png' /></td></tr><tr><td style='padding:5% 5%; color:#b41547; font-size: 18px; text-align: center;'>Se realizo un cambio de estatus de la  solicitud de vacaciones, por parte de:<br> Administrador</td></tr><tr><td style='padding:0% 5%; color: #5c2a7e; font-size: 18px; text-align: left;'>Solicitante: " + empleado + "</td></tr><tr><td style='padding:0% 5%; color: #5c2a7e; font-size: 18px; text-align: left;'>Tipo Solicitud: " + solicitud + "</td></tr><tr><td style='padding:0% 5%; color: #5c2a7e; font-size: 18px; text-align: left;'>Fecha Inicio: " + fecha_inicio + "</td></tr><tr><td style=' padding:0% 5%; color: #5c2a7e; font-size: 18px; text-align: left;'>Fecha Fin: " + fecha_fin + "</td></tr><tr><td style=' padding:0% 5%; color: #5c2a7e; font-size: 18px; text-align: left;'>Observacion Solicitante: " + observacion + "</td></tr><tr><td style=' padding:5% 5% 0% 5%; color:#b41547; font-size: 18px; text-align: center; '>Nuevo estatus: " + estatus + "</td></tr><tr><td style=' padding:0% 5%; color:#b41547; font-size: 18px; text-align: left; '> Observacion Aprobador: " + observacion_aprobador + "</td></tr><tr><td><img src='https://i.postimg.cc/hvSK9qPN/pie-mail.png' /></td></tr></table>";


                    MailMessage correo = new MailMessage(origen, destino);
                    correo.IsBodyHtml = true;
                    correo.Subject = asunto;
                    correo.Body = mensage;

                    SmtpClient cliente = new SmtpClient();
                    cliente.Host = "smtp.gmail.com";
                    cliente.EnableSsl = true;
                    cliente.Port = 587;
                    cliente.UseDefaultCredentials = false;
                    cliente.Credentials = new System.Net.NetworkCredential(origen, pass);

                    //cliente.Send(correo);
                    cliente.Dispose();
                    return Content("1");

                }
                catch (Exception ex)
                {
                    return Content(ex.ToString());
                }
            }
        }
    }
}