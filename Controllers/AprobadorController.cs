using System;
using System.Collections.Generic;
using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using desconectate.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using System.Data;


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
                    SqlCommand cmd = new SqlCommand("SELECT * FROM empleados WHERE IdSAP = @IdSAP",conn);
                    cmd.Parameters.AddWithValue("@IdSAP", usuario);

                    SqlDataReader sqlReader = cmd.ExecuteReader();

                    sqlReader.Read();

                    empleado.IdSAP = sqlReader.GetInt32(1);
                    empleado.Nombre = sqlReader[2].ToString();
                    
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
                SqlCommand cmd = new SqlCommand("select s.folio, e.nombre, ts.solicitud, s.fecha_inicio,s.fecha_fin,es.descripcion from solicitudes s inner join empleados e on s.id_sap = e.IdSAP inner join ctipos_solicitud ts on s.tipo_solicitud = ts.id_tipo_solicitud inner join cestatus es on es.estatus = s.estatus  where id_sap_aprobador = @IdSAP",conn);
                cmd.Parameters.AddWithValue("@IdSAP",id_sap);

                SqlDataReader sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    lst.Add(new Solicitud { folio = sqlReader.GetInt32(0) , nombre = sqlReader[1].ToString(), solicitudName = sqlReader[2].ToString(), fecha_inicio = Convert.ToDateTime(sqlReader.IsDBNull(3) ? null : sqlReader[3]), fecha_fin = Convert.ToDateTime(sqlReader.IsDBNull(4) ? null : sqlReader[4]), estatusDescripcion = sqlReader[5].ToString()});
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
                SqlCommand cmd = new SqlCommand("SELECT s.id_sap,e.nombre,DATEDIFF(month,e.fecha_ingreso,GETDATE()) as antiguedad ,e.dias_disponibles, cts.maximo_dias, cts.solicitud as solicitudName, e.fecha_ingreso as aniversario, s.fecha_inicio, s.fecha_fin, s.folio  FROM solicitudes s inner join empleados e on s.id_sap = e.IdSAP inner join ctipos_solicitud cts on s.tipo_solicitud = cts.id_tipo_solicitud where s.folio = @folio ",conn);
                cmd.Parameters.AddWithValue("@folio",id_folio);

                SqlDataReader sqlReader = cmd.ExecuteReader();

                sqlReader.Read();

                infosolicitud.id_sap = sqlReader.GetInt32(0);
                infosolicitud.nombre = sqlReader[1].ToString();
                infosolicitud.antiguedad = sqlReader.GetInt32(2);
                infosolicitud.dias_disponibles = sqlReader.GetInt32(3);
                infosolicitud.maximo_dias = sqlReader.GetInt32(4);
                infosolicitud.solicitudName = sqlReader[5].ToString();
                infosolicitud.aniversario = Convert.ToDateTime(sqlReader.IsDBNull(6) ? null : sqlReader[6]);
                infosolicitud.fecha_inicio = Convert.ToDateTime(sqlReader.IsDBNull(7) ? null : sqlReader[7]);
                infosolicitud.fecha_fin = Convert.ToDateTime(sqlReader.IsDBNull(8) ? null : sqlReader[8]);
                infosolicitud.folio = sqlReader.GetInt32(9);
                
                conn.Close();
                return infosolicitud;
            }
        }
        
        public IActionResult ActualizarSolicitud(int id_folio, int s_estatus, String comentarios)
        {
            string connString = _configuration.GetConnectionString("MyConnection");
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("update solicitudes SET estatus = @status, observacion_aprobador = @coments where folio = @folios", conn);
                cmd.Parameters.AddWithValue("@folios", id_folio);
                cmd.Parameters.AddWithValue("@status", s_estatus);
                cmd.Parameters.AddWithValue("@coments",comentarios);

                var aux = cmd.ExecuteNonQuery();
                return Content(Convert.ToString(aux));
            }
        }
        
    }
}
