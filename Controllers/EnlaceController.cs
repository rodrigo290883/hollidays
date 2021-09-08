using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using desconectate.Models;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace desconectate.Controllers
{
    public class EnlaceController : Controller
    {

        private readonly IConfiguration _configuration;

        public EnlaceController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: /<controller>/
        public List<RegistroEnlace> Info(string idsap,string inicio, string fin)
        {
            List<RegistroEnlace> registros = new List<RegistroEnlace>();

            string connString = _configuration.GetConnectionString("MyConnection"); // Read the connection string from the web.config file
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                SqlCommand select = new SqlCommand("select e.idsap,e.nombre,e.email,rd.dias,s.tipo_solicitud,s.idsap_aprobo,s.dias_detalle,ts.solicitud,s.con_goce,ts.clave_AS400  " +
                                                    "from empleados e "+
                                                    "LEFT JOIN registros_dias rd on e.idsap = rd.idsap and rd.registro_padre != 0 "+
                                                    "LEFT JOIN solicitudes s on rd.folio_solicitud = s.folio "+
                                                    "LEFT JOIN ctipos_solicitud ts on s.tipo_solicitud = ts.id_tipo_solicitud "+
                                                    "WHERE e.idsap = @idsap and (CONVERT(date,s.fecha_inicio ) between @inicio and @fin or CONVERT(date,s.fecha_fin ) between @inicio and @fin);", conn);
                select.Parameters.AddWithValue("@idsap", idsap);
                select.Parameters.AddWithValue("@inicio", inicio);
                select.Parameters.AddWithValue("@fin", fin);

                SqlDataReader sqlReader = select.ExecuteReader();

                while (sqlReader.Read())
                {
                    string data_idsap = sqlReader[0].ToString();
                    string data_nombre = sqlReader[1].ToString();
                    string data_email = sqlReader[2].ToString();
                    int data_dias = sqlReader.GetInt32(3);
                    string data_tipo = sqlReader[7].ToString();
                    string data_aprobador = sqlReader[5].ToString();
                    string data_detalle = sqlReader[6].ToString();
                    string data_clave = sqlReader[9].ToString();

                    if (sqlReader.GetInt32(4) == 1)
                    {
                        if (sqlReader[8].ToString() == "N")
                        {
                            registros.Add(new RegistroEnlace { idsap = data_idsap, nombre = data_nombre, email = data_email, dias = data_dias, tipo_solicitud = data_tipo, idsap_aprobo = data_aprobador, dias_detalle = data_detalle, clave_as400 = "215" });
                        }
                        else
                        {
                            registros.Add(new RegistroEnlace { idsap = data_idsap, nombre = data_nombre, email = data_email, dias = data_dias, tipo_solicitud = data_tipo, idsap_aprobo = data_aprobador, dias_detalle = data_detalle, clave_as400 = "217" });
                        }
                    }
                    else
                    {
                         registros.Add(new RegistroEnlace { idsap = data_idsap, nombre = data_nombre, email = data_email, dias = data_dias, tipo_solicitud = data_tipo, idsap_aprobo = data_aprobador, dias_detalle = sqlReader[6].ToString(), clave_as400 = data_clave });

                    }
                }
                return registros; 
            }
        }

        public class RegistroEnlace
        {
            public string idsap { get; set; }
            public string nombre { get; set; }
            public string email { get; set; }
            public int dias { get; set; }
            public string tipo_solicitud { get; set; }
            public string idsap_aprobo { get; set; }
            public string dias_detalle { get; set; }
            public string clave_as400 { get; set; }

        }
    }
}
