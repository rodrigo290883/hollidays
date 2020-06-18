using System;
using System.IO;
using System.Web;  
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using LinqToExcel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using desconectate.Models;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.AspNetCore.Hosting;
using desconetate.Models;



// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace desconectate.Controllers
{
    public class CargaEmpleadosController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            string usuario = HttpContext.Session.GetString("usuario");
            ViewBag.tipo = HttpContext.Session.GetString("tipo");
            if (usuario != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult SubirArchivo(){
            return View(new CargaEmpleados());
        }
        
        [HttpPost]
        public IActionResult SubirArchivo(CargaEmpleados model){

            var datos = model.archivo;

            var Nombre = Path.GetFileName(model.archivo.FileName);

            var contenido = model.archivo.ContentType;

            return Content(Convert.ToString(Nombre));
        }
    }
}
