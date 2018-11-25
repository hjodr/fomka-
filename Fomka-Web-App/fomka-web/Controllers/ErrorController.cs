using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace fomka_web.Controllers
{
    public class ErrorController : BaseController
    {
        [HttpGet]
        public ActionResult AccessDenied()
        {
            return Content("Access denied");
        }
    }
}