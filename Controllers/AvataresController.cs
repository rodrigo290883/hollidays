using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace desconectate.Controllers
{
    public class AvataresController : Controller
    {

        private readonly IConfiguration _configuration;


        public AvataresController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CambiaAvatar(string avatar)
        {
            var id_sap = HttpContext.Session.GetString("usuario"); 
            string connString = _configuration.GetConnectionString("MyConnection"); // Read the connection string from the web.config file
            using (SqlConnection conn = new SqlConnection(connString))
            {
                int folio = 0;
                conn.Open();
                SqlCommand cmd = new SqlCommand("Update empleados Set avatar = @avatar Where IdSAP = @IdSAP ", conn);
                cmd.Parameters.AddWithValue("@IdSAP", id_sap);
                cmd.Parameters.AddWithValue("@avatar", avatar);

                if (cmd.ExecuteNonQuery()!=0)
                {
                    conn.Close();
                    return Content("1");
                }
                else
                {
                    conn.Close();
                    return Content("No se pudo guardar el avatar, intentalo nuevamente");
                }
            }
        }
    }
}
