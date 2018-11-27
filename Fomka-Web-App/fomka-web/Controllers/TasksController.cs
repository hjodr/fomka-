using fomka_web.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fomka_web.BL;
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
            string blocks = null;
            var selection = "";
            if (Request.Cookies["blocks"] != null)
            {
                selection = Uri.UnescapeDataString(Request.Cookies["selection"].Value);
                blocks = Uri.UnescapeDataString(Request.Cookies["blocks"].Value);
            }

            if (blocks == null) //if first time open task
            {
                vm.Task = dbRepo.GeTaskById(Convert.ToInt32(taskId));
                vm.SequenceOfBlocks =
                    BlockGenerator.RandomizeBlocks(BlockGenerator.Code2Blocks(vm.Task.Standard.StandardFile));
                vm.SelectedBlocks = new List<BlockOfCode>();
                vm.blocks = BlockGenerator.GetOrder(vm.SequenceOfBlocks);
                vm.selection = "";
                return View(vm);
            }
            else //if sellecting stuff
            {
                vm.Task = dbRepo.GeTaskById(Convert.ToInt32(taskId));

                var codeBlocks = BlockGenerator.Code2Blocks(vm.Task.Standard.StandardFile);
                vm.SelectedBlocks = BlockGenerator.OrderBlocks(codeBlocks, selection);
                vm.SequenceOfBlocks = BlockGenerator.SetSelected(BlockGenerator.OrderBlocks(codeBlocks, blocks),vm.SelectedBlocks);
                vm.selection = selection;
                vm.blocks = blocks;

                return View(vm);
            }
        }

        public ActionResult Select(string taskId)
        {
            return RedirectToAction("Open",new{ taskId = taskId});
        }

        public ActionResult Edit(string taskId)
        {
            vm.Task = dbRepo.GeTaskById(Convert.ToInt32(taskId));
            var difficultyLevels = dbRepo.GetDifficultyLevels();
            vm.DifficultyLevels = difficultyLevels.Select(d => new SelectListItem() {Text = d.Title, Value = d.Id.ToString()}).ToList();
            var programmingLanguages = dbRepo.GetLanguages();
            vm.ProgrammingLanguages = programmingLanguages.Select(d => new SelectListItem() { Text = d.Title, Value = d.Id.ToString() }).ToList();
            return View(vm);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditTask(TaskVm vm)
        {
            dbRepo.SaveTask(vm.Task);
            return RedirectToAction("Index","Home");
        }

        public ActionResult Add()
        {
            vm.Task = new Task();
            var difficultyLevels = dbRepo.GetDifficultyLevels();
            vm.DifficultyLevels = difficultyLevels.Select(d => new SelectListItem() { Text = d.Title, Value = d.Id.ToString() }).ToList();
            var programmingLanguages = dbRepo.GetLanguages();
            vm.ProgrammingLanguages = programmingLanguages.Select(d => new SelectListItem() { Text = d.Title, Value = d.Id.ToString() }).ToList();
            return View(vm);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddTask(TaskVm vm)
        {
            dbRepo.SaveTask(vm.Task);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Delete(string taskId)
        {
            dbRepo.DeleteTask(Convert.ToInt32(taskId));
            return RedirectToAction("Index", "Home");
        }
    }
}