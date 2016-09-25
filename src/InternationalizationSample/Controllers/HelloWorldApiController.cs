using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternationalizationSample.Controllers
{
    [Route("hello-world")]
    public class HelloWorldApiController
    {
        private readonly IStringLocalizer<HelloWorldApiController> localizer;

        public HelloWorldApiController(IStringLocalizer<HelloWorldApiController> localizer)
        {
            this.localizer = localizer;
        }

        [HttpGet]
        public string SayHello()
        {
            return this.localizer["HelloWorld"];
        }
    }
}
