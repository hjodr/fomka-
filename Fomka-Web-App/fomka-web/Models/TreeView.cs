using System;
using System.Collections.Generic;
using System.Linq;

namespace fomka_web.Models
{
    public class TreeViewItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Enabled => Id > -1;
        public bool Selected { get; set; }
        public IEnumerable<TreeViewItem> SubItems { get; set; }
        public bool HasCaret => SubItems.Any();
        public TreeViewItem(int id, string title, bool selected)
        {
            SubItems = new List<TreeViewItem>();
            Id = id;
            Title = title;
            Selected = selected;
        }
        public TreeViewItem(string title)
            :this(-1, title, false)
        {

        }

        public bool IsOpened()
        {
            return Selected || SubItems.Any(s => s.IsOpened());
        }
    }
}