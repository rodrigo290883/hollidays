using System;
using System.Web;

using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using desconectate.Models;

namespace mvc_dotnet.Controllers
{
    public class HomeController : Controller
    {

        private readonly IConfiguration _configuration;
        

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Politicas()
        {
            return View();
        }

        public ActionResult Entrar(string usuario, string contrasena)
        {
            try
            {
                string connString = _configuration.GetConnectionString("MyConnection"); // Read the connection string from the web.config file
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    if(usuario == null || contrasena == null)
                    {
                        return Content("Ingresa Usuario y Contraseña");
                    }
                    else
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("SELECT count(*) FROM Empleados WHERE email = @email AND contrasena = @contrasena", conn);
                        cmd.Parameters.AddWithValue("@email", usuario);
                        cmd.Parameters.AddWithValue("@contrasena", contrasena);

                        if (Convert.ToBoolean(cmd.ExecuteScalar()))
                        {
                            //Session["usuario"] = 1;
                            conn.Close();
                            return Content("1");
                        }
                        else
                        {
                            conn.Close();
                            return Content("No se encontro el usuario o password incorrecto");
                        }
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
