using fomka_web.Domain;
using fomka_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace fomka_web.Controllers
{
    public class MarksController : BaseController
    {
        private MarksVm vm = new MarksVm();
        private MainRepo dbRepo = new MainRepo();

        public ActionResult Index(int? id = null)
        {
            id = id ?? 0;
            vm.ModuleId = id.Value;
            if (id!=0) vm.SelectedModule = dbRepo.GetModules().SingleOrDefault(m => m.Id == id) ?? null;
            if (GeLoginInfo() == null) return RedirectToAction("Login");
            vm.Role = GeLoginInfo().Type;
            vm.Tasks = dbRepo.GeTasksByModule(id.Value);
            vm.Students = dbRepo.GetStudents();

            vm.User = dbRepo.GetUserByUsername(GeLoginInfo().Username);

            return View(vm);
        }
    }
}