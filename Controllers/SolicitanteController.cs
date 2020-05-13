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
            if (usuario != null)
            {
                Empleados empleado = new Empleados();

                string connString = _configuration.GetConnectionString("MyConnection"); // Read the connection string from the web.config file
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select e.IdSAP,e.nombre,e.email,e.estatus,e.fecha_ingreso,e.dias_disponibles,e.ultimo_desconecte,e.url_poliza,e.idsap_padre,e.esquema,e.sexo," +
                        "DATEDIFF(month,e.fecha_ingreso,GETDATE()) as antiguedad, DATEDIFF(month,e.ultimo_desconecte,GETDATE()) as meses_ultimo_desconecte,iif(DATEDIFF(DAY,CONCAT(datepart(yyyy,getdate()),'-',(datepart(mm,e.fecha_ingreso)+1),'-',datepart(dd,e.fecha_ingreso)),getdate()) <=0,datepart(yyyy,getdate())-1,datepart(yyyy,getdate())) " +
                        "from dbo.empleados e  WHERE e.IdSAP = @IdSAP ", conn);
                    cmd.Parameters.AddWithValue("@IdSAP", usuario);

                    SqlDataReader sqlReader = cmd.ExecuteReader();

                    sqlReader.Read();

                    empleado.IdSAP = sqlReader.GetInt32(0);
                    empleado.Nombre = sqlReader[1].ToString();
                    empleado.email = sqlReader[2].ToString();
                    empleado.estatus = sqlReader.GetInt32(3);
                    empleado.fecha_ingreso = Convert.ToDateTime(sqlReader.IsDBNull(4) ? null : sqlReader[4]);
                    empleado.dias_disponibles = sqlReader.GetInt32(5);
                    empleado.ultimo_desconecte = Convert.ToDateTime(sqlReader.IsDBNull(6) ? null : sqlReader[6]);
                    empleado.url_poliza = sqlReader[7].ToString();
                    empleado.Idsap_padre = sqlReader.GetInt32(8);
                    empleado.esquema = sqlReader[9].ToString();
                    empleado.sexo = sqlReader[10].ToString()[0];
                    ViewBag.periodo = sqlReader.GetInt32(13);

                    empleado.antiguedad = sqlReader.GetInt32(11);
                    empleado.meses_ultimo_desconecte = sqlReader.GetInt32(12);
                    empleado.caducidad = Convert.ToDateTime(sqlReader.IsDBNull(6) ? null : sqlReader[6]);
                    
                    
                    

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

                    conn.Close();
                    return View();
                }
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
                SqlCommand cmd = new SqlCommand("insert into dbo.solicitudes (id_sap,tipo_solicitud,fecha_solicitud,fecha_inicio,fecha_fin,estatus,id_sap_aprobador,observacion_solicitante) VALUES (@IdSAP,@tipo_solicitud,GETDATE(),@fecha_inicio,@fecha_fin,0,@id_sap_aprobador,@observaciones); " + "SELECT CAST(scope_identity() AS int)", conn);
                cmd.Parameters.AddWithValue("@IdSAP", solicitud.id_sap);
                cmd.Parameters.AddWithValue("@tipo_solicitud", solicitud.tipo_solicitud);
                cmd.Parameters.AddWithValue("@fecha_inicio", solicitud.fecha_inicio);
                cmd.Parameters.AddWithValue("@fecha_fin", solicitud.fecha_fin);
                cmd.Parameters.AddWithValue("@id_sap_aprobador", solicitud.id_sap_aprobador);
                cmd.Parameters.AddWithValue("@observaciones", solicitud.observacion_solicitante);

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
                SqlCommand cmd = new SqlCommand("select sol.id_sap, emp.nombre as solicitante,sol.id_sap_aprobador,ap.nombre as aprobador, ap.email as email_aprobador,tip.solicitud ,sol.fecha_inicio ,sol.fecha_fin ,sol.fecha_solicitud,sol.observacion_solicitante from solicitudes sol left join empleados ap on sol.id_sap_aprobador = ap.IdSAP left join empleados emp on sol.id_sap = emp.IdSAP left join ctipos_solicitud tip on sol.tipo_solicitud = tip.id_tipo_solicitud where sol.folio = @folio ", conn);

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
                    string origen = "rodrigo290883@gmail.com";
                    
                    string pass = "AbrilSantiago";
                    string asunto = "Solicitud pendiente de Aprobacion Folio:" + folio;
                    string mensage = "<table><tr><td>Se realizo una solicitud de vacaciones por parte del colaborador: <b>"+empleado+"</b></td></tr><tr><td>Tipo Solicitud: "+solicitud+"</td></tr><tr><td>Fecha Inicio: "+fecha_inicio+"</td></tr><tr><td>Fecha Fin: "+fecha_fin+"</td></tr><tr><td>Observacion Solicitante: "+observacion+"</td></tr><tr><td>Favor de ingresar al sitio de <a href='#'>vacaciones</a> para su aprobacion.</td></tr></table>";


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

            public List<Solicitud> EnlistaSolicitudes(int id_sap)
        {
            List<Solicitud> lst = new List<Solicitud>();

            string connString = _configuration.GetConnectionString("MyConnection"); // Read the connection string from the web.config file
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT sol.folio,ts.solicitud,sol.fecha_inicio,sol.fecha_fin,sol.estatus,em.nombre,sol.observacion_solicitante FROM solicitudes sol LEFT JOIN ctipos_solicitud ts ON sol.tipo_solicitud = ts.id_tipo_solicitud LEFT JOIN empleados em ON sol.id_sap_aprobador = em.IdSAP WHERE id_sap = @IdSAP", conn);
                cmd.Parameters.AddWithValue("@IdSAP", id_sap);

                SqlDataReader sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    lst.Add(new Solicitud { folio = sqlReader.GetInt32(0), tipo_solicitud = sqlReader[1].ToString(), fecha_inicio = Convert.ToDateTime(sqlReader.IsDBNull(2) ? null : sqlReader[2]), fecha_fin = Convert.ToDateTime(sqlReader.IsDBNull(3) ? null : sqlReader[3]), estatus = sqlReader.GetInt32(4), aprobador = sqlReader[5].ToString(),observacion_solicitante = sqlReader[6].ToString() });
                }

                conn.Close();
                return lst;
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
