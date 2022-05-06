using Linklives.Domain;
using Linklives.DAL;
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
        private readonly IEFLifeCourseRepository lifeCourseRepository;

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

        [HttpGet("~/Link/{id}/Ratings")]
        [ProducesResponseType(typeof(List<LinkRating>), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetByLinkId(int id)
        {
            var result = repository.GetbyLinkId(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpGet("~/Link/{id}/Ratings/stats")]
        [ProducesResponseType(typeof(List<LinkRatingStats>), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetLinkRatingStats(int id)
        {
            var ratings = repository.GetbyLinkId(id);
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
        public ActionResult Post([FromBody] PostLinkRating linkRatingData)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

                var ratings = repository.GetbyLinkId(linkRatingData.LinkId);
                var alreadyRated = ratings.Any((rating) => rating.User == userId);

                if (alreadyRated)
                {
                    return Forbid();
                }

                try
                {
                    var lifecourse = lifeCourseRepository.GetByLinkId(linkRatingData.LinkId);

                    if (lifecourse.Is_historic){
                        return Forbid();
                    }
                }
                catch (Exception e)
                {
                    return StatusCode(400, "Could not check if the links' lifecourse is historic: " + e.Message);
                }


                var linkRating = linkRatingData.ToLinkRating(userId);
                repository.Insert(linkRating);
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
