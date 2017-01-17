using System;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.SwaggerGen.Annotations;
using System.Net;
using Jomaya.AutoService.Entities;
using Jomaya.AutoService.Facade.Errors;
using Jomaya.Common;

namespace Jomaya.AutoService.Facade.Controllers
{
    [Route("api/[controller]")]
    public class AutoController : Controller
    {
        private readonly Services.AutoService _service;

        public AutoController(Services.AutoService service)
        {
            _service = service;
        }
        
        [HttpPost]        
        [SwaggerOperation("PostCreateAuto")]
        [ProducesResponseType(typeof(Auto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public IActionResult CreateAuto([FromBody]Auto auto)
        {
            if (!ModelState.IsValid)
            {
                var error = new ErrorMessage(ErrorTypes.BadRequest, $"Modelstate Invalide, {auto.ToString()}");
                return BadRequest(error);
            }
            try
            {
                auto = _service.CreateAuto(auto);
                return Ok(auto);
            }
            catch (Exception e)
            {
                ExceptionEventPublisher.PublishException(e);
                var error = new ErrorMessage(ErrorTypes.Unknown,
                    $"Onbekende fout met volgende Command: Kenteken:{auto?.Kenteken}, KlantId:{auto?.KlantId}, TimeStamp:{DateTime.UtcNow} {e}");
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
