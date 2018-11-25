using fomka_web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace fomka_web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


        protected void Application_PostAuthenticateRequest(Object sender, EventArgs args)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                var serializeModel = serializer.Deserialize<LoginInfoWrapper>(authTicket.UserData);

                LoginInfo newUser = new LoginInfo()
                {
                    UserId = serializeModel.UserId,
                    Username = serializeModel.Username,
                    Password = serializeModel.Password
                };
                //newUser. = serializeModel.Id;
                //newUser.FirstName = serializeModel.FirstName;
                //newUser.LastName = serializeModel.LastName;

                HttpContext.Current.User = newUser;
            }
        }
    }
}
