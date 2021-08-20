using linklives_api_dal.domain;
using linklives_api_dal.Repositories;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace linklives_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly ElasticClient _esClient;
        private readonly ILifeCourseRepository _lifeCourseRepository;
        private readonly ILinkRepository _linkRepository;
        private readonly ILinkRatingRepository _linkRatingRepository;

        public StatsController(ElasticClient esClient, ILifeCourseRepository lifeCourseRepository, ILinkRepository linkRepository, ILinkRatingRepository linkRatingRepository)
        {
            _esClient = esClient;
            _lifeCourseRepository = lifeCourseRepository;
            _linkRepository = linkRepository;
            _linkRatingRepository = linkRatingRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Stats), 200)]
        public IActionResult Get()
        {
            var result = new Stats
            {
                EsPersonAppearanceCount = _esClient.Count<dynamic>(x => x.Index("pas")).Count,
                EsLifecourseCount = _esClient.Count<LifeCourse>(x => x.Index("lifecourses")).Count,
                EsLinkCount = _esClient.Count<Link>(x => x.Index("links")).Count,
                EsSourceCount = _esClient.Count<Source>(x => x.Index("sources")).Count,
                DbLifecourseCount = _lifeCourseRepository.Count(),
                DbLinkCount = _linkRepository.Count(),
                DbLinkRatingCount = _linkRatingRepository.Count()

            };
            return Ok(result);
        }
    }
}
