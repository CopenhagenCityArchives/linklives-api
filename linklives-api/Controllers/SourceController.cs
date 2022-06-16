using Linklives.Domain;
using Linklives.DAL;
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

        //TODO: Is this endpoint used?
        [HttpGet("~/sources")]
        [ResponseCache(CacheProfileName = "StaticLinkLivesData")]
        [ProducesResponseType(typeof(List<Source>),200)]
        public IActionResult GetAll()
        {
            return Ok(repository.GetAll());
        }
    }
}
