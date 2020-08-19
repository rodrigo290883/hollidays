using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using desconectate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace desconectate.Controllers
{
    public class ReporteadorController : Controller
    {
        private readonly IConfiguration _configuration;


        public ReporteadorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {

            string connString = _configuration.GetConnectionString("MyConnection"); // Read the connection string from the web.config file
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM creportes", conn);

                SqlDataReader sqlReader = cmd.ExecuteReader();

                List<Reporte> lst = new List<Reporte>();

                while (sqlReader.Read())
                {
                    lst.Add(new Reporte { id_reporte = sqlReader.GetInt32(0), reporte = sqlReader[1].ToString() });
                }

                ViewBag.reportes = lst;

                return View();
            }
        }

        public DataSet ConsultaReporte(Reporte reporte)
        {
            DataSet data = new DataSet();

            string connString = _configuration.GetConnectionString("MyConnection"); // Read the connection string from the web.config file
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT reporte,consulta FROM creportes WHERE id_reporte = @id_reporte", conn);
                cmd.Parameters.AddWithValue("@id_reporte", reporte.id_reporte);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(data);

                return data;
            }
        }
    }
}