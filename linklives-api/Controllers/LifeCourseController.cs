﻿using linklives_api_dal.domain;
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

        public LifeCourseController(ILifeCourseRepository repository)
        {
            this.repository = repository;
        }
        // GET: LifeCourse/5
        [HttpGet]
        public ActionResult Get(string id)
        {
            var result = repository.GetByKey(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        // PUT: LifeCourse/
        [HttpPut]
        [Authorize]
        public ActionResult Put([FromBody]LifeCourse lifeCourse)
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
        [Authorize]
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
        [HttpDelete]
        [Authorize]
        public ActionResult Delete(string id)
        {
            repository.Delete(id);
            repository.Save();
            return Ok();
        }
    }
}
