using fomka_web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fomka_web.DAL;
using fomka_web.Domain;
using fomka_web.Models;

namespace fomka_web.Controllers
{
    public class HomeController : BaseController
    {
        private MainVM vm = new MainVM();
        private MainRepo dbRepo = new MainRepo();

        public ActionResult Index()
        {
            vm.Tasks = dbRepo.GeTasks();

            return View(vm);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginInfo objUser)
        {

            //TODO:validation with ef 

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

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}