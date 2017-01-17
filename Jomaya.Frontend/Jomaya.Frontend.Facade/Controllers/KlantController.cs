using Jomaya.Common;
using Jomaya.Frontend.Entities;
using Jomaya.Frontend.Infrastructure.Agents;
using Jomaya.Frontend.Infrastructure.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jomaya.Frontend.Facade.Controllers
{
    public class KlantController
        : Controller
    {
        private IKlantServiceAgent _agent;

        public KlantController(IKlantServiceAgent agent)
        {
            _agent = agent;
        }

        public IActionResult Invoeren()
        {
            return View();
        }

        public IActionResult Insert(Klant klant)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Invoeren", klant);
                }

                klant = _agent.KlantInvoeren(klant);
                return RedirectToAction("Invoeren", "Auto", new { klantId = klant.Id }); // ga naar Auto invoeren
            }
            catch (Exception e)
            {
                ExceptionEventPublisher.PublishException(e);
                return RedirectToAction("VierNulVier", "fout");
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
