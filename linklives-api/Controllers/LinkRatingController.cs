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
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(LinkRating), 200)]
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
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResultkey Delete(int id)
        {
            repository.Delete(id);
            repository.Save();
            return Ok();
        }
    }
}
