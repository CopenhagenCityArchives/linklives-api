﻿
using Linklives.DAL;
using Linklives.Domain;
using Linklives.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace linklives_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LifeCourseController : ControllerBase
    {
        private readonly IEFLifeCourseRepository repository;
        private readonly IKeyedRepository<LifeCourse> esRepository;
        private readonly IPersonAppearanceRepository pa_repo;
        private readonly ISourceRepository source_repo;
        private readonly IEFDownloadHistoryRepository downloadHistoryRepository;

        public LifeCourseController(
            IEFLifeCourseRepository repository,
            IKeyedRepository<LifeCourse> esRepository,
            IPersonAppearanceRepository pa_repo,
            ISourceRepository source_repo,
            IEFDownloadHistoryRepository downloadHistoryRepository
        )
        {
            this.repository = repository;
            this.esRepository = esRepository;
            this.pa_repo = pa_repo;
            this.source_repo = source_repo;
            this.downloadHistoryRepository = downloadHistoryRepository;
        }
        // GET: LifeCourse/5
        [HttpGet("{key}")]
        [ResponseCache(CacheProfileName = "StaticLinkLivesData")]
        [ProducesResponseType(typeof(LifeCourse),200)]
        [ProducesResponseType(typeof(LifeCourse), 206)]
        [ProducesResponseType(404)]
        public ActionResult Get(string key)
        {
            var result = repository.GetByKey(key);
            if (result != null)
            {
                try
                {
                    GetPAsLinksAndLinkRatings(result);
                }
                catch (Exception e)
                {
                    System.Console.WriteLine($"Failed to load links and link ratings for lifecourse - returning limited data {e}");
                    result.PersonAppearances = new List<BasePA>();
                    //If for some reason we fail to get the person appearance data we return what we have with http 206 to indicate partial content
                    return StatusCode(206, result);
                }

                return Ok(result);
            }
            return NotFound();
        }

        // GET: LifeCourse/5/download.xlsx
        [HttpPost("{key}/download.{format}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(206)]
        [ProducesResponseType(404)]
        [Authorize]
        public ActionResult Download(string key, string format)
        {
            var encoder = Encoder.ForFormat(format);
            if (encoder == null) {
                return NotFound("No formatter for that format exists.");
            }

            var lifecourse = esRepository.GetByKey(key);
            if (lifecourse == null)
            {
                return NotFound("No lifecourse with that key exists");
            }

            GetPAsLinksAndLinkRatings(lifecourse);

            var linksRows = lifecourse.Links.Select((link) => {
                return new Dictionary<string, (string, Exportable)> {
                    ["life_course_id"] = (lifecourse.Life_course_id.ToString(), new Exportable(FieldCategory.Identification)),
                    ["link_id"] = (link.Link_id, new Exportable(FieldCategory.Identification)),
                    ["pa_id1"] = (link.Pa_id1.ToString(), new Exportable(FieldCategory.Identification, extraWeight: 1)),
                    ["pa_id2"] = (link.Pa_id2.ToString(), new Exportable(FieldCategory.Identification, extraWeight: 2)),
                    ["method_id"] = (link.Method_id, new Exportable(extraWeight: 3)),
                    ["score"] = (link.Score, new Exportable(extraWeight: 4)),
                };
            }).ToArray();

            var result = encoder.Encode(new Dictionary<string, Dictionary<string, (string, Exportable)>[]>{
                ["Lifecourse"] = SpreadsheetSerializer.Serialize(lifecourse),
                ["Links"] = linksRows,
            });

            downloadHistoryRepository.RegisterDownload(new DownloadHistoryEntry(
                DownloadType.Lifecourse,
                key,
                User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value
            ));
            downloadHistoryRepository.Save();

            return File(result, encoder.ContentType, $"lifecourse.{format}");
        }

        [HttpGet("~/user/ratings/lifecourses")]
        [ResponseCache(CacheProfileName = "UserRatings")]
        [ProducesResponseType(typeof(LifeCourse), 200)]
        [ProducesResponseType(404)]
        [Authorize]
        public ActionResult GetByUserid()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            
            var keys = repository.GetKeysByUserId(userId);

            var result = esRepository.GetByKeys(keys.ToList());

            if (result == null)
            {
                return NotFound();
            }

            bool anyFailed = false;
            foreach (var lc in result)
            {
                try
                {
                    GetPAsLinksAndLinkRatings(lc);
                }
                catch (Exception e)
                {
                    anyFailed = true;
                    System.Console.WriteLine($"Failed to load links and link ratings for a lifecourse: {e}");
                }
            }
            //If for some reason we fail to get the person appearance data we return what we have with http 206 to indicate partial content
            if(anyFailed) {
                return StatusCode(206, result);
            }
            return Ok(result);
        }

        // POST: LifeCourse/
        [HttpPost]
        [Authorize]
        public ActionResult Post([FromBody]LifeCourse lifeCourse)
        {
            try
            {
                repository.Insert(lifeCourse);
                repository.Save();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // PUT: LifeCourse/BulkInsert
        [HttpPut("BulkInsert")]
        //[Authorize]
        public ActionResult BulkInsert([FromBody]IEnumerable<LifeCourse> lifeCourses)
        {
            try
            {
                repository.Upsert(lifeCourses
                    .GroupBy(lc => lc.Key)
                    .Select(g => g.First())); //Filter out duplicate keys before inserting
                repository.Save();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        // DELETE: LifeCourse/5
        [HttpDelete("{key}")]
        [Authorize]
        public ActionResult Delete(string key)
        {
            repository.Delete(key);
            repository.Save();
            return Ok();
        }
        private void GetPAsLinksAndLinkRatings(LifeCourse lifecourse)
        {
            //Fetch person apperances and add them to the lifecourse
            repository.GetLinksAndRatings(lifecourse);
            lifecourse.PersonAppearances = pa_repo.GetByIds(lifecourse.Links.SelectMany(l => new[] { $"{l.Source_id1}-{l.Pa_id1}", $"{l.Source_id2}-{l.Pa_id2}" }).Distinct().ToList()).ToList();
        }
    }
}
