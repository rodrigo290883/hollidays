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


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace desconectate.Controllers
{
    public class AprobadorController : Controller
    {


        private  readonly IConfiguration _configuration;

        public AprobadorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.tipo = HttpContext.Session.GetString("tipo");
            string usuario = HttpContext.Session.GetString("usuario");
            if (usuario != null)
            {
                Empleados empleado = new Empleados();

                string connString = _configuration.GetConnectionString("MyConnection");
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT idsap,nombre FROM empleados WHERE idsap = @idsap",conn);
                    cmd.Parameters.AddWithValue("@idsap", usuario);

                    SqlDataReader sqlReader = cmd.ExecuteReader();

                    sqlReader.Read();

                    empleado.idsap = sqlReader.GetInt32(0);
                    empleado.nombre = sqlReader[1].ToString();
                    
                    ViewBag.Empleado = empleado;

                    sqlReader.Close();          

                    conn.Close();
                    return View();
                }
            }
            else
                return RedirectToAction("Index", "Home");
            
        }

        public List<Solicitud> SolicitudesPendientes(int id_sap)
        {
            List<Solicitud> lst = new List<Solicitud>();

            string connString = _configuration.GetConnectionString("MyConnection");
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select s.folio, e.nombre, ts.solicitud, s.fecha_inicio,s.fecha_fin,es.descripcion from solicitudes s inner join empleados e on s.idsap = e.idsap inner join ctipos_solicitud ts on s.tipo_solicitud = ts.id_tipo_solicitud inner join cestatus es on es.estatus = s.estatus  where s.estatus = 0 and idsap_aprobador = @idsap",conn);
                cmd.Parameters.AddWithValue("@idsap",id_sap);

                SqlDataReader sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    lst.Add(new Solicitud { folio = sqlReader.GetInt32(0) , nombre = sqlReader[1].ToString(), solicitudName = sqlReader[2].ToString(), fecha_inicio = Convert.ToDateTime(sqlReader.IsDBNull(3) ? null : sqlReader[3]), fecha_fin = Convert.ToDateTime(sqlReader.IsDBNull(4) ? null : sqlReader[4]), estatusDescripcion = sqlReader[5].ToString()});
                }

                conn.Close();
                return lst;
            }
        }

        public List<Empleados> infoEquipo()
        {
            List<Empleados> lst = new List<Empleados>();

            string connString = _configuration.GetConnectionString("MyConnection");
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string usuario = HttpContext.Session.GetString("usuario");
                SqlCommand cmd = new SqlCommand("select idsap,nombre,area,banda,fecha_ingreso_grupo,dias_disponibles,ultimo_desconecte from empleados where idsap_padre = @idsap", conn);
                cmd.Parameters.AddWithValue("@idsap", usuario);

                SqlDataReader sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    lst.Add(new Empleados { idsap = sqlReader.GetInt32(0), nombre = sqlReader[1].ToString(), area = sqlReader[2].ToString(), banda = sqlReader[3].ToString(), fecha_ingreso_grupo = Convert.ToDateTime(sqlReader.IsDBNull(4) ? null : sqlReader[4]), dias_disponibles = sqlReader.GetInt32(5), ultimo_desconecte = Convert.ToDateTime(sqlReader.IsDBNull(6) ? null : sqlReader[6]) });
                }

                conn.Close();
                return lst;
            }
        }

        public Solicitud solicitudInfo(int id_folio){

            Solicitud infosolicitud = new Solicitud();
            
            string connString = _configuration.GetConnectionString("MyConnection");
            using (SqlConnection conn = new SqlConnection(connString))
            {
                
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT s.idsap,e.nombre,DATEDIFF(month,e.fecha_ingreso_grupo,GETDATE()) as antiguedad ,e.dias_disponibles, cts.maximo_dias, cts.solicitud as solicitudName, e.fecha_ingreso_grupo as aniversario, s.fecha_inicio, s.fecha_fin, s.folio,s.tipo_solicitud,cts.con_goce as tipo_solicitud_goce,s.observacion_solicitante  FROM solicitudes s inner join empleados e on s.idsap = e.idsap inner join ctipos_solicitud cts on s.tipo_solicitud = cts.id_tipo_solicitud where s.folio = @folio ",conn);
                cmd.Parameters.AddWithValue("@folio",id_folio);

                SqlDataReader sqlReader = cmd.ExecuteReader();

                sqlReader.Read();

                infosolicitud.idsap = sqlReader.GetInt32(0);
                infosolicitud.nombre = sqlReader[1].ToString();
                infosolicitud.antiguedad = sqlReader.GetInt32(2);
                infosolicitud.dias_disponibles = sqlReader.GetInt32(3);
                infosolicitud.maximo_dias = sqlReader.GetInt32(4);
                infosolicitud.solicitudName = sqlReader[5].ToString();
                infosolicitud.aniversario = Convert.ToDateTime(sqlReader.IsDBNull(6) ? null : sqlReader[6]);
                infosolicitud.fecha_inicio = Convert.ToDateTime(sqlReader.IsDBNull(7) ? null : sqlReader[7]);
                infosolicitud.fecha_fin = Convert.ToDateTime(sqlReader.IsDBNull(8) ? null : sqlReader[8]);
                infosolicitud.folio = sqlReader.GetInt32(9);
                infosolicitud.tipo_solicitud = sqlReader.GetInt32(10); 
                infosolicitud.tipo_solicitud_goce = sqlReader.GetInt32(11);
                infosolicitud.observacion_solicitante = sqlReader[12].ToString(); 

                conn.Close();
                return infosolicitud;
            }
        }
        
        public IActionResult ActualizarSolicitud(int id_folio, int s_estatus, string comentarios, string con_goce)
        {
            string connString = _configuration.GetConnectionString("MyConnection");

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("update solicitudes SET estatus = @status, observacion_aprobador = @coments, con_goce = @con_goce, fecha_aprobacion = GETDATE() where folio = @folios", conn);
                cmd.Parameters.AddWithValue("@folios", id_folio);
                cmd.Parameters.AddWithValue("@status", s_estatus);
                cmd.Parameters.AddWithValue("@coments",comentarios);
                cmd.Parameters.AddWithValue("@con_goce", con_goce);

                var aux = cmd.ExecuteNonQuery();

                conn.Close();

                if (aux == 1 && s_estatus == 1)
                {
                    conn.Open();
                    SqlCommand cmd2 = new SqlCommand("select e.idsap,e.dias_disponibles,DATEDIFF(day,sol.fecha_inicio,sol.fecha_fin) as dias,tsol.consume_vacaciones from empleados e,solicitudes sol left join ctipos_solicitud tsol on sol.tipo_solicitud = tsol.id_tipo_solicitud where e.idsap in (select s.idsap from solicitudes s where s.folio = @folio) and sol.folio = @folio", conn);
                    cmd2.Parameters.AddWithValue("@folio", id_folio);

                    SqlDataReader sqlReader = cmd2.ExecuteReader();

                    sqlReader.Read();

                    int idsap = sqlReader.GetInt32(0);
                    int dias_disponibles = sqlReader.GetInt32(1);
                    int dias = sqlReader.GetInt32(2);
                    dias = dias_disponibles - (dias + 1);

                    string consume_vacaciones = sqlReader[3].ToString();

                    conn.Close();

                    if (consume_vacaciones == "S")
                    {
                        conn.Open();
                        SqlCommand cmd3 = new SqlCommand("update empleados SET dias_disponibles = @dias where idsap = @idsap", conn);
                        cmd3.Parameters.AddWithValue("@idsap", idsap);
                        cmd3.Parameters.AddWithValue("@dias", dias);


                        aux = cmd3.ExecuteNonQuery();

                        conn.Close();
                    }
                }

                
                return Content(Convert.ToString(aux));
            }
        }

        public IActionResult EnviaCorreo(int folio)
        {
            string connString = _configuration.GetConnectionString("MyConnection"); // Read the connection string from the web.config file
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select sol.idsap, emp.nombre as solicitante,sol.idsap_aprobador,ap.nombre as aprobador, emp.email as email_solicitante,tip.solicitud ,sol.fecha_inicio ," +
                    "sol.fecha_fin ,sol.fecha_solicitud,sol.observacion_solicitante,sol.observacion_aprobador,est.descripcion from solicitudes sol left join empleados ap on sol.idsap_aprobador = ap.idsap left join empleados emp on sol.idsap = emp.idsap" +
                    " left join ctipos_solicitud tip on sol.tipo_solicitud = tip.id_tipo_solicitud left join cestatus est on sol.estatus = est.estatus where sol.folio = @folio ", conn);

                cmd.Parameters.AddWithValue("@folio", folio);

                SqlDataReader sqlReader = cmd.ExecuteReader();

                sqlReader.Read();

                string empleado = sqlReader[1].ToString();
                string id_empleado = sqlReader[0].ToString();
                string aprobador = sqlReader[3].ToString();
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
                    string origen = "rodrigo290883@gmail.com";

                    string pass = "010607SA";
                    string asunto = "Cambio de Estatus de la Solicitud Folio:" + folio;
                    string mensage = "<head><style>img{width:100%;padding:0px;margin:0px;}tr{background-image:url('https://i.postimg.cc/FzTgvcWz/cuerpo-mail.png'); background-repeat: repeat-y;background-size:100% 100%; padding:0px; margin:0px;}td{padding:0px; margin:0px;}</style></head><table style='padding:0px;marging:0px;border:0px;border-collapse: collapse;border-spacing:0px;'><tr><td><img src='https://i.postimg.cc/1319y6Dv/encabezado-mail.png' /></td></tr><tr><td style='padding:5% 5%; color:#b41547; font-size: 18px; text-align: center;'>Se realizo un cambio de estatus de la  solicitud de vacaciones, por parte de:<br>" + aprobador + "</td></tr><tr><td style='padding:0% 5%; color: #5c2a7e; font-size: 18px; text-align: left;'>Solicitante: " + empleado + "</td></tr><tr><td style='padding:0% 5%; color: #5c2a7e; font-size: 18px; text-align: left;'>Tipo Solicitud: " + solicitud + "</td></tr><tr><td style='padding:0% 5%; color: #5c2a7e; font-size: 18px; text-align: left;'>Fecha Inicio: " + fecha_inicio + "</td></tr><tr><td style=' padding:0% 5%; color: #5c2a7e; font-size: 18px; text-align: left;'>Fecha Fin: " + fecha_fin + "</td></tr><tr><td style=' padding:0% 5%; color: #5c2a7e; font-size: 18px; text-align: left;'>Observacion Solicitante: " + observacion + "</td></tr><tr><td style=' padding:5% 5% 0% 5%; color:#b41547; font-size: 18px; text-align: center; '>Nuevo estatus: "+estatus+ "</td></tr><tr><td style=' padding:0% 5%; color:#b41547; font-size: 18px; text-align: left; '> Observacion Aprobador: " + observacion_aprobador + "</td></tr><tr><td><img src='https://i.postimg.cc/hvSK9qPN/pie-mail.png' /></td></tr></table>";


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

                    cliente.Send(correo);
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
