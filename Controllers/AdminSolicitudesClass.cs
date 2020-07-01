using System;
using System.Collections.Generic;
using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using desconectate.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Net;

namespace desconectate.Controllers
{

    public class AdminSolicitudesController : Controller
    {
        private readonly IConfiguration _configuration;

        public AdminSolicitudesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}