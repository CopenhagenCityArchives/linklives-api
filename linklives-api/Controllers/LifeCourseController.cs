﻿using linklives_api_dal.domain;
using linklives_api_dal.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace linklives_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LifeCourseController : ControllerBase
    {
        private readonly ILifeCourseRepository repository;
        private readonly IPersonAppearanceRepository pa_repo;
        private readonly ISourceRepository source_repo;

        public LifeCourseController(ILifeCourseRepository repository, IPersonAppearanceRepository pa_repo, ISourceRepository source_repo)
        {
            this.repository = repository;
            this.pa_repo = pa_repo;
            this.source_repo = source_repo;
        }
        // GET: LifeCourse/5
        [HttpGet("{key}")]
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
                    AddPersonApperances(result);
                }
                catch (Exception e)
                {
                    //If for some reason we fail to get the person appearance data we return what we have with http 206 to indicate partial content
                    return StatusCode(206, result);
                }
               
                return Ok(result);
            }
            return NotFound();
        }
        [HttpGet("~/user/ratings/lifecourses")]
        [ProducesResponseType(typeof(LifeCourse), 200)]
        [ProducesResponseType(404)]
        [Authorize]
        public ActionResult GetByUserid()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = repository.GetByUserRatings(userId);
            if (result != null)
            {
                try
                {
                    foreach (var lc in result)
                    {
                        AddPersonApperances(lc);
                    }
                }
                catch (Exception)
                {
                    //If for some reason we fail to get the person appearance data we return what we have with http 206 to indicate partial content
                    return StatusCode(206, result);
                }
                return Ok(result);
            }
            return NotFound();
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
        private void AddPersonApperances(LifeCourse lifecourse)
        {
            //Fetch person apperances and add them to the lifecourse
            lifecourse.PersonAppearances = pa_repo.GetByIds(lifecourse.Links.SelectMany(l => new[] { $"{l.Source_id1}-{l.Pa_id1}", $"{l.Source_id2}-{l.Pa_id2}" }).Distinct().ToList());
        }
    }
}
