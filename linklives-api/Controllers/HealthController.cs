using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace linklives_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            var assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName();
            return Ok($"{assemblyName.Name} version {assemblyName.Version} is alive!");
        }
    }
}
