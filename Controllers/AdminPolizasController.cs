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
    public class AdminPolizasController : Controller
    {

         private  readonly IConfiguration _configuration;

        public AdminPolizasController(IConfiguration configuration)
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

        public List<Empleados> BuscarSap(string valor){

            List<Empleados> lst = new List<Empleados>();

            string connString = _configuration.GetConnectionString("MyConnection");
            
            using(SqlConnection conn = new SqlConnection(connString)){
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT idsap,nombre,area,banda FROM empleados WHERE idsap LIKE '"+valor+"%' OR nombre LIKE '"+valor+ "%' OR area LIKE '" + valor + "%' ;", conn);
                cmd.Parameters.AddWithValue("@valor",valor);

                SqlDataReader sqlReader = cmd.ExecuteReader();

                while(sqlReader.Read()){
                    lst.Add(new Empleados { idsap =sqlReader.GetInt32(0), nombre = sqlReader[1].ToString(), area = sqlReader[2].ToString(), banda = sqlReader[3].ToString()});
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
                SqlCommand cmd = new SqlCommand("SELECT idsap,nombre,area,banda FROM empleados ",conn);

                SqlDataReader sqlReader = cmd.ExecuteReader();

                while(sqlReader.Read()){
                    lst.Add(new Empleados { idsap = sqlReader.GetInt32(0), nombre = sqlReader[1].ToString(), area = sqlReader[2].ToString(), banda = sqlReader[3].ToString()});
                }

                conn.Close();
                return lst;
            }
        }

        public ActionResult DescargaPoliza(int idsap)
        {
        
            WebClient cliente = new WebClient();

            /*string idsap = HttpContext.Session.GetString("usuario");*/
            try
            {

                byte[] archivo = cliente.DownloadData("GMM/"+idsap + ".pdf");
                return File(archivo, "application/pdf", "PGMM_" + idsap + ".pdf");
            }
            catch (Exception ex)
            {
                return Content("No se encontro el archivo de poliza, favor de contactar a recursos humanos.   "+idsap);
            }
        
        }
    }
}
