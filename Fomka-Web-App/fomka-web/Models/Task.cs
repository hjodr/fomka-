using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fomka_web.Models
{
    public class Task
    {
        public ushort TaskID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<BlockOfCode> SequenceOfBlocks { get; set; } = new List<BlockOfCode>();
    }
}