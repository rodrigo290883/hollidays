using System;
using System.Collections.Generic;
using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using desconectate.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Net.Mail;
using System.Net;

using System.Globalization;

namespace desconectate.Controllers
{
    public class SolicitanteController : Controller
    {
        private readonly IConfiguration _configuration;


        public SolicitanteController(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        public IActionResult Index()
        {
            string usuario = HttpContext.Session.GetString("usuario");
            ViewBag.tipo = HttpContext.Session.GetString("tipo");

            if (usuario != null && ViewBag.tipo != "A" && ViewBag.tipo != "P" && ViewBag.tipo != "R" && ViewBag.tipo != "D" && ViewBag.tipo != "E")
            {
                Empleados empleado = new Empleados();

                string connString = _configuration.GetConnectionString("MyConnection"); // Read the connection string from the web.config file
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT idsap,nombre,email,estatus,fecha_ingreso_grupo,idsap_padre,esquema," +
                        " DATEDIFF(month,fecha_ingreso_grupo,GETDATE()) as antiguedad, avatar,email_line ,nombre_line, r.semana, e.fecha_ingreso_uen " +
                        " FROM empleados e INNER JOIN croles r ON e.rol = r.rol WHERE idsap = @idsap", conn);
                     
                    cmd.Parameters.AddWithValue("@idsap", usuario);

                    SqlDataReader sqlReader = cmd.ExecuteReader();

                    sqlReader.Read();

                    empleado.idsap = sqlReader.GetInt32(0);
                    empleado.nombre = sqlReader[1].ToString();
                    empleado.email = sqlReader[2].ToString();
                    empleado.estatus = sqlReader.GetInt32(3);
                    empleado.fecha_ingreso_grupo = Convert.ToDateTime(sqlReader.IsDBNull(4) ? null : sqlReader[4]);
                    
                    empleado.idsap_padre = sqlReader.GetInt32(5);
                    empleado.esquema = sqlReader.GetInt32(6);
                    empleado.antiguedad = sqlReader.GetInt32(7);
                    empleado.avatar = sqlReader[8].ToString();
                    empleado.email_line = sqlReader[9].ToString();//
                    empleado.nombre_line = sqlReader[10].ToString();//
                    empleado.rol = sqlReader[11].ToString();
                    empleado.fecha_ingreso_uen = Convert.ToDateTime(sqlReader.IsDBNull(12) ? null : sqlReader[12]);

                    sqlReader.Close();

                    SqlCommand cmd2 = new SqlCommand("SELECT disponibles, caducidad , periodo , (dias - disponibles) as tomados," +
                        " DATEDIFF(month, (SELECT TOP 1 fecha_inicio FROM solicitudes  WHERE estatus = 1 and fecha_inicio <= GETDATE() ORDER BY fecha_inicio DESC), GETDATE()) meses_ultimo," +
                        "ISNULL((SELECT TOP 1 fecha_inicio FROM solicitudes  WHERE idsap = @idsap  and estatus = 1 and fecha_inicio <= GETDATE() ORDER BY fecha_inicio DESC),e.ultimo_desconecte) as ultimo" +
                        " FROM registros_dias r LEFT JOIN empleados e ON r.idsap = e.idsap WHERE r.idsap = @idsap  and registro_padre = 0 and caducidad >= GETDATE() ORDER BY fecha_creacion,caducidad", conn);

                    cmd2.Parameters.AddWithValue("@idsap", usuario);

                    SqlDataReader sqlReader2 = cmd2.ExecuteReader();

                    var disponibles = 0;
                    var tomados = 0;


                    while (sqlReader2.Read())
                    {
                        disponibles = disponibles + sqlReader2.GetInt32(0);
                        tomados = tomados + sqlReader2.GetInt32(3);
                    }
                    sqlReader2.Close();

                    sqlReader2 = cmd2.ExecuteReader();
                    sqlReader2.Read();

                    empleado.dias_disponibles = disponibles;
                    empleado.caducidad = Convert.ToDateTime(sqlReader2.IsDBNull(1) ? null : sqlReader2[1]);
                    ViewBag.periodo = sqlReader2[2].ToString();
                    ViewBag.dias_tomados = tomados;
                    empleado.meses_ultimo_desconecte = sqlReader2.IsDBNull(4) ? 100 : sqlReader2.GetInt32(4);
                    empleado.ultimo_desconecte = Convert.ToDateTime(sqlReader2.IsDBNull(5) ? null : sqlReader2[5]);

                    sqlReader2.Close();

                    ViewBag.Empleado = empleado;

                    List<TipoSolicitud> lst = new List<TipoSolicitud>();

                    if (empleado.esquema == 1)
                    {
                        cmd = new SqlCommand("SELECT * FROM ctipos_solicitud WHERE seleccionable = 1 and id_tipo_solicitud != 6", conn);
                    }
                    else {
                        cmd = new SqlCommand("SELECT * FROM ctipos_solicitud WHERE seleccionable = 1", conn);
                    }

                    sqlReader = cmd.ExecuteReader();

                    while (sqlReader.Read())
                    {
                        lst.Add(new TipoSolicitud{  id_tipo_solicitud = sqlReader.GetInt32(0), solicitud = sqlReader[1].ToString(), maximo_dias = sqlReader.GetInt32(2), consume_vacaciones = sqlReader[4].ToString()[0], texto = sqlReader[7].ToString() });
                    }

                    ViewBag.Opciones = lst;

                    sqlReader.Close();

                    cmd = new SqlCommand("SELECT convert(varchar,fecha) FROM cdias_festivos WHERE pais = 'MX' and esquema = @esquema ;", conn);
                    cmd.Parameters.AddWithValue("@esquema", empleado.esquema);

                    sqlReader = cmd.ExecuteReader();
                    string cadena = "'0'";

                    while (sqlReader.Read())
                    {
                        cadena = cadena + ",'" + sqlReader[0].ToString() + "'";
                    }

                    ViewBag.Feriados = cadena;

                    conn.Close();
                    return View();
                }
            }
            else if (usuario != null && ViewBag.tipo == "A")
            {
                return RedirectToAction("Index", "AdminSolicitudes");
            }
            else if (usuario != null && ViewBag.tipo == "P")
            {
                return RedirectToAction("Index", "AdminPolizas");
            }
            else if (usuario != null && ViewBag.tipo == "R")
            {
                return RedirectToAction("Index", "AdminBase");
            }
            else if (usuario != null && ViewBag.tipo == "E")
            {
                return RedirectToAction("Index", "Extraccion");
            }
            else if (usuario != null && ViewBag.tipo == "D")
            {
                return RedirectToAction("Index", "Aprobador");
            }
            else
                return RedirectToAction("Index", "Home");
            
        }

        public IActionResult CreaSolicitud(Solicitud solicitud)
        {
            string connString = _configuration.GetConnectionString("MyConnection"); // Read the connection string from the web.config file
            using (SqlConnection conn = new SqlConnection(connString))
            {
                int folio = 0;
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into dbo.solicitudes (idsap,tipo_solicitud,fecha_solicitud,fecha_inicio,fecha_fin,dias,dias_detalle,estatus,idsap_aprobador,nombre_aprobador,email_aprobador,observacion_solicitante,fecha_asignacion,ultima_notificacion) " +
                    "VALUES (@idsap,@tipo_solicitud,GETDATE(),@fecha_inicio,@fecha_fin,@dias,@dias_detalle,0,@idsap_aprobador,@nombre_aprobador,@correo_aprobador,@observaciones,GETDATE(),GETDATE()); " + "SELECT CAST(scope_identity() AS int)", conn);
                cmd.Parameters.AddWithValue("@idsap", solicitud.idsap);
                cmd.Parameters.AddWithValue("@tipo_solicitud", solicitud.tipo_solicitud);
                cmd.Parameters.AddWithValue("@fecha_inicio", solicitud.fecha_inicio);
                cmd.Parameters.AddWithValue("@fecha_fin", solicitud.fecha_fin);
                cmd.Parameters.AddWithValue("@idsap_aprobador", solicitud.idsap_aprobador);
                cmd.Parameters.AddWithValue("@nombre_aprobador", solicitud.nombre_aprobador);
                cmd.Parameters.AddWithValue("@correo_aprobador", solicitud.email_aprobador);
                cmd.Parameters.AddWithValue("@observaciones", solicitud.observacion_solicitante);
                cmd.Parameters.AddWithValue("@dias", solicitud.dias);
                cmd.Parameters.AddWithValue("@dias_detalle", solicitud.dias_detalle);

                try
                { 
                    folio = (Int32)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    return Content(Convert.ToString(ex.Message));
                }
                return Content(Convert.ToString(folio));
            }
        }

        public IActionResult EnviaCorreo(int folio)
        {
            string connString = _configuration.GetConnectionString("MyConnection"); // Read the connection string from the web.config file
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select sol.idsap, emp.nombre as solicitante,sol.idsap_aprobador,ap.nombre as aprobador, ap.email_line as email_aprobador,tip.solicitud ,sol.fecha_inicio ," +
                    "sol.fecha_fin ,sol.fecha_solicitud,sol.observacion_solicitante from solicitudes sol left join empleados ap on sol.idsap_aprobador = ap.idsap left join empleados emp on sol.idsap = emp.idsap" +
                    " left join ctipos_solicitud tip on sol.tipo_solicitud = tip.id_tipo_solicitud where sol.folio = @folio ", conn);

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

                try
                {
                    string origen = "envio.correos.sistemas@gmail.com";
                    
                    string pass = "244466666";
                    string asunto = "Solicitud pendiente de Aprobacion Folio:" + folio;
                    string mensage = "<head><style>img{width:100%;padding:0px;margin:0px;}tr{background-image:url('https://i.postimg.cc/FzTgvcWz/cuerpo-mail.png'); background-repeat: repeat-y;background-size:100% 100%; padding:0px; margin:0px;}td{padding:0px; margin:0px;}</style></head><table style='padding:0px;marging:0px;border:0px;border-collapse: collapse;border-spacing:0px;'><tr><td><img src='https://i.postimg.cc/1319y6Dv/encabezado-mail.png' /></td></tr><tr><td style='padding:5% 5%; color:#b41547; font-size: 18px; text-align: center;'>Se realizo una solicitud de vacaciones por parte de:<br>" + empleado+ "</td></tr><tr><td style='padding:0% 5%; color: #5c2a7e; font-size: 18px; text-align: left;'>Tipo Solicitud: " + solicitud+ "</td></tr><tr><td style='padding:0% 5%; color: #5c2a7e; font-size: 18px; text-align: left;'>Fecha Inicio: " + fecha_inicio+ "</td></tr><tr><td style=' padding:0% 5%; color: #5c2a7e; font-size: 18px; text-align: left;'>Fecha Fin: " + fecha_fin+ "</td></tr><tr><td style=' padding:0% 5%; color: #5c2a7e; font-size: 18px; text-align: left;'>Observacion Solicitante: " + observacion+ "</td></tr><tr><td style=' padding:5% 5%; color:#b41547; font-size: 18px; text-align: center; '>Favor de ingresar al sitio de <a href='#'>vacaciones</a> para su aprobacion.</td></tr><tr><td><img src='https://i.postimg.cc/hvSK9qPN/pie-mail.png' /></td></tr></table>";


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

        public List<Solicitud> EnlistaSolicitudes(int id_sap)
        {
            List<Solicitud> lst = new List<Solicitud>();

            string connString = _configuration.GetConnectionString("MyConnection"); // Read the connection string from the web.config file
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT sol.folio,ts.solicitud,sol.fecha_inicio,sol.fecha_fin,sol.estatus,sol.nombre_aprobador,sol.observacion_solicitante,sol.observacion_aprobador FROM solicitudes sol " +
                    "LEFT JOIN ctipos_solicitud ts ON sol.tipo_solicitud = ts.id_tipo_solicitud WHERE sol.idsap = @idsap ORDER BY sol.fecha_inicio DESC", conn);
                cmd.Parameters.AddWithValue("@idsap", id_sap);

                SqlDataReader sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    lst.Add(new Solicitud { folio = sqlReader.GetInt32(0), solicitudName = sqlReader[1].ToString(), fecha_inicio = Convert.ToDateTime(sqlReader.IsDBNull(2) ? null : sqlReader[2]), fecha_fin = Convert.ToDateTime(sqlReader.IsDBNull(3) ? null : sqlReader[3]), estatus = sqlReader.GetInt32(4), nombre_aprobador = sqlReader[5].ToString(),observacion_solicitante = sqlReader[6].ToString(),observacion_aprobador = sqlReader[7].ToString() });
                }

                conn.Close();
                return lst;
            }
        }

        public ActionResult CancelaSolicitud(int folio)
        {
            try
            {
                string connString = _configuration.GetConnectionString("MyConnection"); // Read the connection string from the web.config file
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE solicitudes SET estatus = 3 WHERE folio = @folio", conn);
                    cmd.Parameters.AddWithValue("@folio", folio);

                    cmd.ExecuteNonQuery();

                    return Content("1");
                }
            }
            catch(Exception ex)
            {
                return Content("No se pudo realizar la cancelacion:"+ex.Message);
            }
        }


        public ActionResult DescargaPoliza()
        {
        
            WebClient cliente = new WebClient();

            string idsap = HttpContext.Session.GetString("usuario");
            try
            {

                byte[] archivo = cliente.DownloadData("Polizas/"+idsap + ".pdf");
                return File(archivo, "application/pdf", "PGMM_" + idsap + ".pdf");
            }
            catch (Exception ex)
            {
                return Content("Upss!!, tu póliza básica no se encuentra disponible. Solicítala a: seguros.modelo@aon.com     \nY en una emergencia busca a tu BP de People");
            }
        
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult UpdatePassword(int id_sap, string NewPass) 
        {
            string connString = _configuration.GetConnectionString("MyConnection"); // Read the connection string from the web.config file
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd =  new SqlCommand("UPDATE empleados SET contrasena = @NewPass , estatus = 1 WHERE idsap = @idsap", conn);
                cmd.Parameters.AddWithValue("@idsap", id_sap);
                cmd.Parameters.AddWithValue("@NewPass",NewPass);

                var aux = cmd.ExecuteNonQuery();
                return Content(Convert.ToString(aux));
            }
        }

    }
}
