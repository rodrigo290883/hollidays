using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using desconectate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using CsvHelper;
using CsvHelper.TypeConversion;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using System.Net;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace desconectate.Controllers
{
    public class ExtraccionController : Controller
    {
        private readonly IConfiguration _configuration;


        public ExtraccionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();           
        }

        public List<Registro> ConsultaReporte(string fecha_inicio, string fecha_fin)
        {
            List<Registro> records = new List<Registro>();

            string connString = _configuration.GetConnectionString("MyConnection"); // Read the connection string from the web.config file
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select e.idsap,e.nombre,e.area,s.fecha_inicio,s.fecha_fin,t.solicitud,es.descripcion " + 
                "from solicitudes s LEFT JOIN empleados e ON s.idsap = e.idsap LEFT JOIN ctipos_solicitud t ON s.tipo_solicitud = t.id_tipo_solicitud LEFT JOIN cestatus es ON s.estatus = es.estatus "+
                "WHERE s.estatus = 1 and ((CONVERT(date,@fecha_inicio) between fecha_inicio and fecha_fin) or (CONVERT(date,@fecha_fin) between fecha_inicio and fecha_fin))", conn);
                cmd.Parameters.AddWithValue("@fecha_inicio", fecha_inicio);
                cmd.Parameters.AddWithValue("@fecha_fin", fecha_fin);

                SqlDataReader sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    records.Add(new Registro { IDSAP = sqlReader[0].ToString(), NOMBRE = sqlReader[1].ToString(), AREA = sqlReader[2].ToString(), FECHA_INICIO = sqlReader[3].ToString(), FECHA_FIN = sqlReader[4].ToString(), MOTIVO = sqlReader[5].ToString(), ESTATUS = sqlReader[6].ToString() });
                }
                
                sqlReader.Close();

                using (var writer = new StreamWriter("extraccion.csv"))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteHeader<Registro>();
                    csv.NextRecord();

                    foreach (Registro  record in records)
                    {
                        csv.WriteRecord(record);
                        csv.NextRecord();
                    }
                }

                return records;
            }
        }

        public class Registro
        {
            public string IDSAP { get; set; }
            public string NOMBRE { get; set; }
            public string AREA { get; set; }
            public string FECHA_INICIO { get; set; }
            public string FECHA_FIN { get; set; }
            public string MOTIVO { get; set; }
            public string ESTATUS { get; set; }
        }


        public ActionResult DescargaCsv()
        {

            WebClient cliente = new WebClient();

            string idsap = HttpContext.Session.GetString("usuario");
            try
            {

                byte[] archivo = cliente.DownloadData("extraccion.csv");
                return File(archivo, "text/csv", "solicitudes.csv");
            }
            catch (Exception ex)
            {
                return Content("No se encontro el archivo de poliza, favor de contactar a recursos humanos.");
            }

        }
    }
}
