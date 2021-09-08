using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using desconectate.Models;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace desconectate.Controllers
{
    public class ReportePolizasController : Controller
    {
        private readonly IConfiguration _configuration;


        public ReportePolizasController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            string path = @"Polizas";
            string searchPattern = "*.pdf";

            DirectoryInfo di = new DirectoryInfo(path);

            FileInfo[] files =
            di.GetFiles(searchPattern, SearchOption.TopDirectoryOnly);

            string connString = _configuration.GetConnectionString("MyConnection"); // Read the connection string from the web.config file
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT idsap,nombre,area,banda,fecha_ingreso_grupo FROM empleados WHERE tipo not in ('A','P') and estatus != 2;", conn);

                SqlDataReader sqlReader = cmd.ExecuteReader();

                List<Poliza> lst = new List<Poliza>();

                string existe = "";

                while (sqlReader.Read())
                {
                    int id = sqlReader.GetInt32(0);

                    var busqueda = Array.Find(files, element => element.Name == id.ToString()+".pdf");
                    if (busqueda != null)
                    {
                        existe = "con_poliza";
                    }
                    else
                    {
                        existe = "sin_poliza";
                    }

                    lst.Add(new Poliza { idsap = sqlReader.GetInt32(0), nombre = sqlReader[1].ToString(), area = sqlReader[2].ToString(), banda = sqlReader[3].ToString(), fecha_ingreso_grupo = Convert.ToDateTime(sqlReader.IsDBNull(4) ? null : sqlReader[4]), archivo_poliza = existe });
                }

                ViewBag.polizas = lst;

                return View();
            }
        }

        
    }
}
