using Linklives.DAL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult Get(string id)
        {
            var result = repository.GetById(id).ToString(Formatting.None);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
    }
}
