using Jomaya.Frontend.Entities;
using Jomaya.Frontend.Infrastructure.Agents.Service;
using Jomaya.Frontend.Infrastructure.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Jomaya.Common;
using System;
using Jomaya.Frontend.Services.Interfaces;

namespace Jomaya.Frontend.Facade.Controllers
{
    public class OnderhoudController : Controller
    {
        private IAutoServiceAgent _agent;
        private IRepository<Auto, long> _autoRepo;
        private IRepository<Onderhoudsopdracht, long> _onderhoudRepo;
        private IRepository<Klant, long> _klantRepo;

        public OnderhoudController(IAutoServiceAgent agent, IRepository<Auto, long> autoRepo, IRepository<Onderhoudsopdracht, long> onderhoudRepo, IRepository<Klant, long> klantRepo)
        {
            _agent = agent;
            _autoRepo = autoRepo;
            _onderhoudRepo = onderhoudRepo;
            _klantRepo = klantRepo;
        }

        public IActionResult Index()
        {
            var onderhoudsOpdrachten = _onderhoudRepo.FindBy(a => a.Status < OnderhoudStatus.Afgemeld)
                .OrderBy(a => a.Datum)
                .ThenBy(a => a.Status)
                .ToList();

            return View(onderhoudsOpdrachten);
        }

        public IActionResult OnderhoudBewerken(long id)
        {
            try
            {
                var onderhoudsopdracht = _onderhoudRepo.Find(id);
                onderhoudsopdracht.Auto = _autoRepo.Find(onderhoudsopdracht.AutoId);
                onderhoudsopdracht.Auto.klant = _klantRepo.Find(onderhoudsopdracht.Auto.KlantId);
                switch (onderhoudsopdracht.Status)
                {
                    case OnderhoudStatus.Aangemeld:
                        return View("OnderhoudStarten", onderhoudsopdracht);
                    case OnderhoudStatus.Opgepakt:
                        return View("OnderhoudKlaarmelden", onderhoudsopdracht);
                    case OnderhoudStatus.Klaargemeld:
                        return View("WachtenOpRDW", onderhoudsopdracht);
                    case OnderhoudStatus.InSteekproef:
                        return View("OnderhoudAfmelden", onderhoudsopdracht);
                }
                return View();
            }
            catch (Exception e)
            {
                ExceptionEventPublisher.PublishException(e);
                return RedirectToAction("VierNulVier", "fout");
            }
        }

        public IActionResult Invoeren(Auto auto)
        {
            try
            {
                return View(new Onderhoudsopdracht() { AutoId = auto.Id });
            }
            catch (Exception e)
            {
                ExceptionEventPublisher.PublishException(e);
                return RedirectToAction("VierNulVier", "fout");
            }
        }

        public IActionResult Toevoegen(Onderhoudsopdracht onderhoudsopdracht)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Invoeren", onderhoudsopdracht);
                }

                var result = _agent.OnderhoudsopdrachtInvoeren(onderhoudsopdracht);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ExceptionEventPublisher.PublishException(e);
                return RedirectToAction("VierNulVier", "fout");
            }
        } 
        
        
        public IActionResult OnderhoudStarten(Onderhoudsopdracht onderhoudsopdracht)
        {
            try
            {
                if (!ModelState.IsValid)
                {                    
                    return View("Toevoegen", onderhoudsopdracht);
                }
                onderhoudsopdracht = _onderhoudRepo.Find(onderhoudsopdracht.Id);
                onderhoudsopdracht.Status = OnderhoudStatus.Opgepakt;
                _agent.OnderhoudsOpdrachtUpdate(onderhoudsopdracht);
                return RedirectToAction("Index"); 
            }
            catch (Exception e)
            {
                ExceptionEventPublisher.PublishException(e);
                return RedirectToAction("VierNulVier", "fout");
            }
        }
        public IActionResult Klaarmelden(Onderhoudsopdracht onderhoudsopdracht)
        {
            try
            {
                if (!ModelState.IsValid)
                {                    
                    return View("OnderhoudStarten", onderhoudsopdracht);
                }
                onderhoudsopdracht = _onderhoudRepo.Find(onderhoudsopdracht.Id);
                onderhoudsopdracht.Status = OnderhoudStatus.Klaargemeld;
                _agent.OnderhoudsOpdrachtUpdate(onderhoudsopdracht);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ExceptionEventPublisher.PublishException(e);
                return RedirectToAction("VierNulVier", "fout");
            }
        }
        public IActionResult OnderhoudAfmelden(Onderhoudsopdracht onderhoudsopdracht)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Toevoegen", onderhoudsopdracht);
                    
                }
                onderhoudsopdracht = _onderhoudRepo.Find(onderhoudsopdracht.Id);
                onderhoudsopdracht.Status = OnderhoudStatus.Afgemeld;
                _agent.OnderhoudsOpdrachtUpdate(onderhoudsopdracht);
                return RedirectToAction("Index"); 
            }
            catch (Exception e)
            {
                ExceptionEventPublisher.PublishException(e);
                return RedirectToAction("VierNulVier", "fout");
            }
        }
    }
}
