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
    public class LinkRatingRatingController : ControllerBase
    {
        private readonly ILinkRatingRepository repository;

        public LinkRatingRatingController(ILinkRatingRepository repository)
        {
            this.repository = repository;
        }

        // GET: LinkRating/5
        [HttpGet]
        public ActionResult Get(int id)
        {
            var result = repository.GetLinkRatingByID(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        // PUT: LinkRating/
        [HttpPut]
        [Authorize]
        public ActionResult Put([FromBody] LinkRating linkRating)
        {
            try
            {
                repository.InsertLinkRating(linkRating);
                repository.Save();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        // DELETE: LinkRating/5
        [HttpDelete]
        [Authorize]
        public ActionResult Delete(int id)
        {
            repository.DeleteLinkRating(id);
            repository.Save();
            return Ok();
        }
    }
}
