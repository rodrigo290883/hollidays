using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CsvHelper;
using CsvHelper.TypeConversion;
using System.IO;
using System.Globalization;
using desconectate.Models;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace desconectate.Controllers
{
    public class CargaEmpleadosController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public List<Empleados> LeeArchivo()
        {

            using (var reader = new StreamReader("con.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = new List<Empleados>();
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    Empleados record = new Empleados
                    {
                        idsap = csv.GetField<int>("SAP"),
                        nombre = csv.GetField("NOMBRE"),
                        area = csv.GetField("AREA"),
                        banda = csv.GetField("PUESTO"),
                        fecha_ingreso_grupo = DateTime.ParseExact(csv.GetField("FECHA INGRESO GRUPO"), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        fecha_ingreso_uen = DateTime.ParseExact(csv.GetField("INGRESO A LA UEN"), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        dias_disponibles = csv.GetField<int>("PENDIENTES POR GOZAR"),
                        email = csv.GetField("CORREO COLABORADOR"),
                        idsap_padre = csv.GetField<int>("SAP_LINE"),
                        nombre_line = csv.GetField("LINE MANAGER"),
                        email_line = csv.GetField("CORREO LINE")
                    };
                    records.Add(record);
                }
                return records;
            }

        }

        public bool SubeArchivo(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            var filePaths = new List<string>();

            try
            {

                foreach (var formFile in files)
                {
                    if (formFile.Length > 0)
                    {
                        // full path to file in temp location
                        var filePath = "con.csv"; //we are using Temp file name just for the example. Add your own file path.
                        filePaths.Add(filePath);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            formFile.CopyTo(stream);
                        }
                    }
                }

                // process uploaded files
                // Don't rely on or trust the FileName property without validation.

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
