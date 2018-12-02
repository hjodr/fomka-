using fomka_web.Attributes;
using fomka_web.DAL;
using fomka_web.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fomka_web.DAL;
using fomka_web.Domain;
using fomka_web.Models;
using System.Web.Script.Serialization;
using System.Web.Security;
using static fomka_web.Attributes.RoleAuthorizeAttribute;

namespace fomka_web.Controllers
{
    public class HomeController : BaseController
    {
        private MainVM vm = new MainVM();
        private MainRepo dbRepo = new MainRepo();

        public ActionResult Index()
        {
            if (GeLoginInfo() == null) return RedirectToAction("Login");
            vm.Role = GeLoginInfo().Type;
            vm.Tasks = dbRepo.GeTasks();

            vm.User = dbRepo.GetUserByUsername(GeLoginInfo().Username);

            return View(vm);
        }

        [HttpGet]
        public ActionResult Login()
        {
            var vm = new LoginInfoWrapper()
            {
                //Username = "Bbrizhaty",
                //Password = "lolkekchebureK1"
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginInfoWrapper objUser)
        {
            if (!ModelState.IsValid)
                return View(objUser);

            var dbUser = Context.Users.FirstOrDefault(u => u.Login == objUser.Username) ?? new User()
            {
                Type = (int)AppUserType.Stundent,
                Password = objUser.Password,
                Login = objUser.Username
            };

            objUser.Type = (AppUserType)dbUser.Type;

            if (dbUser == null || dbUser.Password != objUser.Password)
            {
                ModelState.AddModelError(nameof(LoginInfoWrapper.Username), "Wrong username of password");
                return View(objUser);
            }

            SetLoggedUserInfo(objUser);

            return RedirectToAction(nameof(Index));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [RoleAuthorize(UserType = AppUserType.Lecturer )]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction(nameof(Index));
        }
    }
}