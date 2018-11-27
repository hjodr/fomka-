using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using fomka_web.DAL;

namespace fomka_web.Domain
{
    public class MainRepo
    {
        public SEVL context = new SEVL();

        public List<Task> GeTasks()
        {
            var context = new SEVL();
            using (context)
            {
                return context.Tasks.Select(t=>t).Include(t=>t.DifficultyLevel).Include(t=>t.ProgrammingLanguage).Include(t=>t.Standard).ToList();
            }
        }

        public Task GeTaskById(int taskId)
        {
            var context = new SEVL();
            using (context)
            {
                return context.Tasks.Select(t=>t).Include(t=>t.DifficultyLevel).Include(t=>t.ProgrammingLanguage).Include(t=>t.Standard).SingleOrDefault(t=>t.Id==taskId);
            }
        }

        public List<ProgrammingLanguage> GetLanguages()
        {
            var context = new SEVL();
            using (context)
            {
                return context.ProgrammingLanguages.Select(d => d).ToList();
            }
        }

        public List<DifficultyLevel> GetDifficultyLevels()
        {
            var context = new SEVL();
            using (context)
            {
                return context.DifficultyLevels.Select(d=>d).ToList();
            }
        }

        public void SaveTask(Task task)
        {
            var context = new SEVL();
            using (context)
            {
                if (task.Id!=0)
                {
                    var taskToEdit = GeTaskById(task.Id);
                    taskToEdit.Description = task.Description;
                    taskToEdit.Title = task.Title;
                    taskToEdit.PLId = task.PLId;
                    taskToEdit.DifficultyLevelId = task.DifficultyLevelId;
                    taskToEdit.Standard.StandardFile = task.Standard.StandardFile;
                }
                else
                {
                    var newTask = new Task()
                    {
                        Description = task.Description,
                        Title = task.Title,
                        PLId = task.PLId,
                        DifficultyLevelId = task.DifficultyLevelId,
                        Standard = new Standard()
                        {
                            StandardFile = task.Standard.StandardFile
                        }
                    };
                    context.Tasks.Add(newTask);
                }
                context.SaveChanges();
            }
        }

        public void DeleteTask(int taskId)
        {
            var context = new SEVL();
            using (context)
            {
                var task = GeTaskById(taskId);
                context.Tasks.Attach(task);
                context.Tasks.Remove(task);
                context.SaveChanges();
            }
        }
    }
}