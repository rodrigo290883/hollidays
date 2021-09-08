using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CsvHelper;
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
                int existe;
                int esquema = 0;
                int estatus;
                int idsap_padre = 0;

                string connString = _configuration.GetConnectionString("MyConnection"); // Read the connection string from the web.config file
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    foreach(Empleados registro in registros)
                    {
                        SqlCommand select = new SqlCommand("SELECT esquema,idsap,idsap_padre FROM empleados WHERE idsap = @idsap;", conn);
                        select.Parameters.AddWithValue("@idsap", registro.idsap);

                        SqlDataReader sqlReader = select.ExecuteReader();

                        if(sqlReader.Read()){
                            existe = 1;
                            esquema = sqlReader.GetInt32(0);
                            idsap_padre = sqlReader.GetInt32(2);
                        }
                        else
                        {
                            existe = 0;
                        }
                        
                        

                        sqlReader.Close();
                       

                        LogClass log = new LogClass(_configuration);


                        if (existe == 0)// Si es alta de empleado se crean los registros necesarios
                        {
                            //Se inserta el empleado
                            
                            estatus = 0;

                            SqlCommand cmd;

                            if (registro.ultimo_desconecte != null)
                            {
                                cmd = new SqlCommand("INSERT INTO empleados(idsap,nombre,email,area,banda,fecha_ingreso_grupo,fecha_ingreso_uen,dias_disponibles,ultimo_desconecte,idsap_padre,nombre_line,email_line,contrasena,tipo,esquema,rol,estatus,rfc) " +
                                    "VALUES (@idsap,@nombre,@email,@area,@banda,@fecha_ingreso_grupo,@fecha_ingreso_uen,@dias_disponibles,@ultimo_desconecte,@idsap_padre,@nombre_line,@email_line,'12345','S',@esquema,@rol,@estatus,@rfc)", conn);
                                cmd.Parameters.AddWithValue("@idsap", registro.idsap);
                                cmd.Parameters.AddWithValue("@nombre", registro.nombre);
                                cmd.Parameters.AddWithValue("@email", registro.email);
                                cmd.Parameters.AddWithValue("@area", registro.area);
                                cmd.Parameters.AddWithValue("@banda", registro.banda);
                                cmd.Parameters.AddWithValue("@fecha_ingreso_grupo", registro.fecha_ingreso_grupo);
                                cmd.Parameters.AddWithValue("@fecha_ingreso_uen", registro.fecha_ingreso_uen);
                                cmd.Parameters.AddWithValue("@dias_disponibles", registro.dias_disponibles);
                                cmd.Parameters.AddWithValue("@ultimo_desconecte", registro.ultimo_desconecte);
                                cmd.Parameters.AddWithValue("@idsap_padre", registro.idsap_padre);
                                cmd.Parameters.AddWithValue("@nombre_line", registro.nombre_line);
                                cmd.Parameters.AddWithValue("@email_line", registro.email_line);
                                cmd.Parameters.AddWithValue("@esquema", registro.esquema);
                                cmd.Parameters.AddWithValue("@rol", registro.rol);
                                cmd.Parameters.AddWithValue("@estatus", estatus);
                                cmd.Parameters.AddWithValue("@rfc", registro.rfc);
                            }
                            else
                            {
                                cmd = new SqlCommand("INSERT INTO empleados(idsap,nombre,email,area,banda,fecha_ingreso_grupo,fecha_ingreso_uen,dias_disponibles,idsap_padre,nombre_line,email_line,contrasena,tipo,esquema,rol,estatus,rfc) " +
                                    "VALUES (@idsap,@nombre,@email,@area,@banda,@fecha_ingreso_grupo,@fecha_ingreso_uen,@dias_disponibles,@idsap_padre,@nombre_line,@email_line,'12345','S',@esquema,@rol,@estatus,@rfc)", conn);
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
                                cmd.Parameters.AddWithValue("@esquema", registro.esquema);
                                cmd.Parameters.AddWithValue("@rol", registro.rol);
                                cmd.Parameters.AddWithValue("@estatus", estatus);
                                cmd.Parameters.AddWithValue("@rfc", registro.rfc);
                            }

                            int n = cmd.ExecuteNonQuery();
                            if (n != 0)
                            {
                                DateTime now = DateTime.Now;
                                log.idsap = registro.idsap;
                                log.log = "Se creo el registro del empleado: " + registro.idsap + " mediante carga de empleados";
                                log.fecha_creacion = now;
                                log.idsap_creacion = 101010;
                                log.createLog();

                                var hoy = DateTime.Now;
                                var periodo = hoy.Year;
                                var ingreso_uen = (DateTime)registro.fecha_ingreso_uen;

                                if (ingreso_uen.Month > hoy.Month)
                                {
                                    periodo = hoy.Year - 1;
                                }
                                else if(ingreso_uen.Month == hoy.Month && ingreso_uen.Day > hoy.Day)
                                {
                                    periodo = hoy.Year - 1;
                                }


                                // Creacion de registros de dias disponibles
                                if(registro.previo != 0)
                                {
                                    int dias_previo = 0;
                                    int dias_gozados = 0;

                                    if(registro.dias_gozados >= registro.previo)
                                    {
                                        dias_previo = 0;
                                        dias_gozados = (int)(registro.dias_gozados - registro.previo);
                                    }
                                    else
                                    {
                                        dias_previo = (int)(registro.previo - registro.dias_gozados);
                                        dias_gozados = 0;
                                    }
                                    // Si tiene 
                                    SqlCommand cmd3 = new SqlCommand("INSERT INTO registros_dias(idsap,periodo,registro_padre,dias,disponibles,caducidad) VALUES (@idsap,@periodo,0,@dias,@dias_disponibles,DATEADD(month, 13, Convert(date, CONCAT(@periodo, '-', (datepart(mm, @ingreso)), '-', datepart(dd, @ingreso)))));", conn);
                                    cmd3.Parameters.AddWithValue("@periodo", periodo - 1);
                                    cmd3.Parameters.AddWithValue("@idsap", registro.idsap);
                                    cmd3.Parameters.AddWithValue("@dias", registro.previo);
                                    cmd3.Parameters.AddWithValue("@dias_disponibles", dias_previo);
                                    cmd3.Parameters.AddWithValue("@ingreso", registro.fecha_ingreso_uen);

                                    cmd3.ExecuteNonQuery();

                                    SqlCommand cmd2 = new SqlCommand("INSERT INTO registros_dias(idsap,periodo,registro_padre,dias,disponibles,caducidad) VALUES (@idsap,@periodo,0,@dias,@dias_disponibles,DATEADD(month, 13, Convert(date, CONCAT(@periodo, '-', (datepart(mm, @ingreso)), '-', datepart(dd, @ingreso)))));", conn);
                                    cmd2.Parameters.AddWithValue("@periodo", periodo);
                                    cmd2.Parameters.AddWithValue("@idsap", registro.idsap);
                                    cmd2.Parameters.AddWithValue("@dias", registro.dias_disponibles);
                                    cmd2.Parameters.AddWithValue("@dias_disponibles", registro.dias_disponibles - dias_gozados);
                                    cmd2.Parameters.AddWithValue("@ingreso", registro.fecha_ingreso_uen);

                                    cmd2.ExecuteNonQuery();
                                }
                                else
                                {
                                    SqlCommand cmd2 = new SqlCommand("INSERT INTO registros_dias(idsap,periodo,registro_padre,dias,disponibles,caducidad) VALUES (@idsap,@periodo,0,@dias,@dias_disponibles,DATEADD(month, 13, Convert(date, CONCAT(@periodo, '-', (datepart(mm, @ingreso)), '-', datepart(dd, @ingreso)))));", conn);
                                    cmd2.Parameters.AddWithValue("@periodo", periodo);
                                    cmd2.Parameters.AddWithValue("@idsap", registro.idsap);
                                    cmd2.Parameters.AddWithValue("@dias", registro.dias_disponibles);
                                    cmd2.Parameters.AddWithValue("@dias_disponibles", registro.dias_disponibles - registro.dias_gozados);
                                    cmd2.Parameters.AddWithValue("@ingreso", registro.fecha_ingreso_uen);

                                    cmd2.ExecuteNonQuery();
                                }

                                insertados++;
                            }
                            else
                            {
                                fallo++;
                            }
                        }

                        else
                        {
                            var bandera = 0;

                            if (registro.estatus == 2)
                            {
                                estatus = 2;
                            }
                            else 
                            {
                                estatus = 1;
                            }
                            SqlCommand cmd;
                            //Se actualiza el empleado
                            cmd = new SqlCommand("UPDATE empleados SET email=@email,area=@area,banda=@banda,fecha_ingreso_grupo=@fecha_ingreso_grupo,fecha_ingreso_uen=fecha_ingreso_uen, "+
                                "dias_disponibles=@dias_disponibles,idsap_padre=@idsap_padre,nombre_line=@nombre_line,email_line=@email_line,esquema=@esquema,rol=@rol,estatus=@estatus,rfc=@rfc " +
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
                            cmd.Parameters.AddWithValue("@esquema", registro.esquema);
                            cmd.Parameters.AddWithValue("@rol", registro.rol);
                            cmd.Parameters.AddWithValue("@estatus", estatus);
                            cmd.Parameters.AddWithValue("@rfc", registro.rfc);

                            if (esquema != registro.esquema)//Hay un cambio de esquema
                            {
                                bandera = 1;
                            }
                            
                            int n = cmd.ExecuteNonQuery();
                            if (n != 0) // Se actualizo el empleado
                            {
                                if (idsap_padre != registro.idsap_padre) //Hay cambio de line manager
                                {

                                    SqlCommand cmd2 = new SqlCommand("UPDATE solicitudes SET idsap_aprobador = @idsap_aprobador, nombre_aprobador = @nombre_aprobador, email_aprobador = @email_aprobador WHERE idsap = @idsap and estatus = 0 and fecha_inicio >= getdate();", conn);
                                    cmd2.Parameters.AddWithValue("@idsap_aprobador", registro.idsap_padre);
                                    cmd2.Parameters.AddWithValue("@nombre_aprobador", registro.nombre_line);
                                    cmd2.Parameters.AddWithValue("@email_aprobador", registro.email_line);
                                    cmd2.Parameters.AddWithValue("@idsap", registro.idsap);

                                    int m = cmd2.ExecuteNonQuery();

                                }

                                if (bandera == 0)
                                {
                                    log.idsap = registro.idsap;
                                    log.log = "Se actualizo el empleado: " + registro.idsap + " mediante carga de empleados";
                                    log.idsap_creacion = 101010;
                                    log.createLog();
                                }
                                else
                                {

                                    log.idsap = registro.idsap;
                                    log.log = "Se actualizo el empleado: " + registro.idsap + " y hay un cambio de esquema, mediante carga de empleados ";
                                    log.idsap_creacion = 101010;
                                    log.createLog();
                                }
                               

                                actualizados++;
                            }
                            else
                                fallo++;
                        }
                    }

                    //actualiza line managers

                    SqlCommand cmd5 = new SqlCommand("UPDATE empleados SET tipo = 'L' WHERE tipo = 'S' AND idsap IN(SELECT idsap_padre FROM empleados);", conn);
                    cmd5.ExecuteNonQuery();

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

        public string SeparadorArchivo()
        {
            using (var reader = new StreamReader("con.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.Delimiter = ";";
                csv.Read();
                csv.ReadHeader();
                csv.Read();
                try
                {
                    var aux = csv.GetField("SAP"); 
                }
                catch(Exception ex)
                {
                    return ",";
                }

                return ";";
            }
        }

        public List<Empleados> LeeArchivo()
        {

            using (var reader = new StreamReader("con.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = new List<Empleados>();
                csv.Configuration.Delimiter = SeparadorArchivo();
               
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    
                        Empleados record;
                        var ultimo = csv.GetField("ULTIMO DESCONECTE");


                        var ingreso_grupo = csv.GetField("FECHA INGRESO GRUPO");
                        if (ingreso_grupo != "")
                        {
                            DateTime ingreso_g;

                            if (ingreso_grupo.Length > 8)
                            {
                                ingreso_g = DateTime.ParseExact(ingreso_grupo, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            }
                            else
                            {
                                ingreso_g = DateTime.ParseExact(ingreso_grupo, "dd/MM/yy", CultureInfo.InvariantCulture);
                            }

                            var ingreso_uen = csv.GetField("INGRESO A LA UEN");
                            DateTime ingreso_u;

                            if (ingreso_uen.Length > 8)
                            {
                                ingreso_u = DateTime.ParseExact(ingreso_uen, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            }
                            else
                            {
                                ingreso_u = DateTime.ParseExact(ingreso_uen, "dd/MM/yy", CultureInfo.InvariantCulture);
                            }

                            

                            int d_idsap = csv.GetField<int>("SAP");
                            string d_nombre = csv.GetField("NOMBRE");
                            string d_rfc = csv.GetField("RFC");
                            string d_area = csv.GetField("AREA");
                            string d_banda = csv.GetField("PUESTO");
                            DateTime d_fecha_ingreso_grupo = ingreso_g;
                            DateTime d_fecha_ingreso_uen = ingreso_u;
                            int d_dias_disponibles = csv.GetField<int>("VACACIONES");
                            int d_dias_gozados = csv.GetField<int>("DIAS GOZADOS");
                            string d_email = csv.GetField("CORREO COLABORADOR");
                            int d_idsap_padre = csv.GetField<int>("SAP_LINE");
                            string d_nombre_line = csv.GetField("LINE MANAGER");
                            string d_email_line = csv.GetField("CORREO LINE");
                            int d_esquema = csv.GetField<int>("ESQUEMA");
                            string d_rol = csv.GetField("ROL");
                            int d_estatus = csv.GetField<int>("ESTATUS");

                            string previo = csv.GetField("PREVIO");

                           
                            if (ultimo != "-")
                            {
                                DateTime d_ultimo;
                                if (ultimo.Length > 8)
                                {
                                    d_ultimo = DateTime.ParseExact(ultimo, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                }
                                else
                                {
                                    d_ultimo = DateTime.ParseExact(ultimo, "dd/MM/yy", CultureInfo.InvariantCulture);
                                }

                                record = new Empleados
                                {
                                    idsap = d_idsap,
                                    nombre = d_nombre,
                                    area = d_area,
                                    banda = d_banda,
                                    fecha_ingreso_grupo = d_fecha_ingreso_grupo,
                                    fecha_ingreso_uen = d_fecha_ingreso_uen,
                                    dias_disponibles = d_dias_disponibles,
                                    dias_gozados = d_dias_gozados,
                                    email = d_email,
                                    idsap_padre = d_idsap_padre,
                                    nombre_line = d_nombre_line,
                                    email_line = d_email_line,
                                    esquema = d_esquema,
                                    rol = d_rol,
                                    estatus = d_estatus,
                                    ultimo_desconecte = d_ultimo,
                                    rfc =d_rfc
                                };
                            }
                            else
                            {
                                record = new Empleados
                                {
                                    idsap = d_idsap,
                                    nombre = d_nombre,
                                    area = d_area,
                                    banda = d_banda,
                                    fecha_ingreso_grupo = d_fecha_ingreso_grupo,
                                    fecha_ingreso_uen = d_fecha_ingreso_uen,
                                    dias_disponibles = d_dias_disponibles,
                                    dias_gozados = d_dias_gozados,
                                    email = d_email,
                                    idsap_padre = d_idsap_padre,
                                    nombre_line = d_nombre_line,
                                    email_line = d_email_line,
                                    esquema = d_esquema,
                                    rol = d_rol,
                                    estatus = d_estatus,
                                    ultimo_desconecte = null,
                                    rfc = d_rfc
                                };
                            }


                            record.previo = (previo == "") ? 0 : Convert.ToInt32(previo);

                            records.Add(record);
                        }
                   
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
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
