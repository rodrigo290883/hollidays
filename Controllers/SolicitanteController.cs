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
using System.IO;
using System.Net.Http;
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

            CultureInfo culture = new CultureInfo("es-ES");

            if (usuario != null && ViewBag.tipo != "A" && ViewBag.tipo != "P" && ViewBag.tipo != "R")
            {
                Empleados empleado = new Empleados();

                string connString = _configuration.GetConnectionString("MyConnection"); // Read the connection string from the web.config file
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT TOP 1  e.idsap,e.nombre,e.email,e.estatus ,e.fecha_ingreso_grupo ,rd.disponibles,e.ultimo_desconecte,e.idsap_padre,"+
                    "e.esquema, DATEDIFF(month, e.fecha_ingreso_grupo, GETDATE()) as antiguedad, DATEDIFF(month, e.ultimo_desconecte, GETDATE()) as meses_ultimo_desconecte,rd.periodo, e.avatar," +
                    " e.email_line, e.nombre_line, cr.semana, (SELECT SUM(dias) FROM registros_dias rd2 WHERE registro_padre = rd.registro AND id_tipo_solicitud = 0) as tomados, rd.caducidad"+
                    "  FROM registros_dias rd LEFT JOIN empleados e ON rd.idsap = e.idsap LEFT JOIN croles cr ON e.rol = cr.rol"+
                    " WHERE rd.idsap = @idsap and rd.caducidad >= getdate() and rd.registro_padre = 0 and rd.disponibles != 0 order by rd.fecha_creacion asc; ", conn);
                    cmd.Parameters.AddWithValue("@idsap", usuario);

                    SqlDataReader sqlReader = cmd.ExecuteReader();

                    sqlReader.Read();

                    empleado.idsap = sqlReader.GetInt32(0);
                    empleado.nombre = sqlReader[1].ToString();
                    empleado.email = sqlReader[2].ToString();
                    empleado.estatus = sqlReader.GetInt32(3);
                    empleado.fecha_ingreso_grupo = Convert.ToDateTime(sqlReader.IsDBNull(4) ? null : sqlReader[4]);
                    empleado.dias_disponibles = sqlReader.IsDBNull(5)?0:sqlReader.GetInt32(5);
                    empleado.ultimo_desconecte = Convert.ToDateTime(sqlReader.IsDBNull(6) ? null : sqlReader[6]);
                    //empleado.url_poliza = sqlReader[7].ToString();
                    empleado.idsap_padre = sqlReader.GetInt32(7);
                    empleado.nombre_line = sqlReader[14].ToString();//
                    empleado.email_line = sqlReader[13].ToString();//
                    empleado.esquema = sqlReader.GetInt32(8);

                    ViewBag.periodo = sqlReader[11].ToString();
                    ViewBag.dias_tomados = sqlReader[16].ToString();

                    empleado.antiguedad = sqlReader.GetInt32(9);
                    empleado.meses_ultimo_desconecte = sqlReader.IsDBNull(10)?100: sqlReader.GetInt32(10);
                    empleado.caducidad = Convert.ToDateTime(sqlReader.IsDBNull(17) ? null : sqlReader[17]);
                    empleado.avatar = sqlReader[12].ToString();
                    empleado.rol = sqlReader[15].ToString();

                    ViewBag.Empleado = empleado;

                    sqlReader.Close();

                    List<TipoSolicitud> lst = new List<TipoSolicitud>();

                    cmd = new SqlCommand("SELECT * FROM ctipos_solicitud WHERE seleccionable = 1", conn);

                    sqlReader = cmd.ExecuteReader();

                    while (sqlReader.Read())
                    {
                        lst.Add(new TipoSolicitud{  id_tipo_solicitud = sqlReader.GetInt32(0), solicitud = sqlReader[1].ToString(), maximo_dias = sqlReader.GetInt32(2), consume_vacaciones = sqlReader[4].ToString()[0] });
                    }

                    ViewBag.Opciones = lst;

                    sqlReader.Close();

                    cmd = new SqlCommand("SELECT fecha FROM cdias_festivos WHERE fecha >= GETDATE() and pais = 'MX'", conn);

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
                    "LEFT JOIN ctipos_solicitud ts ON sol.tipo_solicitud = ts.id_tipo_solicitud WHERE sol.idsap = @idsap and sol.fecha_inicio >= GETDATE()", conn);
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


        public ActionResult DescargaPoliza()
        {
        
            WebClient cliente = new WebClient();

            string idsap = HttpContext.Session.GetString("usuario");
            try
            {

                byte[] archivo = cliente.DownloadData("GMM/"+idsap + ".pdf");
                return File(archivo, "application/pdf", "PGMM_" + idsap + ".pdf");
            }
            catch (Exception ex)
            {
                return Content("No se encontro el archivo de poliza, favor de contactar a recursos humanos.");
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
