using fomka_web.DAL;
using fomka_web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace fomka_web.Attributes
{
    public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
        public AppUserType UserType { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
            {
                return false;
            }

            var usr = httpContext.User as LoginInfo;
            if (usr == null)
                return false;

            return UserType.HasFlag(usr.Type) || usr.Type.HasFlag(AppUserType.Admin);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
            }
        }

        // must coallate with UserType table Id's
        public enum AppUserType
        {
            Stundent = 1,
            Lecturer = 2,
            Admin = 4,
        }
    }
}