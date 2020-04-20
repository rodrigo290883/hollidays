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
                return View();
            else
                return RedirectToAction("Index", "Home");
            
        }

        public ActionResult Solicitar(int idsap,int tipo_solicitud, string fecha_inicio, string fecha_fin )
        {
            try
            {
                string connString = _configuration.GetConnectionString("MyConnection"); // Read the connection string from the web.config file
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("SELECT count(*) FROM Empleados WHERE email = @email AND contrasena = @contrasena", conn);
                        cmd.Parameters.AddWithValue("@email", idsap);
                        cmd.Parameters.AddWithValue("@contrasena", tipo_solicitud);

                        if (Convert.ToBoolean(cmd.ExecuteScalar()))
                        {
                            //Session['usuario'] = usuario;
                            conn.Close();
                            return Content("1");
                        }
                        else
                        {
                            conn.Close();
                            return Content("No se encontro el usuario o password incorrecto");
                        }
                    

                }
                //return Content("1");

            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
