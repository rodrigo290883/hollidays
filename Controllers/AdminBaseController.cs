using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace desconectate.Controllers
{
    public class AdminBaseController : Controller
    {
        private readonly IConfiguration _configuration;


        public AdminBaseController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EjecutarConsulta(int num)
        {
            string fileName = @"temp.sql";

            using (StreamReader streamReader = new StreamReader(fileName, Encoding.UTF8, true))
            {
                string text = streamReader.ReadToEnd();
                string[] lista = text.Split(";", StringSplitOptions.RemoveEmptyEntries);

                string connString = _configuration.GetConnectionString("MyConnection"); // Read the connection string from the web.config file
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(lista[num], conn);
                    try{
                        cmd.ExecuteNonQuery();
                        return Content("1");
                    }
                    catch(SqlException ex)
                    {
                        return Content(ex.ToString());
                    }
                }
            }
            
        }

        public string[] LeeArchivo()
        {

            string fileName = @"temp.sql";

            using (StreamReader streamReader = new StreamReader(fileName, Encoding.UTF8, true))
            {
                string text = streamReader.ReadToEnd();


                string[] lista = text.Split(";", StringSplitOptions.RemoveEmptyEntries);

                return lista;
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
                        var filePath = "temp.sql"; //we are using Temp file name just for the example. Add your own file path.
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
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
