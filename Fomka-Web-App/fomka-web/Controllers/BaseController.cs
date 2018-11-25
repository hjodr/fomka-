using fomka_web.Common;
using fomka_web.DAL;
using fomka_web.Domain;
using fomka_web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;


namespace fomka_web.Controllers
{
    public class BaseController : Controller
    {
        protected virtual void SetLoggedUserInfo(LoginInfoWrapper loginInfoWrapper)
        {
            var authTicket = new FormsAuthenticationTicket(1, 
                loginInfoWrapper.Username, 
                DateTime.UtcNow, 
                DateTime.UtcNow.AddMinutes(AppSettings.AuthCookieExpirationTime), 
                false, 
                new JavaScriptSerializer().Serialize(loginInfoWrapper));
            string encTicket = FormsAuthentication.Encrypt(authTicket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            Response.Cookies.Add(authCookie);
        }

        private SEVL _context = null;

        protected SEVL Context
        {
            get { if (_context == null) _context = new SEVL(); return _context; }
        }
    }
}