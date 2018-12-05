using fomka_web.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fomka_web.Attributes;
using fomka_web.BL;
using fomka_web.Models;
using fomka_web.Domain;
using static fomka_web.Attributes.RoleAuthorizeAttribute;

namespace fomka_web.Controllers
{
    public class TasksController : BaseController
    {
        private TaskVm vm = new TaskVm();
        private MainRepo dbRepo = new MainRepo();
        // GET: Tasks
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Open(string taskId, string mode)
        {
            string blocks = null;
            var selection = "";
            if (mode == "open") {
                vm.Task = dbRepo.GeTaskById(Convert.ToInt32(taskId));
                vm.SequenceOfBlocks = BlockGenerator.RandomizeBlocks(BlockGenerator.Code2Blocks(vm.Task.Standard.StandardFile));
                vm.SelectedBlocks = new List<BlockOfCode>();
                vm.blocks = BlockGenerator.GetOrder(vm.SequenceOfBlocks);
                vm.selection = "";
                return View(vm);
            }
            else
            {
                if (Request.Cookies["blocks"] != null)
                {
                    selection = Uri.UnescapeDataString(Request.Cookies["selection"].Value);
                    blocks = Uri.UnescapeDataString(Request.Cookies["blocks"].Value);
                }

                vm.Task = dbRepo.GeTaskById(Convert.ToInt32(taskId));

                var codeBlocks = BlockGenerator.Code2Blocks(vm.Task.Standard.StandardFile);
                vm.SelectedBlocks = BlockGenerator.OrderBlocks(codeBlocks, selection);
                vm.SequenceOfBlocks = BlockGenerator.SetSelected(BlockGenerator.OrderBlocks(codeBlocks, blocks), vm.SelectedBlocks);
                vm.selection = selection;
                vm.blocks = blocks;

                return View(vm);
            }
        }

        public ActionResult Select(string taskId)
        {
            return RedirectToAction("Open",new{ taskId = taskId});
        }

        [RoleAuthorize(UserType = AppUserType.Lecturer)]
        public ActionResult Edit(string taskId)
        {
            vm.Task = dbRepo.GeTaskById(Convert.ToInt32(taskId));
            var difficultyLevels = dbRepo.GetDifficultyLevels();
            vm.DifficultyLevels = difficultyLevels.Select(d => new SelectListItem() {Text = d.Title, Value = d.Id.ToString()}).ToList();
            var programmingLanguages = dbRepo.GetLanguages();
            vm.ProgrammingLanguages = programmingLanguages.Select(d => new SelectListItem() { Text = d.Title, Value = d.Id.ToString() }).ToList();
            return View(vm);
        }

        [Authorize]
        public ActionResult Submit(TaskVm vm)
        {
            var userName = GeLoginInfo().Username;
            var userId = dbRepo.GetUserByUsername(userName).Id;
            vm.Task = dbRepo.GeTaskById(vm.Task.Id);
            if (vm.Task.Marks.Select(m=>m.UserId==userId).Count() == 0)
            {
                string blocks = null;
                var selection = "";
                if (Request.Cookies["blocks"] != null)
                {
                    selection = Uri.UnescapeDataString(Request.Cookies["selection"].Value);
                    blocks = Uri.UnescapeDataString(Request.Cookies["blocks"].Value);
                }

                var codeBlocks = BlockGenerator.Code2Blocks(vm.Task.Standard.StandardFile);
                vm.SelectedBlocks = BlockGenerator.OrderBlocks(codeBlocks, selection);
                vm.SequenceOfBlocks =
                    BlockGenerator.SetSelected(BlockGenerator.OrderBlocks(codeBlocks, blocks), vm.SelectedBlocks);
                vm.selection = selection;
                vm.blocks = blocks;

                var answer = BlockGenerator.AnswerCorrectInPercent(vm.SelectedBlocks, vm.SequenceOfBlocks);
                vm.Answer = "Завдання успішно здано. Ваша оцінка: " + answer.ToString("F3");
                
                dbRepo.SaveMark(taskId: vm.Task.Id, mark: answer, userId: userId);
            }
            else
            {
                vm.Answer = "Ви вже виконали дане завдання";
            }

            return View(vm);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditTask(TaskVm vm)
        {
            dbRepo.SaveTask(vm.Task);
            return RedirectToAction("Index","Home", new { id = vm.Task.ModuleId });
        }

        [RoleAuthorize(UserType = AppUserType.Lecturer)]
        public ActionResult Add()
        {
            vm.Task = new Task();
            var difficultyLevels = dbRepo.GetDifficultyLevels();
            vm.DifficultyLevels = difficultyLevels.Select(d => new SelectListItem() { Text = d.Title, Value = d.Id.ToString() }).ToList();
            var programmingLanguages = dbRepo.GetLanguages();
            vm.ProgrammingLanguages = programmingLanguages.Select(d => new SelectListItem() { Text = d.Title, Value = d.Id.ToString() }).ToList();
            var modules = dbRepo.GetModules();
            vm.Modules = dbRepo
                .GetModules()
                .Select(m => new SelectListItem() { Text = m.Title, Value = m.Id.ToString() }).ToList();

            return View(vm);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddTask(TaskVm vm)
        {
            dbRepo.SaveTask(vm.Task);
            return RedirectToAction("Index", "Home", new { id = vm.Task.ModuleId });
        }

        [RoleAuthorize(UserType = AppUserType.Lecturer)]
        public ActionResult Delete(string taskId)
        {
            dbRepo.DeleteTask(Convert.ToInt32(taskId));
            return RedirectToAction("Index", "Home");
        }
    }
}