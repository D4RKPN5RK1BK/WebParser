using System;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using WebParser.Models;
using static System.Environment;

namespace WebPareser.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Page>? Pages { get; set; }

        public DbSet<Document>? Documents { get; set; }

        public DbSet<PageGroup> PageGroups { get; set; }

        public string DbPath { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {

            SpecialFolder folder = Environment.SpecialFolder.Desktop;
            string path = Environment.GetFolderPath(folder);

            if (!Directory.Exists(path + "/WebParser"))
                Directory.CreateDirectory(path + "/WebParser");

            DbPath = Path.Join(path, "WebParser\\Database.db");

            builder.UseSqlite($"DataSource={DbPath}");
        }


        public void ClearEmptyPages()
        {
            Pages.RemoveRange(Pages.Where(o => String.IsNullOrEmpty(o.Name) || Regex.IsMatch(o.Name, @"^\s*$")));
            SaveChanges();
        }

        public void AddPageGroup(PageGroup pageGroup)
        {
            if (!PageGroups.Any(o => o.Name == pageGroup.Name))
            {
                PageGroups.Add(pageGroup);
                SaveChanges();
            }
        }

        public void AddPage(Page page)
        {
            if (!Pages.Any(o => o.Name == page.Name && o.LegasyURL == page.LegasyURL))
            {
                Pages.Add(page);
                SaveChanges();
            }
        }
    }
}