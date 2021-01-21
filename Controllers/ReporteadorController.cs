using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using desconectate.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

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
            string tipo = HttpContext.Session.GetString("tipo");
            ViewBag.tipo = tipo;
            string connString = _configuration.GetConnectionString("MyConnection"); // Read the connection string from the web.config file
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM creportes WHERE tipo = @tipo;", conn);
                cmd.Parameters.AddWithValue("@tipo", tipo);

                SqlDataReader sqlReader = cmd.ExecuteReader();

                List<Reporte> lst = new List<Reporte>();

                while (sqlReader.Read())
                {
                    lst.Add(new Reporte { id_reporte = sqlReader.GetInt32(0), nombre = sqlReader[1].ToString() });
                }

                ViewBag.reportes = lst;

                return View();
            }
        }

        public IActionResult ConsultaReporte(Reporte reporte)
        {
            DataTable data = new DataTable();

            string connString = _configuration.GetConnectionString("MyConnection"); // Read the connection string from the web.config file
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT nombre,consulta FROM creportes WHERE id_reporte = @id_reporte", conn);
                cmd.Parameters.AddWithValue("@id_reporte", reporte.id_reporte);

                SqlDataReader sqlReader = cmd.ExecuteReader();
                sqlReader.Read();

                var nombre = sqlReader[0].ToString();
                var consulta = sqlReader[1].ToString();

                sqlReader.Close();

                SqlDataAdapter adapter = new SqlDataAdapter(consulta, conn);
                adapter.Fill(data);

                string JSONresult;
                JSONresult = JsonConvert.SerializeObject(data);

                return Content(JSONresult);
            }
        }
    }
}