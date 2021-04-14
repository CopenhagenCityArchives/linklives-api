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
    public class LinkController : ControllerBase
    {
        private readonly ILinkRepository repository;

        public LinkController(ILinkRepository repository)
        {
            this.repository = repository;
        }
        // GET: Link/5
        [HttpGet]
        public ActionResult Get(string id)
        {
            var result = repository.GetLinkByID(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        // PUT: Link/
        [HttpPut]
        [Authorize]
        public ActionResult Put([FromBody] Link link)
        {
            try
            {
                repository.InsertLink(link);
                repository.Save();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT: Link/BulkInsert
        [HttpPut("BulkInsert")]
        [Authorize]
        public ActionResult BulkInsert([FromBody] IEnumerable<Link> links)
        {
            try
            {
                repository.InsertLinks(links);
                repository.Save();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        // DELETE: Link/5
        [HttpDelete]
        [Authorize]
        public ActionResult Delete(string id)
        {
            repository.DeleteLink(id);
            repository.Save();
            return Ok();
        }
    }
}
