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
using Microsoft.AspNetCore.Http;

namespace desconectate.Controllers
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
            string usuario = HttpContext.Session.GetString("usuario");
            if(usuario == null)
                return View();
            else
                return RedirectToAction("Index", "Solicitante");
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
                        SqlCommand cmd = new SqlCommand("SELECT tipo FROM Empleados WHERE idsap = @idsap AND contrasena = @contrasena", conn);
                        cmd.Parameters.AddWithValue("@idsap", usuario);
                        cmd.Parameters.AddWithValue("@contrasena", contrasena);

                        SqlDataReader sqlReader = cmd.ExecuteReader();

                        try
                        {
                            sqlReader.Read();
                            string tipo  = sqlReader[0].ToString();
                            HttpContext.Session.SetString("usuario", usuario);
                            HttpContext.Session.SetString("tipo", tipo);

                            conn.Close();
                            return Content("1");
                        }
                        catch (Exception ex)
                        {
                            conn.Close();
                            return Content("No se encontro el usuario o contraseña incorrecta");
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

        public ActionResult Salir()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
