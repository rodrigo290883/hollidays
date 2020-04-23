using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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

                    conn.Close();
                    return View(empleado);
                }
            }
            else
                return RedirectToAction("Index", "Home");
            
        }

        public IActionResult ObtieneTipos()
        {
                string connString = _configuration.GetConnectionString("MyConnection"); // Read the connection string from the web.config file
                using (SqlConnection conn = new SqlConnection(connString))
                {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("SELECT * FROM ctipos_solicitud WHERE seleccionable = 1", conn);

                        SqlDataReader sqlReader = cmd.ExecuteReader();

                        

                        

                        sqlReader.Close();
                        cmd.Dispose();
                        conn.Close();

                    return Content("1");
                }

                        

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
