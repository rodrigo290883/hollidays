using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mvc_dotnet.Data;
using mvc_dotnet.Models;

namespace mvc_dotnet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public object Session { get; private set; }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Politicas()
        {
            return View();
        }

        public ActionResult Entrar(string usuario, string contrasena)
        {
            try
            {
                
                using (DesconectateContext db = new DesconectateContext())
                {
                    var lista = from d in db.Empleados
                                where d.email == usuario && d.contrasena == contrasena && d.estatus == 1
                                select d;

                    if (lista.Count() > 0)
                    {
                        Session = lista.First();
                        return Content("1");
                    }
                    else
                    {
                        return Content("Datos incorrectos");
                    }
                }
            }
            catch (Exception ex)
            {
                return Content("Ocurrio un error" + ex.Message);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
