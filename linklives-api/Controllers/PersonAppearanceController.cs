using linklives_api_dal.domain;
using linklives_api_dal.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace linklives_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonAppearanceController : ControllerBase
    {
        private readonly IPersonAppearanceRepository repository;

        public PersonAppearanceController(IPersonAppearanceRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("{id}")]
        [HttpGet("{key}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult Get(int id)
        {
            var result = repository.GetRawJsonById(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
    }
}
