using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OnlineWizard.Controllers
{

    public class HomeController : Controller
    {
        [Route("")]
        public ActionResult Wizard()
        {
            ViewBag.HideMenu = true;

            return View();
        }

        [Route("CompareTools")]
        public ActionResult CompareTools()
        {
            return View();
        }
    }
}
