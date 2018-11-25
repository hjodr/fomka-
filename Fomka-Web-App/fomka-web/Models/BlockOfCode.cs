using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fomka_web.Models
{
    public class BlockOfCode
    {
        public int BlockID { get; set; }
        public bool selected { get; set; }
        public string Code { get; set; }
    }
}