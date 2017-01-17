using System;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.SwaggerGen.Annotations;
using System.Net;
using Jomaya.Klantenservice.Facade.Errors;
using Jomaya.Klantenservice.Services;
using Jomaya.Klantenservice.Entities;
using Jomaya.Common;

namespace Jomaya.Klantenservice.Facade.Controllers
{
    [Route("api/[controller]")]
    public class KlantController : Controller
    {
        private readonly KlantService _service;

        public KlantController(KlantService service)
        {
            _service = service;
        }

        // POST api/values
        [HttpPost]        
        [SwaggerOperation("Post")]
        [ProducesResponseType(typeof(Klant), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public IActionResult CreateKlant([FromBody]Klant klant)
        {
            if (!ModelState.IsValid)
            {
                var error = new ErrorMessage(ErrorTypes.BadRequest, "Modelstate Invalide");
                return BadRequest(error);
            }
            try
            {
                _service.CreateKlant(klant);
                return Ok(klant);
            }
            catch (Exception e)
            {
                ExceptionEventPublisher.PublishException(e);
                var error = new ErrorMessage(ErrorTypes.Unknown,
                    $"Onbekende fout met volgende Command: RoomName:{klant.Voorletters}, TimeStamp:{DateTime.UtcNow} {e}");
                return BadRequest(error);
            }
        }
        
        [HttpGet("{klantId}")]
        [SwaggerOperation("GetKlant")]
        [ProducesResponseType(typeof(Klant), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public IActionResult GetKlant(long klantId)
        {            
            try
            {
                var klant = _service.GetKlant(klantId);
                return Ok(klant);
            }
            catch (Exception e)
            {
                ExceptionEventPublisher.PublishException(e);
                var error = new ErrorMessage(ErrorTypes.Unknown,
                    $"Onbekende fout met volgende Command: Method:'GetEigenaarNaam' KlantID:{klantId}, TimeStamp:{DateTime.UtcNow} {e}");
                return BadRequest(error);
            }
        }

        protected override void Dispose(bool disposing)
        {
            _service?.Dispose();
            base.Dispose(disposing);
        }
    }
}
