using System;
using System.IO;
using System.Web;  
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using desconectate.Models;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.AspNetCore.Hosting;
using System.Data;
using System.Data.OleDb;
using ExcelDataReader;




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

      /*  public IEnumerable Method1(string fileSrc, string worksheet)
          {   
             //Reading excel data using ExcelDataReader
              var excelData = new ExcelData(fileSrc);
              //Get data from specified worksheet
              var query = excelData.getData(worksheet);
              int cp = 0;
              //intialize the result list
              var list = new List();
              foreach (var line in query)
              {
                      try
                      {
                          var surname = line[0].ToString();//surname
                          var name = line[1].ToString();//name    
                          var date = line[2].ToString();//custom date format
                          list.Add(new Person(surname, name, date));
                      }
                      catch (Exception exception)
                      {
                          ;
                      }
              }
              return list.AsEnumerable();
          }

        [HttpGet]
        public IActionResult SubirArchivo(){
            return View(new CargaEmpleados(Nombre,Edad));
        }
        
        [HttpPost]
        public IActionResult SubirArchivo(CargaEmpleados model){

            var datos = model.archivo;

            var Nombre = model.archivo.FileName;

            var extencion = Path.GetExtension(model.archivo.FileName);

            if (Convert.ToString(extencion) == ".csv"){

                string saveFile = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/Temp",Nombre);

                using(var stream = new FileStream(saveFile,FileMode.Create)){
                    
                    model.archivo.CopyTo(stream);
                }
                
                
                

                IEnumerable list = new List();

                list = Method1(saveFile,"1");

                return View(list);

               string saveFile = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/Temp",Nombre);

                using(var stream = new FileStream(saveFile,FileMode.Create)){
                    
                    model.archivo.CopyTo(stream);
                }


                using(var stream = File.Open(saveFile,FileMode.Open,FileAccess.Read)){
                    using (var reader = ExcelReaderFactory.CreateReader(stream)){
                        do{
                            while(reader.Read()){
                                reader.GetDouble(0);
                            }
                        }while(reader.NextResult());

                    }

                }


            }else{
                return Content("0");
            }

            //return Content(Convert.ToString(extencion));
        }*/
    }
}
