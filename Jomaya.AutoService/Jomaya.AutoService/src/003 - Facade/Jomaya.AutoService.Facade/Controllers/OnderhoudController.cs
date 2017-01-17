using Jomaya.AutoService.Entities;
using Jomaya.AutoService.Facade.Errors;
using Jomaya.Common;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.SwaggerGen.Annotations;
using System;
using System.Net;

namespace Jomaya.AutoService.Facade.Controllers
{
    [Route("api/[controller]")]
    public class OnderhoudController : Controller
    {
        private readonly Services.AutoService _service;

        public OnderhoudController(Services.AutoService service)
        {
            _service = service;
        }

        [HttpPost]
        [SwaggerOperation("PostCreateOnderhoudsopdracht")]
        [ProducesResponseType(typeof(Onderhoudsopdracht), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public IActionResult CreateOnderhoudsopdracht([FromBody]Onderhoudsopdracht onderhoudsopdracht)
        {
            if (!ModelState.IsValid)
            {
                var error = new ErrorMessage(ErrorTypes.BadRequest, "Modelstate Invalide");
                return BadRequest(error);
            }
            try
            {
                onderhoudsopdracht = _service.CreateOnderhoudsopdracht(onderhoudsopdracht);
                return Ok(onderhoudsopdracht);
            }
            catch (Exception e)
            {
                ExceptionEventPublisher.PublishException(e);
                var error = new ErrorMessage(ErrorTypes.Unknown,
                    $"Onbekende fout met volgende Command: AutoId:{onderhoudsopdracht.Auto.Id}, kilometerstand:{onderhoudsopdracht.Kilometerstand} {e}");
                return BadRequest(error);
            }
        }

        [HttpPut]
        [SwaggerOperation("PutUpdateOnderhoudsopdracht")]
        [ProducesResponseType(typeof(Onderhoudsopdracht), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateOnderhoudsopdracht([FromBody]Onderhoudsopdracht onderhoudsopdracht)
        {
            if (!ModelState.IsValid)
            {
                var error = new ErrorMessage(ErrorTypes.BadRequest, "Modelstate Invalide");
                return BadRequest(error);
            }
            try
            {
                Onderhoudsopdracht onderhoudsOpdracht = _service.UpdateOnderhoudsopdracht(onderhoudsopdracht);
                return Ok(onderhoudsOpdracht);
            }
            catch (Exception e)
            {
                ExceptionEventPublisher.PublishException(e);
                var error = new ErrorMessage(ErrorTypes.Unknown,
                    $"Onbekende fout met volgende Command: OnderhoudId:{onderhoudsopdracht.Id}, status:{onderhoudsopdracht.Status} {e}");
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
