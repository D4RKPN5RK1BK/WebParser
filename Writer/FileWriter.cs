using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebParser.Models;

namespace WebParser.Writer
{
    internal class FileWriter
    {
        public FileWriter()
        {

        }

        public void CreateHTMLMap(List<PageGroup> pageGroups)
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine("<ol>");
            foreach (PageGroup pg in pageGroups)
            {
                output.AppendLine("<li>");
                output.AppendLine("<div>");
                output.AppendLine(pg.Name);
                output.AppendLine("</div>");
                output.AppendLine("<ol>");
                foreach (Page p in pg.Pages)
                    output.Append(OLIterator(p));
                output.AppendLine("</ol>");
                output.AppendLine("</li>");
            }

            var env = Environment.SpecialFolder.Desktop;
            var path = Environment.GetFolderPath(env);

            if (!Directory.Exists(path + "/WebParser"))
                Directory.CreateDirectory(path + "/WebParser");

            FileStream mapFile = File.Create(path + "/WebParser/map.html");
            byte[] bytes = Encoding.UTF8.GetBytes(output.ToString());
            mapFile.Write(bytes);
        }

        private StringBuilder OLIterator(Page page)
        {
            StringBuilder output = new StringBuilder();

            if (page.Children.Count() > 0)
            {
                output.AppendLine("<li>");
                output.AppendLine($"<a href=\"{page.LegasyURL}\">{page.Name}</a>");
                output.AppendLine("<ol>");
                foreach (Page p in page.Children)
                    output.Append(OLIterator(p));
                output.AppendLine("</ol>");
                output.AppendLine("</li>");
            }
            else
                output.AppendLine($"<li><a href =\"{page.LegasyURL}\">{page.Name}</a></li>");

            return output;
        }
    }
}
