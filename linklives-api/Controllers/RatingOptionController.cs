using linklives_api_dal.domain;
using linklives_api_dal.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace linklives_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatingOptionController : Controller
    {
        private readonly IRatingOptionRepository repository;

        public RatingOptionController(IRatingOptionRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet()]
        [ProducesResponseType(typeof(List<RatingOption>), 200)]
        public IActionResult Get()
        {
            return Ok(repository.GetAll());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RatingOption), 200)]
        [ProducesResponseType(404)]
        public ActionResult Get(int id)
        {
            var result = repository.GetById(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
    }
}
