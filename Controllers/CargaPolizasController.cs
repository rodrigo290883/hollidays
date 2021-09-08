using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;
using Microsoft.AspNetCore.Http;
using System.IO;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace desconectate.Controllers
{
    public class CargaPolizasController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            string usuario = HttpContext.Session.GetString("usuario");

            if (usuario != null)
            {
                ViewBag.tipo = HttpContext.Session.GetString("tipo");
                return View();
            }
            else
                return RedirectToAction("Index", "Home");
        }

        public IActionResult SubeArchivo(List<IFormFile> files)
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
                        var filePath = "temp.zip"; //we are using Temp file name just for the example. Add your own file path.
                        filePaths.Add(filePath);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            formFile.CopyTo(stream);
                        }
                    }
                }

                var extractPath = @"./Polizas";

                extractPath = Path.GetFullPath(extractPath);

                //ZipFile.ExtractToDirectory("temp.zip", extractPath,true);

                using (ZipArchive archive = ZipFile.OpenRead("temp.zip"))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        if (entry.FullName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                        {
                            var destino_path = Path.Combine(extractPath, entry.FullName);
                            
                            entry.ExtractToFile(destino_path,true);
                        }
                    }
                }


                return Content("1");
            }
            catch (Exception ex)
            {
                return Content(Convert.ToString(ex.Message));
            }
        }
    }
}
