using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using desconectate.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace desconectate.Controllers
{
    public class AjusteEmpleadoController : Controller
    {

        private readonly IConfiguration _configuration;

        public AjusteEmpleadoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Opcion> lst = new List<Opcion>();

            ViewBag.tipo = HttpContext.Session.GetString("tipo");

            string connString = _configuration.GetConnectionString("MyConnection"); // Read the connection string from the web.config file
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT rol,descripcion FROM croles ", conn);

                SqlDataReader sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    lst.Add(new Opcion { value = sqlReader.GetInt32(0), desc = sqlReader[1].ToString() });
                }

                ViewBag.Roles = lst;

                sqlReader.Close();

                return View();
            }
        }

        public class Opcion
        {
            public int value { get; set; }
            public string desc { get; set; }
        }


        public List<Empleados> Buscar(string valor)
        {

            List<Empleados> lst = new List<Empleados>();

            string connString = _configuration.GetConnectionString("MyConnection");

            using (SqlConnection conn = new SqlConnection(connString))
            {

                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT idsap,nombre,area,banda,email,nombre_line,esquema,estatus FROM empleados WHERE tipo IN ('S','L','D') and (idsap like '%"+ valor+ "%' OR nombre like '%" + valor + "%' OR area like '%" + valor + "%' OR banda like '%" + valor + "%' OR nombre_line like '%" + valor + "%');", conn);
               
                SqlDataReader sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    lst.Add(new Empleados { idsap = sqlReader.GetInt32(0), nombre = sqlReader[1].ToString(), area = sqlReader[2].ToString(), banda = sqlReader[3].ToString(), email = sqlReader[4].ToString(), nombre_line = sqlReader[5].ToString(), esquema = sqlReader.GetInt32(6), estatus = sqlReader.GetInt32(7) });
                }

                conn.Close();
                return lst;
            }
        }

        public int EditarInfo(Empleados registro)
        {
            string connString = _configuration.GetConnectionString("MyConnection");

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE empleados SET nombre = @nombre,area = @area,banda = @banda,email = @email,fecha_ingreso_grupo = @ingreso_grupo, fecha_ingreso_uen = @ingreso_uen," +
                                                "nombre_line = @nombre_line,idsap_padre = @idsap_padre,email_line = @email_line,esquema = @esquema, rol = @rol, estatus = @estatus WHERE idsap = @idsap;", conn);
                cmd.Parameters.AddWithValue("@nombre", registro.nombre);
                cmd.Parameters.AddWithValue("@area", registro.area);
                cmd.Parameters.AddWithValue("@banda", registro.banda);
                cmd.Parameters.AddWithValue("@email", registro.email);
                cmd.Parameters.AddWithValue("@ingreso_grupo", Convert.ToDateTime(registro.fecha_ingreso_grupo));
                cmd.Parameters.AddWithValue("@ingreso_uen", Convert.ToDateTime(registro.fecha_ingreso_uen));
                cmd.Parameters.AddWithValue("@nombre_line", registro.nombre_line);
                cmd.Parameters.AddWithValue("@idsap_padre", registro.idsap_padre);
                cmd.Parameters.AddWithValue("@email_line", registro.email_line);
                cmd.Parameters.AddWithValue("@esquema", registro.esquema);
                cmd.Parameters.AddWithValue("@rol", registro.rol);
                cmd.Parameters.AddWithValue("@estatus", registro.estatus);
                cmd.Parameters.AddWithValue("@idsap", registro.idsap);
                

                cmd.ExecuteNonQuery();

                conn.Close();
                return 1;
            }
        }

        public int EditarRegistro(RegistroDias register)
        {
            string connString = _configuration.GetConnectionString("MyConnection");

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE registros_dias SET caducidad = @caducidad, dias = @dias,disponibles = @disponibles WHERE registro = @registro;", conn);
                cmd.Parameters.AddWithValue("@dias", register.dias);
                cmd.Parameters.AddWithValue("@caducidad", register.caducidad);

                cmd.Parameters.AddWithValue("@disponibles", register.disponibles);
                cmd.Parameters.AddWithValue("@registro", register.registro);

                cmd.ExecuteNonQuery();

                conn.Close();

                return 1;
            }
        }

        public int logCambios(string idsap,string cambios)
        {
            string connString = _configuration.GetConnectionString("MyConnection");
            string usuario = HttpContext.Session.GetString("usuario");

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd2 = new SqlCommand("INSERT INTO log_cambios(usuario,idsap,fecha,cambios) VALUES (@usuario,@idsap,getdate(),@cambios);", con);
                cmd2.Parameters.AddWithValue("@usuario", usuario);
                cmd2.Parameters.AddWithValue("@idsap", idsap);
                cmd2.Parameters.AddWithValue("@cambios", cambios);

                cmd2.ExecuteNonQuery();

                con.Close();

                return 1;
            }
        }


        public class RegistroDias
        {
            public string registro { get; set; }
            public string generado { get; set; }
            public string periodo { get; set; }
            public string caducidad { get; set; }
            public string dias { get; set; }
            public string disponibles { get; set; }
        }

        public List<RegistroDias> solicitudRegistros(string valor)
        {
            List<RegistroDias> registros = new List<RegistroDias>();

            string connString = _configuration.GetConnectionString("MyConnection");

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT registro,FORMAT(caducidad,'D','en-gb'),FORMAT(fecha_creacion,'D','en-gb'),dias,disponibles,periodo FROM registros_dias WHERE registro_padre = 0 and idsap = @valor and caducidad >= getdate();", conn);
                cmd.Parameters.AddWithValue("@valor", valor);
                SqlDataReader sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    registros.Add(new RegistroDias { registro = sqlReader[0].ToString(),caducidad = sqlReader[1].ToString(), generado = sqlReader[2].ToString(), dias = sqlReader[3].ToString(), disponibles = sqlReader[4].ToString(), periodo = sqlReader[5].ToString() });
                }

                return registros;
            }
        }

        public RegistroDias solicitudInfoRegistro(string valor)
        {
            RegistroDias registro = new RegistroDias();

            string connString = _configuration.GetConnectionString("MyConnection");

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT registro,FORMAT(caducidad,'D','en-gb'),periodo,dias,disponibles FROM registros_dias WHERE registro = @valor;", conn);
                cmd.Parameters.AddWithValue("@valor", valor);
                SqlDataReader sqlReader = cmd.ExecuteReader();

                sqlReader.Read();

                registro.registro = sqlReader[0].ToString();
                registro.caducidad = sqlReader[1].ToString();
                registro.periodo = sqlReader[2].ToString();
                registro.dias = sqlReader[3].ToString();
                registro.disponibles = sqlReader[4].ToString();
               
                return registro;
            }
        }


        public Empleados solicitudInfo(string valor)
        {
            Empleados registro = new Empleados();

            string connString = _configuration.GetConnectionString("MyConnection");

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT e.idsap,e.nombre,e.area,e.banda,e.email,e.fecha_ingreso_grupo,e.fecha_ingreso_uen,e.idsap_padre,e.nombre_line,e.email_line,e.pais,e.esquema,e.rol,DATEDIFF(month,e.fecha_ingreso_grupo,GETDATE()) as antiguedad,CASE WHEN e.esquema = '0' THEN 'DIBLO' WHEN e.esquema = '1' THEN 'EDM' WHEN e.esquema = '2' THEN 'EDM Corporativo' END as nombre_esquema,cr.descripcion as nombre_rol,e.estatus  FROM empleados e LEFT JOIN croles cr ON e.rol = cr.rol WHERE e.idsap = @valor ;", conn);
                cmd.Parameters.AddWithValue("@valor", valor);
                SqlDataReader sqlReader = cmd.ExecuteReader();

                sqlReader.Read();

                registro.idsap = sqlReader.GetInt32(0);
                registro.nombre = sqlReader[1].ToString();
                registro.area = sqlReader[2].ToString();
                registro.banda = sqlReader[3].ToString();
                registro.email = sqlReader[4].ToString();
                registro.fecha_ingreso_grupo = Convert.ToDateTime(sqlReader.IsDBNull(5) ? null : sqlReader[5]);
                registro.fecha_ingreso_uen = Convert.ToDateTime(sqlReader.IsDBNull(6) ? null : sqlReader[6]);
                registro.nombre_line = sqlReader[8].ToString();
                registro.idsap_padre = sqlReader.GetInt32(7);
                registro.email_line = sqlReader[9].ToString();
                registro.pais = sqlReader[10].ToString();
                registro.esquema = sqlReader.GetInt32(11);
                registro.rol = sqlReader[12].ToString();
                registro.antiguedad = sqlReader.GetInt32(13);
                registro.nombre_esquema = sqlReader[14].ToString();
                registro.nombre_rol = sqlReader[15].ToString();
                registro.estatus = sqlReader.GetInt32(16);

                return registro;
            }
        }

        public int CambioLine(int idsap)
        {

            return 1;
        }

        public int BajaEmpleado(int idsap)
        {
            string connString = _configuration.GetConnectionString("MyConnection");

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE registros_dias SET vigencia = getdate(), disponibles = 0 WHERE registro_padre = 0 and idsap = @idsap and vigencia >= getdate() ;", conn);
                cmd.Parameters.AddWithValue("@idsap", idsap);

                cmd.ExecuteNonQuery();

                conn.Close();

                return 1;
            }
        }

        public int AltaEmpleado(int idsap)
        {
            string connString = _configuration.GetConnectionString("MyConnection");

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("", conn);
                cmd.Parameters.AddWithValue("@idsap", idsap);

                cmd.ExecuteNonQuery();

                conn.Close();

                return 1;
            }
        }

    }

}



