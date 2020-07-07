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
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace desconectate.Controllers
{
    public class CargaEmpleadosController : Controller
    {

        private readonly IConfiguration _configuration;

        public CargaEmpleadosController(IConfiguration configuration)
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
                return RedirectToAction("Index", "Home");
        }

        public List<int> ProcesaRegistros(int numero_registros)
        {
            List<Empleados> registros = LeeArchivo();
            int num = registros.Count;

            if (num == numero_registros)
            {
                
                int insertados = 0;
                int actualizados = 0;
                int fallo = 0;

                string connString = _configuration.GetConnectionString("MyConnection"); // Read the connection string from the web.config file
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    foreach(Empleados registro in registros)
                    {
                        SqlCommand cmd = new SqlCommand("SELECT idsap FROM empleados WHERE idsap = @idsap ", conn);
                        cmd.Parameters.AddWithValue("@idsap", registro.idsap);

                        LogClass log = new LogClass(_configuration);

                        if (cmd.ExecuteScalar() == null)
                        {
                            //Se inserta el empleado
                            cmd = new SqlCommand("INSERT INTO empleados(idsap,nombre,email,area,banda,fecha_ingreso_grupo,fecha_ingreso_uen,dias_disponibles,idsap_padre,nombre_line,email_line,contrasena,tipo,esquema,rol,estatus) "+
                                "VALUES (@idsap,@nombre,@email,@area,@banda,@fecha_ingreso_grupo,@fecha_ingreso_uen,@dias_disponibles,@idsap_padre,@nombre_line,@email_line,'12345','S',0,0,0)", conn);
                            cmd.Parameters.AddWithValue("@idsap", registro.idsap);
                            cmd.Parameters.AddWithValue("@nombre", registro.nombre);
                            cmd.Parameters.AddWithValue("@email", registro.email);
                            cmd.Parameters.AddWithValue("@area", registro.area);
                            cmd.Parameters.AddWithValue("@banda", registro.banda);
                            cmd.Parameters.AddWithValue("@fecha_ingreso_grupo", registro.fecha_ingreso_grupo);
                            cmd.Parameters.AddWithValue("@fecha_ingreso_uen", registro.fecha_ingreso_uen);
                            cmd.Parameters.AddWithValue("@dias_disponibles", registro.dias_disponibles);
                            cmd.Parameters.AddWithValue("@idsap_padre", registro.idsap_padre);
                            cmd.Parameters.AddWithValue("@nombre_line", registro.nombre_line);
                            cmd.Parameters.AddWithValue("@email_line", registro.email_line);

                            int n = cmd.ExecuteNonQuery();
                            if (n != 0)
                            {
                                DateTime now = DateTime.Now;
                                log.idsap = registro.idsap;
                                log.log = "Se creo el registro del empleado: " + registro.idsap + " mediante carga de empleados";
                                log.fecha_creacion = now;
                                log.idsap_creacion = 101010;
                                log.createLog();
                                insertados++;
                            }
                            else
                            {
                                fallo++;
                            }
                        }

                        else
                        {
                            //Se actualiza el empleado
                            cmd = new SqlCommand("UPDATE empleados SET email=@email,area=@area,banda=@banda,fecha_ingreso_grupo=@fecha_ingreso_grupo,fecha_ingreso_uen=fecha_ingreso_uen, "+
                                "dias_disponibles=@dias_disponibles,idsap_padre=@idsap_padre,nombre_line=@nombre_line,email_line=@email_line " +
                                "WHERE idsap = @idsap", conn);
                            cmd.Parameters.AddWithValue("@idsap", registro.idsap);
                            cmd.Parameters.AddWithValue("@email", registro.email);
                            cmd.Parameters.AddWithValue("@area", registro.area);
                            cmd.Parameters.AddWithValue("@banda", registro.banda);
                            cmd.Parameters.AddWithValue("@fecha_ingreso_grupo", registro.fecha_ingreso_grupo);
                            cmd.Parameters.AddWithValue("@fecha_ingreso_uen", registro.fecha_ingreso_uen);
                            cmd.Parameters.AddWithValue("@dias_disponibles", registro.dias_disponibles);
                            cmd.Parameters.AddWithValue("@idsap_padre", registro.idsap_padre);
                            cmd.Parameters.AddWithValue("@nombre_line", registro.nombre_line);
                            cmd.Parameters.AddWithValue("@email_line", registro.email_line);

                            int n = cmd.ExecuteNonQuery();
                            if (n != 0)
                            {
                                DateTime now = DateTime.Now;
                                log.idsap = registro.idsap;
                                log.log = "Se actualizo el empleado: "+registro.idsap+" mediante carga de empleados";
                                log.fecha_creacion = now;
                                log.idsap_creacion = 101010;
                                log.createLog();
                                actualizados++;
                            }
                            else
                                fallo++;
                        }
                    }
                    
                }
                var mensaje = new List<int> {num,insertados,actualizados,fallo };
                return mensaje;
            }
            else
            {
                var mensaje = new List<int> { 0 };
                return mensaje;
            }
            
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
