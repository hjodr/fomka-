using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using fomka_web.DAL;

namespace fomka_web.Models
{
    public class TaskVm
    {
        public Task Task { get; set; }
        public List<BlockOfCode> SequenceOfBlocks { get; set; } = new List<BlockOfCode>();
    }
}