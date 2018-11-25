using fomka_web.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fomka_web.Models;
using fomka_web.Domain;

namespace fomka_web.Controllers
{
    public class TasksController : Controller
    {
        private TaskVm vm = new TaskVm();
        private MainRepo dbRepo = new MainRepo();
        // GET: Tasks
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Open(string taskId)
        {
            vm.Task = dbRepo.GeTaskById(Convert.ToInt32(taskId));

            return View(vm);
        }
    }
}