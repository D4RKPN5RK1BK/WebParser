using WebPareser.Data;
using System;
using AngleSharp;
using WebPareser.Scanner;
using WebParser.Models;
using Microsoft.EntityFrameworkCore;
using AngleSharp.Dom;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebPareser {
	class Program {

		private static DatabaseContext? context;
		
		static void Main() {
			context = new DatabaseContext();

            Console.WriteLine("Отчистка базы данных");
            context.Pages.RemoveRange(context.Pages);
            context.PageGroups.RemoveRange(context.PageGroups);
            context.SaveChanges();
            Console.WriteLine("Отчистка базы данных завершена");

            WebScanner scanner = new WebScanner();

            Console.WriteLine("Сканирование начальной страницы");
            List<PageGroup> pageGroups = scanner.ScanMainPage();
            Console.WriteLine("Сканирование начальной страницы завершено");

            Console.WriteLine("Сохранение данных в БД");
            context.AddRange(pageGroups);
            context.SaveChanges();
            Console.WriteLine("Сохранение данных в БД завершено");

            List<Page> pages = context.Pages.ToList();

            Console.WriteLine("Сканирование вложенных страниц");
            pages = scanner.ScanPagesRangeBranch(pages);
            Console.WriteLine("Сканирование вложенных страниц завершенно");


            List<Page> dbPages = context.Pages.ToList();
            foreach (Page page in dbPages)
                pages.RemoveAll(o => o == page);

            Console.WriteLine("Сохранение данных в БД");
            context.Pages.AddRange(pages);
            context.SaveChanges();
            Console.WriteLine("Сохранение данных в БД завершено");


        }
    }
}
