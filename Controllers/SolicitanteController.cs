using System;
using System.Collections.Generic;
using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using desconectate.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using System.Data;


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
                    SqlCommand cmd = new SqlCommand("select e.*,DATEDIFF(month,e.fecha_ingreso,GETDATE()) as antiguedad,rd.caducidad, DATEDIFF(month,e.ultimo_desconecte,GETDATE()) as meses_ultimo_desconecte from dbo.empleados e left join dbo.registros_dias rd on e.IdSAP  = rd.IdSAP WHERE e.IdSAP = @IdSAP and rd.id_tipo_solicitud = 0 and rd.periodo =2019", conn);
                    cmd.Parameters.AddWithValue("@IdSAP", usuario);

                    SqlDataReader sqlReader = cmd.ExecuteReader();

                    sqlReader.Read();

                    empleado.IdSAP = sqlReader.GetInt32(0);
                    empleado.Nombre = sqlReader[1].ToString();
                    empleado.email = sqlReader[2].ToString();
                    empleado.fecha_ingreso = Convert.ToDateTime(sqlReader.IsDBNull(7) ? null : sqlReader[7]);
                    empleado.ultimo_desconecte = Convert.ToDateTime(sqlReader.IsDBNull(9) ? null : sqlReader[9]);
                    empleado.url_poliza = sqlReader[10].ToString();
                    empleado.estatus = sqlReader.GetInt32(4);
                    empleado.dias_disponibles = sqlReader.GetInt32(8);
                    empleado.antiguedad = sqlReader.GetInt32(14);
                    empleado.sexo = sqlReader[13].ToString()[0];
                    empleado.caducidad = Convert.ToDateTime(sqlReader.IsDBNull(15) ? null : sqlReader[15]);
                    empleado.meses_ultimo_desconecte = sqlReader.GetInt32(16);

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
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into dbo.solicitudes (id_sap,tipo_solicitud,fecha_solicitud,fecha_inicio,fecha_fin) VALUES (@IdSAP,@tipo_solicitud,GETDATE(),@fecha_inicio,@fecha_fin)", conn);
                cmd.Parameters.AddWithValue("@IdSAP", solicitud.id_sap);
                cmd.Parameters.AddWithValue("@tipo_solicitud", solicitud.tipo_solicitud);
                cmd.Parameters.AddWithValue("@fecha_inicio", solicitud.fecha_inicio);
                cmd.Parameters.AddWithValue("@fecha_fin", solicitud.fecha_fin);

                var aux = cmd.ExecuteNonQuery();
                return Content(Convert.ToString(aux));
            }
        }

        public List<Solicitud> EnlistaSolicitudes(int id_sap)
        {
            List<Solicitud> lst = new List<Solicitud>();

            string connString = _configuration.GetConnectionString("MyConnection"); // Read the connection string from the web.config file
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT sol.folio,ts.solicitud,sol.fecha_inicio,sol.fecha_fin FROM solicitudes sol LEFT JOIN ctipos_solicitud ts ON sol.tipo_solicitud = ts.id_tipo_solicitud  WHERE id_sap = @IdSAP", conn);
                cmd.Parameters.AddWithValue("@IdSAP", id_sap);

                SqlDataReader sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    lst.Add(new Solicitud { folio = sqlReader.GetInt32(0), tipo_solicitud = sqlReader[1].ToString(), fecha_inicio = Convert.ToDateTime(sqlReader.IsDBNull(2) ? null : sqlReader[2]), fecha_fin = Convert.ToDateTime(sqlReader.IsDBNull(3) ? null : sqlReader[3]) });
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
