using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CsvHelper;
using System.IO;
using System.Globalization;
using desconectate.Models;

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

            using (var reader = new StreamReader("concentrado.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = new List<Empleados>();
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var record = new Empleados
                    {
                        idsap = csv.GetField<int>("SAP"),
                        nombre = csv.GetField("NOMBRE"),
                        area = csv.GetField("AREA"),
                        banda = csv.GetField("PUESTO"),
                        //fecha_ingreso_grupo = csv.GetField<DateTime>("FECHA INGRESO GRUPO"),
                        //fecha_ingreso_uen = csv.GetField<DateTime>("INGRESO A LA UEN"),
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
    }
}
