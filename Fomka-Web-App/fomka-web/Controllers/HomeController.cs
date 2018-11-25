using fomka_web.Attributes;
using fomka_web.DAL;
using fomka_web.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace fomka_web.Controllers
{
    public class HomeController : BaseController
    {


        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginInfo objUser)
        {

            //TODO:validation with ef 
            var uData = new LoginInfoWrapper()
            {
                Password = "SamplePass",
                UserId = 100500,
                Username = "SampleName"
            };

            var authTicket = new FormsAuthenticationTicket(1, "SampleName", DateTime.UtcNow, DateTime.UtcNow.AddMinutes(3), false, new JavaScriptSerializer().Serialize(uData));

            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            Response.Cookies.Add(faCookie);

            //success
            Session["UserID"] = objUser.UserId;
            Session["UserName"] = objUser.Username;
            return RedirectToAction("Index");

            //fail
            return View(objUser);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [RoleAuthorize(UserType = new[] { "Type1", "Type2" })]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}