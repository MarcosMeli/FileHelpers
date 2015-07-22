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
            ViewBag.PageTitle = "Online Record Wizard";

            return View();
        }

        [Route("CompareTools")]
        public ActionResult CompareTools()
        {
            ViewBag.PageTitle = "Online Compare Tool";
            return View();
        }
    }
}
