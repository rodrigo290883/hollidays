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
    public class ExtraeUsuariosController : Controller
    {
        private readonly IConfiguration _configuration;


        public ExtraeUsuariosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public List<Registro> ConsultaReporte()
        {
            List<Registro> records = new List<Registro>();

            string connString = _configuration.GetConnectionString("MyConnection"); // Read the connection string from the web.config file
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select e.idsap,e.nombre,e.email,e.contrasena,CONVERT(date,e.fecha_ingreso_grupo) as fecha_ingreso_grupo ,CONVERT(date,e.fecha_ingreso_uen) as fecha_ingreso_uen ,e.idsap_padre as idsap_line,e.nombre_line,e.esquema,e.rol,rd.periodo,rd.dias,rd.disponibles " +
                    ",(select SUM(dias) from registros_dias r where r.idsap = e.idsap and r.registro_padre = rd.registro and r.tipo = 0 and folio_solicitud IS NOT NULL GROUP BY r.registro_padre ) as solicitados from empleados e left join registros_dias rd on e.idsap = rd.idsap and rd.registro_padre = 0 and rd.caducidad >= getdate()  WHERE estatus != 2", conn);
                

                SqlDataReader sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    records.Add(new Registro { IDSAP = sqlReader[0].ToString(), NOMBRE = sqlReader[1].ToString(), EMAIL = sqlReader[2].ToString(), CONTRASENA = sqlReader[3].ToString(), FECHA_INGRESO_GRUPO = sqlReader[4].ToString(), FECHA_INGRESO_UEN = sqlReader[5].ToString(), IDSAP_LINE = sqlReader[6].ToString(), NOMBRE_LINE = sqlReader[7].ToString(), ESQUEMA = sqlReader[8].ToString(), ROL = sqlReader[9].ToString(), PERIODO = sqlReader[10].ToString(), DIAS = sqlReader[11].ToString(), DISPONIBLES = sqlReader[12].ToString(), SOLICITADOS= sqlReader[13].ToString() });
                }

                sqlReader.Close();

                using (var writer = new StreamWriter("extraccion_usuarios.csv"))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteHeader<Registro>();
                    csv.NextRecord();

                    foreach (Registro record in records)
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
            public string EMAIL { get; set; }
            public string CONTRASENA { get; set; }
            public string FECHA_INGRESO_GRUPO { get; set; }
            public string FECHA_INGRESO_UEN { get; set; }
            public string IDSAP_LINE { get; set; }
            public string NOMBRE_LINE { get; set; }
            public string ESQUEMA { get; set; }
            public string ROL { get; set; }
            public string PERIODO { get; set; }
            public string DIAS { get; set; }
            public string DISPONIBLES { get; set; }
            public string SOLICITADOS { get; set; }
        }


        public ActionResult DescargaCsv()
        {

            WebClient cliente = new WebClient();

            string idsap = HttpContext.Session.GetString("usuario");
            try
            {

                byte[] archivo = cliente.DownloadData("extraccion_usuarios.csv");
                return File(archivo, "text/csv", "usuarios.csv");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Content("No se encontro el archivo de poliza, favor de contactar a recursos humanos.");
            }

        }
    }
}
