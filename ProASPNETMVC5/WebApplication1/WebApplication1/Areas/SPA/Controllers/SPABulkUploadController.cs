using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Filters;

namespace WebApplication1.Areas.SPA.Controllers
{
    public class SPABulkUploadController : AsyncController
    {
        // GET: SPA/SPABulkUpload
        [AdminFilter]
        public ActionResult Index()
        {
            return PartialView("Index");
        }


    }
}