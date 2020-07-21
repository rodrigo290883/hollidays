using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace desconectate.Models
{
    public class LogClass
    {
        public int registro { set; get; }
        public int idsap { set; get; }
        public string log { set; get; }
        public Nullable<System.DateTime> fecha_creacion { set; get; }
        public int idsap_creacion { get; set; }


        private string _connectionString;

        public LogClass(IConfiguration iconfiguration)
        {
            _connectionString = iconfiguration.GetConnectionString("MyConnection");
        }

        public int createLog()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO log_vacaciones(idsap,log,idsap_creacion) VALUES (@idsap,@log,101010) " + "SELECT CAST(scope_identity() AS int) ", con);
                    cmd.Parameters.AddWithValue("@idsap", idsap);
                    cmd.Parameters.AddWithValue("@log", log);

                    con.Open();

                    int reg = (Int32)cmd.ExecuteScalar();

                    con.Close();

                    return reg;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }

        }
    }

}

