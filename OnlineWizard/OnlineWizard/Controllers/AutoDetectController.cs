using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using FileHelpers;

namespace OnlineWizard.Controllers
{

    public class AutoDetectController : Controller
    {
        [HttpPost]
        public ActionResult UploadSample(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                var tempFile = Path.GetTempFileName();
                file.SaveAs(tempFile);

                var detector = new FileHelpers.Detection.SmartFormatDetector
                {
                    Encoding = Encoding.UTF8,
                    FileHasHeaders = true
                };

                var res = detector.DetectFileFormat(tempFile);
                try
                {
                    System.IO.File.Delete(tempFile);
                }
                catch
                {}

                return Json(res);
            }

            return Content("");

        }
        
    }
}
