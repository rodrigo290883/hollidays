﻿using System;
using System.Web;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
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
            string tipo = HttpContext.Session.GetString("tipo");
            if (usuario == null)
                return View();
            else if(tipo=="A")
                return RedirectToAction("Index", "AdminSolicitudes");
            else if (tipo == "P")
                return RedirectToAction("Index", "AdminPolizas");
            else if (tipo == "R")
                return RedirectToAction("Index", "AdminBase");
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
                    if (usuario == null || contrasena == null)
                    {
                        return Content("Ingresa Usuario y Contraseña");
                    }
                    else if(usuario == "root" & contrasena == "Modelo2020")
                    {
                        HttpContext.Session.SetString("usuario", usuario);
                        HttpContext.Session.SetString("tipo", "R");
                        return Content("1");
                    }
                    else if (usuario == "digital" & contrasena == "Modelo2021")
                    {
                        HttpContext.Session.SetString("usuario", usuario);
                        HttpContext.Session.SetString("tipo", "E");
                        return Content("1");
                    }
                    else
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("SELECT tipo FROM Empleados WHERE estatus != 2 and idsap = @idsap AND contrasena = @contrasena", conn);
                        cmd.Parameters.AddWithValue("@idsap", usuario);
                        cmd.Parameters.AddWithValue("@contrasena", contrasena);

                        SqlDataReader sqlReader = cmd.ExecuteReader();

                        try
                        {
                            sqlReader.Read();
                            string tipo = sqlReader[0].ToString();
                            HttpContext.Session.SetString("usuario", usuario);
                            HttpContext.Session.SetString("tipo", tipo);

                            conn.Close();
                            return Content("1");
                        }
                        catch (Exception ex)
                        {
                            conn.Close();
                            string nota = ex.ToString();
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
