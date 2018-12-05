using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.WebPages;

namespace fomka_web.Models
{
    public static class TreeViewHelper
    {
        public static MvcHtmlString RenderTree(this HtmlHelper helper, TreeViewItem model)
        {
            return new MvcHtmlString(RenderRawString(model));
        }

        private static string RenderRawString(TreeViewItem model)
        {
            var plain = new StringBuilder();

            plain.Append("<li>");
            if (model.HasCaret)
            {
                plain.Append($"<span class=\"caret\">{model.Title}</span>");
                plain.Append("<ul class=\"nested\">");
                foreach (var item in model.SubItems)
                    plain.Append(RenderRawString(item));
                plain.Append("</ul>");
            }
            else
            {
                if (model.Enabled)
                    plain.Append($"<a href=\"/Home/Index/{model.Id}\">{model.Title}</a>");
                else
                    plain.Append($"<span>{model.Title}</span>");
            }
            plain.Append("</li>");

            return plain.ToString();
        }
    }
}