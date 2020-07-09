using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace desconectate.Controllers
{
    public class DescargaController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            string path = "test.xlsx";
            WebClient cliente = new WebClient();
            byte[] archivo = cliente.DownloadData(path);

                DateTime hoy = DateTime.Today;
                var contentType = "APPLICATION/octet-stream";
                var fileName = "CONCENTRADO_VACACIONES_"+hoy.ToString("d")+".xlsx";
                return File(archivo, contentType, fileName);
        }
    }
}
