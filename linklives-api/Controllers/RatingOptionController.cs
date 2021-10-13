using Linklives.Domain;
using Linklives.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

        [Route("~/[controller]s")]
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
