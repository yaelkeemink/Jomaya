using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Jomaya.Frontend.Infrastructure.Agents;
using Jomaya.Frontend.Infrastructure.Agents.Service;
using Jomaya.Frontend.Entities;
using Jomaya.Frontend.Infrastructure.DAL.Repositories;
using Jomaya.Common;

namespace Jomaya.Frontend.Facade.Controllers
{
    public class AutoController : Controller
    {
        private IAutoServiceAgent _agent;

        public AutoController(IAutoServiceAgent agent)
        {
            _agent = agent;
        }
        
        public IActionResult Invoeren(long klantId)
        {
            try
            {
                if (klantId == 0)
                {
                    Response.Redirect(Request.Headers["Referer"].ToString());
                }

                return View(new Auto() { KlantId = klantId });
            }
            catch (Exception e)
            {
                ExceptionEventPublisher.PublishException(e);
                return RedirectToAction("VierNulVier", "fout");
            }
        }
        
        public IActionResult Toevoegen(Auto auto)
        {
            try
            {
                if (!ModelState.IsValid)
                {                    
                    return View("Invoeren", auto);
                }
                auto = _agent.AutoInvoeren(auto);
                return RedirectToAction("Invoeren", "Onderhoud", auto);
            }
            catch (Exception e)
            {
                ExceptionEventPublisher.PublishException(e);
                return RedirectToAction("VierNulVier", "fout");
            }
        }
    }
}
