using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace InternationalizationSample.Controllers
{
    public class HttpErrorController: Controller
    {
        [HttpGet("statuscode/{code}")]
        public IActionResult Index(HttpStatusCode code)
        {
            return View(code);
        }
    }
}
