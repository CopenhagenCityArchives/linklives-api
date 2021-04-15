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
        [HttpGet]
        public ActionResult Get(int id)
        {
            var result = repository.GetById(id);
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
                repository.Insert(linkRating);
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
            repository.Insert(id);
            repository.Save();
            return Ok();
        }
    }
}
