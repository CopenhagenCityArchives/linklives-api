using linklives_api_dal.domain;
using linklives_api_dal.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace linklives_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LifeCourseController : ControllerBase
    {
        private readonly ILifeCourseRepository repository;
        private readonly IPersonAppearanceRepository pa_repo;

        public LifeCourseController(ILifeCourseRepository repository, IPersonAppearanceRepository pa_repo)
        {
            this.repository = repository;
            this.pa_repo = pa_repo;
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
                    //Go fetch Person Appearance data
                    foreach (var link in result.Links)
                    {
                        link.GetPersonAppearances(pa_repo);
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
            catch
            {
                return BadRequest();
            }
        }

        // PUT: LifeCourse/BulkInsert
        [HttpPut("BulkInsert")]
        //[Authorize]
        public ActionResult BulkInsert([FromBody]IEnumerable<LifeCourse> lifeCourses)
        {
            try
            {
                repository.Insert(lifeCourses);
                repository.Save();
                return Ok();
            }
            catch
            {
                return BadRequest();
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
    }
}
