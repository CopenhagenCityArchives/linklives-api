using linklives_api_dal.domain;
using linklives_api_dal.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

        [HttpGet("~/sources")]
        [ProducesResponseType(typeof(List<dynamic>),200)]
        public IActionResult GetAll()
        {
            return Ok(repository.GetAll());
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<dynamic>), 200)]
        public IActionResult Get(int id)
        {
            return Ok(repository.GetById(id));
        }
    }
}
