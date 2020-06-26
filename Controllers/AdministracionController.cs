using System;
using System.Collections.Generic;
using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using desconectate.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Net;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace desconectate.Controllers
{
    public class AdministracionController : Controller
    {

         private  readonly IConfiguration _configuration;

        public AdministracionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            string usuario = HttpContext.Session.GetString("usuario");
            if (usuario != null)
                return View();
            else
                return RedirectToAction("Index", "Home");
        }

        public List<Empleados> BuscarSap(int valor){

            List<Empleados> lst = new List<Empleados>();

            string connString = _configuration.GetConnectionString("MyConnection");
            
            using(SqlConnection conn = new SqlConnection(connString)){
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT idsap,nombre,area FROM empleados WHERE idsap LIKE '%@valor%';");
                cmd.Parameters.AddWithValue("@valor",valor);

                SqlDataReader sqlReader = cmd.ExecuteReader();

                while(sqlReader.Read()){
                    lst.Add(new Empleados { idsap =sqlReader.GetInt32(0), nombre = sqlReader[1].ToString(), area = sqlReader[2].ToString()});
                }

                conn.Close();
                return lst;
            }
        }

        public List<Empleados> BuscarAll(){

            List<Empleados> lst = new List<Empleados>();

            string connString = _configuration.GetConnectionString("MyConnection");
            
            using(SqlConnection conn = new SqlConnection(connString)){

                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT idsap,nombre,area FROM empleados ");

                SqlDataReader sqlReader = cmd.ExecuteReader();

                while(sqlReader.Read()){
                    lst.Add(new Empleados { idsap = sqlReader.GetInt32(0), nombre = sqlReader[1].ToString(), area = sqlReader[2].ToString()});
                }

                conn.Close();
                return lst;
            }
        }
    }
}
