using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OnlineWizard.Controllers
{

    public class HomeController : Controller
    {
        [Route("")]
        public ActionResult Index()
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
