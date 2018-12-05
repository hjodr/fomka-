using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fomka_web.Models
{
    public class TreeViewItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Enabled => Id > -1;
        public IEnumerable<TreeViewItem> SubItems { get; set; }
        public bool HasCaret => SubItems.Any();
        public TreeViewItem(int id, string title)
        {
            SubItems = new List<TreeViewItem>();
            Id = id;
            Title = title;
        }
        public TreeViewItem(string title)
            :this(-1, title)
        {

        }
    }
}