using linklives_api_dal.domain;
using linklives_api_dal.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace linkRatinglives_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LinkRatingController : ControllerBase
    {
        private readonly ILinkRatingRepository repository;

        public LinkRatingController(ILinkRatingRepository repository)
        {
            this.repository = repository;
        }

        // GET: LinkRating/5
        [HttpGet("{key}")]
        [ProducesResponseType(typeof(LinkRating), 200)]
        [ProducesResponseType(404)]
        public ActionResult Get(string key)
        {
            var result = repository.GetByKey(key);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [HttpGet("~/Link/{key}/Ratings")]
        [ProducesResponseType(typeof(List<LinkRating>), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetByLinkKey(string key)
        {
            var result = repository.GetbyLinkKey(key);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        // POST: LinkRating/
        [HttpPost]
        [Authorize]
        public ActionResult Post([FromBody] LinkRating linkRating)
        {
            try
            {
                repository.Insert(linkRating);
                repository.Save();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // DELETE: LinkRating/5
        [HttpDelete("{key}")]
        [Authorize]
        public ActionResult Delete(string key)
        {
            repository.Delete(key);
            repository.Save();
            return Ok();
        }
    }
}
