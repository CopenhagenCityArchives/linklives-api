﻿using linklives_api_dal.domain;
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
        [ProducesResponseType(typeof(LinkRating), 200)]
        [ProducesResponseType(404)]
        public ActionResult Get(string id)
        {
            var result = repository.GetByKey(id);
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
        public ActionResult Delete(string id)
        {
            repository.Delete(id);
            repository.Save();
            return Ok();
        }
    }
}
