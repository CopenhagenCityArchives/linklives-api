using linklives_api_dal.domain;
using linklives_api_dal.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace linkRatinglives_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LinkRatingController : ControllerBase
    {
        private readonly ILinkRatingRepository repository;
        private readonly IRatingOptionRepository ratingOptionRepository;

        public LinkRatingController(ILinkRatingRepository repository, IRatingOptionRepository ratingOptionRepository)
        {
            this.repository = repository;
            this.ratingOptionRepository = ratingOptionRepository;
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
        [HttpGet("~/Link/{key}/Ratings/stats")]
        [ProducesResponseType(typeof(List<LinkRatingStats>), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetLinkRatingStats(string key)
        {
            var ratings = repository.GetbyLinkKey(key);
            var result = CalculateHeadingStats(ratings);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        // POST: LinkRating/
        [HttpPost]
        [Authorize]
        public ActionResult Post([FromBody] PostLinkRating linkRating)
        {
            try
            {
                repository.Insert(linkRating.ToLinkRating(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value));
                repository.Save();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500,e.Message);
            }
        }
        // DELETE: LinkRating/5
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult Delete(int id)
        {
            repository.Delete(id);
            repository.Save();
            return Ok();
        }

        private LinkRatingStats CalculateHeadingStats(List<LinkRating> linkRatings)
        {
            var headings = ratingOptionRepository.GetAll().Select(ro => ro.Heading).Distinct();
            var headingstats = new LinkRatingStats();
            headingstats.TotalRatings = linkRatings.Count();
            foreach (var heading in headings)
            {
                headingstats.HeadingRatings.Add(heading, linkRatings.Count(lr => lr.Rating.Heading == heading));
            }
            return headingstats;
        }
    }
}
