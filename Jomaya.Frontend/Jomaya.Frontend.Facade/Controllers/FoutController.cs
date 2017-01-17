using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jomaya.Frontend.Facade.Controllers
{
    public class FoutController
        : Controller
    {
        public IActionResult VierNulVier()
        {
            return View();
        }
    }
}
