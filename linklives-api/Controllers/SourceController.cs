using linklives_api_dal.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace linklives_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SourceController : ControllerBase
    {
        private readonly ISourceRepository repository;

        public SourceController(ISourceRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost("~/sources")]
        public IActionResult GetAll()
        {
            return Ok(repository.GetAll());
        }
    }
}
