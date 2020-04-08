using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mvc_dotnet.Models;

namespace mvc_dotnet.Controllers
{
    public class HomeController : Controller
    {
       

        public object Session { get; private set; }

       

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
                
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Select count(*) from Empleados where email='" + usuario + "' and contrasena='" + contrasena + "'", conn);
                    if (Convert.ToBoolean(cmd.ExecuteScalar())){
                        return Content("1");
                    }
                    else
                    {
                        return Content("No se encontro el usuario o password incorrecto");
                    }
                }
                //return Content("1");
                    
            }
            catch (Exception ex)
            {
                return Content("Ocurrio un error" + ex.Message);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
