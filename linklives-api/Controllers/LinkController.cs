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
                try
                {
                    result.GetPersonAppearances(pa_repo);
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

        // PUT: Link/
        [HttpPut]
        [Authorize]
        public ActionResult Put([FromBody] Link link)
        {
            try
            {
                repository.Insert(link);
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
                repository.Insert(links);
                repository.Save();
                return Ok();
            }
            catch
            {
                return BadRequest();
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
