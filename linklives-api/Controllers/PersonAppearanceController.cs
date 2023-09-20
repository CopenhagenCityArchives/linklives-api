using Elasticsearch.Net;
using Linklives.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Linklives.Serialization;
using Linklives.Domain;
using Nest;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace linklives_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonAppearanceController : ControllerBase
    {
        private readonly ElasticClient elasticClient;
        private readonly IPersonAppearanceRepository repository;
        private readonly ITranscribedPARepository transcribedPARepository;
        private readonly IEFDownloadHistoryRepository downloadHistoryRepository;

        public PersonAppearanceController(
            ElasticClient elasticClient,
            IPersonAppearanceRepository repository,
            ITranscribedPARepository transcribedPARepository,
            IEFDownloadHistoryRepository downloadHistoryRepository
        ) {
            this.elasticClient = elasticClient;
            this.repository = repository;
            this.transcribedPARepository = transcribedPARepository;
            this.downloadHistoryRepository = downloadHistoryRepository;
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

        [HttpGet("v2/{id}")]
        [ResponseCache(CacheProfileName = "StaticLinkLivesData")]
        [ProducesResponseType(200)]
        [ProducesResponseType(206)]
        [ProducesResponseType(404)]
        public ActionResult GetV2(string id)
        {
            var pa = repository.GetById(id);

            if (pa == null)
            {
                return NotFound();
            }

            BasePA[] relatedPas;
            try {
                relatedPas = GetRelatedPersonAppearances(pa);
            }
            catch(Exception e) {
                return StatusCode(500, $"Failed to get related Person Appearances: {e.Message}\n{e}");
            }

            return Ok(new {
                PersonAppearance = pa,
                RelatedPersonAppearances = relatedPas
            });
        }

        private BasePA[] GetRelatedPersonAppearances(BasePA pa) {
            if(pa.Source_id == null || pa.Pa_grouping_id_wp4 == null) {
                return new BasePA[]{};
            }

            // Build query JSON
            var data = new Newtonsoft.Json.Linq.JObject();
            data["from"] = 0;
            data["size"] = 100;
            data["query"] = new JObject(
                new JProperty("bool", new JObject(
                    new JProperty("must", new JArray(
                        new JObject(
                            new JProperty("match", new JObject(
                                new JProperty("source_id", pa.Source_id)
                            ))
                        ),
                        new JObject(
                            new JProperty("match", new JObject(
                                new JProperty("pa_grouping_id_wp4", pa.Pa_grouping_id_wp4)
                            ))
                        )
                    ))
                ))
            );

            var stringData = data.ToString(Newtonsoft.Json.Formatting.None);
            var response = elasticClient.LowLevel.Search<StringResponse>("pas", PostData.String(stringData));

            if(response.HttpStatusCode >= 400 && response.HttpStatusCode < 500) {
                throw new ApplicationException($"Bad input for search. ElasticSearch error status {response.HttpStatusCode}: {response.Body}");
            }

            if(response.HttpStatusCode != 200) {
                throw new ApplicationException($"Something went wrong! Failed to load search result data from ElasticSearch - got status code  {response.HttpStatusCode}");
            }

            var result = JObject.Parse(response.Body);
            var hits = (JArray)result.SelectToken("hits.hits");
            var keys = hits.Values<JObject>().Select(hit => {
                return (string)hit.SelectToken("_source.key");
            }).ToList();

            return repository.GetByIds(keys).ToArray();
        }

        [HttpPost("{id}/download.{format}")]
        [ResponseCache(CacheProfileName = "StaticLinkLivesData")]
        [ProducesResponseType(200)]
        [ProducesResponseType(206)]
        [ProducesResponseType(404)]
        [Authorize]
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

            var rows = SpreadsheetSerializer.Serialize(personAppearance);
            var result = encoder.Encode("Person appearance", rows);

            downloadHistoryRepository.RegisterDownload(new DownloadHistoryEntry(
                DownloadType.PersonAppearance,
                id,
                User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value
            ));
            downloadHistoryRepository.Save();

            return File(result, encoder.ContentType, $"personAppearance.{format}");
        }

        [HttpPost("v2/{id}/download.{format}")]
        [ResponseCache(CacheProfileName = "StaticLinkLivesData")]
        [ProducesResponseType(200)]
        [ProducesResponseType(206)]
        [ProducesResponseType(404)]
        [Authorize]
        public ActionResult DownloadV2(string id, string format)
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

            var rows = SpreadsheetSerializer.Serialize(personAppearance);

            BasePA[] relatedPas;
            try {
                relatedPas = GetRelatedPersonAppearances(personAppearance);
            }
            catch(Exception e) {
                Console.WriteLine($"Failed to get related Person Appearances: {e.Message}\n{e}");
                return StatusCode(500, $"Failed to get related Person Appearances");
            }

            var relatedPersonAppearancesRows = SpreadsheetSerializer.SerializeAll(relatedPas);

            var sheet = encoder.Encode(new Dictionary<string, Dictionary<string, (string, Exportable)>[]>{
                ["Person appearance"] = rows,
                ["Related person appearances"] = relatedPersonAppearancesRows,
            });

            downloadHistoryRepository.RegisterDownload(new DownloadHistoryEntry(
                DownloadType.PersonAppearance,
                id,
                User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value
            ));
            downloadHistoryRepository.Save();

            return File(sheet, encoder.ContentType, $"personAppearance.{format}");
        }
    }
}
