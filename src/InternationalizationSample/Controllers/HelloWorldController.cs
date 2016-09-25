using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternationalizationSample.Controllers
{
    public class HelloWorldController: BaseApiController
    {
        private readonly IStringLocalizer<HelloWorldController> localizer;

        public HelloWorldController(IStringLocalizer<HelloWorldController> localizer)
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
