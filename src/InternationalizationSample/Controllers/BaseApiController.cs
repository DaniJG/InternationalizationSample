using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternationalizationSample.Controllers
{
    //This class is just an alternative to the ApiPrefixConvention.
    //Every api controller would need to inherit from this class
    //The routes will forcibly need to contain the controller name matching the class name, as these attributes use the [controller] placeholder    

    //[Route("api/{language:regex(^[[a-z]]{{2}}(?:-[[A-Z]]{{2}})?$)}/[controller]")]
    //[Route("api/[controller]")]
    //public class BaseApiController: Controller
    //{
    //}
}
