using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fomka_web.DAL;

namespace fomka_web.Models
{
    public class TaskVm
    {
        public Task Task { get; set; }
        public List<BlockOfCode> SequenceOfBlocks { get; set; } = new List<BlockOfCode>();
        public List<BlockOfCode> SelectedBlocks { get; set; } = new List<BlockOfCode>();
        public string selection;
        public string blocks;

        public List<SelectListItem> DifficultyLevels { get; set; }
        public List<SelectListItem> ProgrammingLanguages { get; set; }

    }
}