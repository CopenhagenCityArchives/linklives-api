using Linklives.DAL;
using Microsoft.AspNetCore.Mvc;
using Linklives.Domain;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace linklives_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonAppearanceController : ControllerBase
    {
        private readonly IPersonAppearanceRepository repository;
        private readonly ITranscribedPARepository transcribedPARepository;

        public PersonAppearanceController(IPersonAppearanceRepository repository, ITranscribedPARepository transcribedPARepository)
        {
            this.repository = repository;
            this.transcribedPARepository = transcribedPARepository;
        }

        [HttpGet("{id}")]
        [ResponseCache(CacheProfileName = "StaticLinkLivesData")]
        [ProducesResponseType(200)]
        [ProducesResponseType(206)]
        [ProducesResponseType(404)]
        public ActionResult Get(string id)
        {
            var result = repository.GetById(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpPost("{id}/download.{format}")]
        [ResponseCache(CacheProfileName = "StaticLinkLivesData")]
        [ProducesResponseType(200)]
        [ProducesResponseType(206)]
        [ProducesResponseType(404)]
        public ActionResult Download(string id, string format)
        {
            var encoder = Encoder.ForFormat(format);
            if (encoder == null) {
                return NotFound("No formatter for that format exists.");
            }

            var personAppearance = repository.GetById(id);
            if (personAppearance == null)
            {
                return NotFound("No person appearance with that ID exists.");
            }

            var serializable = new RowSerializable[] { personAppearance };
            var rows = SpreadsheetSerializer.Serialize(serializable);
            var result = encoder.Encode(rows);

            return File(result, encoder.ContentType, $"personAppearance.{format}");
        }
    }
}
