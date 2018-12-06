using fomka_web.DAL;
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
            using (var mngr = new MigrationManager(new SEVL(), Server.MapPath("~/App_Data/Migrations")))
            {
                mngr.RunMigration("20181202_add_modules_for_task");
                mngr.RunMigration("20181203_seed_modules");
                mngr.RunMigration("20181206_alter_nullable_columns");
                mngr.RunMigration("20181206_alter_nullable_columns_Marks");
            }
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
                var authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                var userData = (new JavaScriptSerializer())
                    .Deserialize<LoginInfoWrapper>(authTicket.UserData);

                LoginInfo newUser = new LoginInfo(userData.Username, userData.Type)
                {
                    UserId = userData.UserId,
                    Password = userData.Password,
                    Type = userData.Type
                };

                HttpContext.Current.User = newUser;
            }
        }
    }
}
