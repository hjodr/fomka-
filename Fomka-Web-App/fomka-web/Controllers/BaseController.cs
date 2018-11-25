using fomka_web.Domain;
using fomka_web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace fomka_web.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected UserRepo UserRepo { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var loginInfo = Request.Cookies.ExctractLoginInfo();
            UserRepo = new UserRepo(loginInfo);
        }
    }
}