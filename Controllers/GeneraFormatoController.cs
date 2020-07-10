using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace desconectate.Controllers
{
    public class GeneraFormatoController : Controller
    {
        private readonly IConfiguration _configuration;


        public GeneraFormatoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            string usuario = HttpContext.Session.GetString("usuario");
            if (usuario != null)
            {
                ViewBag.tipo = HttpContext.Session.GetString("tipo");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public List<RegistroFormato> GeneraReporte(DateTime fecha_inicio, DateTime fecha_fin)
        {
            List<RegistroFormato> lista = new List<RegistroFormato>();

            string connString = _configuration.GetConnectionString("MyConnection"); // Read the connection string from the web.config file
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT rd.registro,tip.clave_as400,tip.solicitud,sol.fecha_inicio,sol.fecha_fin,rd.dias,emp.idsap,CASE WHEN emp.esquema = 0 THEN 'DIBLO' WHEN emp.esquema = 1 THEN 'EDM' END as sociedad, sol.dias_detalle,tip.con_goce,sol.con_goce "+
                                        "FROM registros_dias rd LEFT JOIN solicitudes sol ON rd.folio_solicitud = sol.folio LEFT JOIN ctipos_solicitud tip ON rd.id_tipo_solicitud = tip.id_tipo_solicitud "+
                                        "LEFT JOIN empleados emp ON rd.idsap = emp.idsap WHERE rd.folio_solicitud != 0 and CONVERT(date, rd.fecha_creacion) between @inicio AND @fin;", conn);
                cmd.Parameters.AddWithValue("@inicio", fecha_inicio);
                cmd.Parameters.AddWithValue("@fin", fecha_fin);

                SqlDataReader sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var tipo = sqlReader[1].ToString();
                    var con_goce = sqlReader.GetInt32(9);
                    if (con_goce == 1)
                    {
                        string[] tipos = tipo.Split(",");
                        var goce = sqlReader[10].ToString();
                        if (goce == "S")
                        {
                            tipo = tipos[0];
                        }
                        else
                        {
                            tipo = tipos[1];
                        }

                    }
                    
                    lista.Add(new RegistroFormato
                    {
                        sociedad = sqlReader[7].ToString(),
                        sap = sqlReader.GetInt32(6),
                        concepto = tipo,
                        dias_detalle = sqlReader[8].ToString()
                    });
                }

                return lista;
            }
        }

        public IActionResult DescargaFormato(DateTime fecha_inicio, DateTime fecha_fin)
        {
            List<RegistroFormato>  lista = GeneraReporte(fecha_inicio,fecha_fin);


            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet1 = workbook.CreateSheet("Sheet1");
            IRow row = sheet1.CreateRow(0);
            row.CreateCell(0).SetCellValue("SOCIEDAD");
            row.CreateCell(1).SetCellValue("SAP");
            row.CreateCell(2).SetCellValue("CONCEPTO");
            row.CreateCell(3).SetCellValue("FECHA DE MOVIMIENTO");
            row.CreateCell(4).SetCellValue("IMPORTE");

            var i = 1;

            foreach (RegistroFormato registro in lista)
            {
                string[] detalle = registro.dias_detalle.Split(",");
                foreach (string dia in detalle) {
                    row = sheet1.CreateRow(i);
                    row.CreateCell(0).SetCellValue(registro.sociedad);
                    row.CreateCell(1).SetCellValue(registro.sap);
                    row.CreateCell(2).SetCellValue(registro.concepto);
                    row.CreateCell(3).SetCellValue(dia);
                    row.CreateCell(4).SetCellValue(1);
                    i++;
                } 
            }

            FileStream sw = new FileStream("test.xlsx", FileMode.Create);
            workbook.Write(sw);
            sw.Close();

            return Content("1");
        }

        public class RegistroFormato
        {
            public string sociedad { get; set; }
            public int sap { get; set; }
            public string concepto { get; set; }
            public string dias_detalle { get; set; }
        }
    }
}
