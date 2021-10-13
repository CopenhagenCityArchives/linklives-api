using Linklives.Domain;
using Linklives.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace linklives_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LinkController : ControllerBase
    {
        private readonly ILinkRepository repository;
        private readonly IPersonAppearanceRepository pa_repo;
        public LinkController(ILinkRepository repository, IPersonAppearanceRepository pa_repo)
        {
            this.repository = repository;
            this.pa_repo = pa_repo;
        }
        // GET: Link/5
        [HttpGet("{key}")]
        [ProducesResponseType(typeof(Link), 200)]
        [ProducesResponseType(typeof(Link), 206)]
        [ProducesResponseType(404)]
        public ActionResult Get(string key)
        {
            var result = repository.GetByKey(key);
            if (result != null)
            {
                //Go fetch person appearance data                             
                return Ok(result);
            }
            return NotFound();
        }

        // POST: Link/
        [HttpPost]
        [Authorize]
        public ActionResult Post([FromBody] Link link)
        {
            try
            {
                repository.Insert(link);
                repository.Save();
                return Ok();
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // PUT: Link/BulkInsert
        [HttpPut("BulkInsert")]
        //[Authorize]
        public ActionResult BulkInsert([FromBody] IEnumerable<Link> links)
        {
            try
            {
                repository.Upsert(links
                    .GroupBy(l => l.Key)
                    .Select(g => g.First())); //Filter out duplicate keys before inserting
                repository.Save();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        // DELETE: Link/5
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
