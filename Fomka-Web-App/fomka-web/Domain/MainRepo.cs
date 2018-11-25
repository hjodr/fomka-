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
            using (context)
            {
                return context.Tasks.Select(t=>t).Include(t=>t.DifficultyLevel).Include(t=>t.ProgrammingLanguage).Include(t=>t.Standard).ToList();
            }
        }

        public Task GeTaskById(int taskId)
        {
            using (context)
            {
                return context.Tasks.Select(t=>t).Include(t=>t.DifficultyLevel).Include(t=>t.ProgrammingLanguage).Include(t=>t.Standard).SingleOrDefault(t=>t.Id==taskId);
            }
        }
    }
}