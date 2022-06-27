using WebPareser.Data;
using System;
using AngleSharp;
using WebPareser.Scanner;
using WebParser.Models;
using Microsoft.EntityFrameworkCore;
using AngleSharp.Dom;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using WebParser.Writer;
using CommandLine;
using WebParser.CommandVerbs;
using WebParser.CommandExecution;
using Microsoft.Extensions.Hosting;

namespace WebPareser {
	class Program {

		private static DatabaseContext context;
        private static ILoggerFactory loggerFactory;
        private static ILogger logger;
		
		static async Task Main(string[] args) {

            // Подключение к файлу конфигурации
            // using IHost host = Host.CreateDefaultBuilder(args).Build();
            // host.Run();

            // Добавление логгера
            loggerFactory = LoggerFactory.Create(config =>
                {config.AddConsole();});

            logger = loggerFactory.CreateLogger<Program>();

            context = new DatabaseContext();
            context.Database.EnsureCreated();


            // Контроллер входящих параметров
            Parser.Default.ParseArguments<ScanPageDataVerb, CreateMapVerb, ScanPageLinksVerb>(args)
                .MapResult(
                    (ScanPageDataVerb options) => ScanPageData.Run(options, context, logger),
                    (CreateMapVerb options) => CreateMap.Run(options, context, logger),
                    (ScanPageLinksVerb options) => ScanPageLinks.Run(options, context, logger),
                    error => 1
                );


            // Some dev branch code here
            // Additional code 
            // Main branch new comment

           /* logger.LogInformation("Отчистка базы данных");
            context.Pages.RemoveRange(context.Pages);
            context.PageGroups.RemoveRange(context.PageGroups);
            context.SaveChanges();
            logger.LogInformation("Отчистка базы данных завершена");

            WebScanner scanner = new WebScanner(logger);

            logger.LogInformation("Сканирование начальной страницы");
            List<PageGroup> pageGroups = scanner.ScanMainPage();
            logger.LogInformation("Сканирование начальной страницы завершено");

            logger.LogInformation("Сохранение данных в БД");
            context.AddRange(pageGroups);
            context.SaveChanges();
            logger.LogInformation("Сохранение данных в БД завершено");

            List<Page> pages = context.Pages.ToList();

            logger.LogInformation("Сканирование вложенных страниц");
            scanner.ScanPagesLinksTree(pages, WebScanner.HEADER_PAGES);
            logger.LogInformation("Сканирование вложенных страниц завершенно");

            pages.All(pages => pages.DeadEnd = false);

            logger.LogInformation("Повторное сканирование ссылок в контенте");
            scanner.ScanPagesLinksTree(pages, WebScanner.PAGE_CONTENT_LINKS);
            logger.LogInformation("Повторное сканирование ссылок в контенте завершено");

            List<Page> dbPages = context.Pages.ToList();
            foreach (Page page in dbPages)
                pages.RemoveAll(o => o == page);

            logger.LogInformation("Сохранение данных в БД");
            context.Pages.AddRange(pages);
            context.SaveChanges();
            logger.LogInformation("Сохранение данных в БД завершено");

            logger.LogInformation("Взятие данных из базы");
            List<PageGroup> pageGroupsList = context.PageGroups.Include(o => o.Pages).OrderBy(o => o.Name).ToList();
            foreach (var pg in pageGroupsList)
                foreach (var p in pg.Pages)
                    p.ChildPages = AddSubpages(p);
            logger.LogInformation("Взятие данных из базы завершено");

            logger.LogInformation("Сортировка данных");
            PageGroup um = pageGroupsList.FirstOrDefault(o => o.Name == "ВЕРХНЕЕ МЕНЮ");
            pageGroupsList.Remove(um);
            pageGroupsList.Insert(0, um);
            logger.LogInformation("Сортировка данных завершена");

            FileWriter writer = new FileWriter();
            writer.CreateHTMLMap(pageGroupsList);*/
        }

        // public static List<Page> AddSubpages(Page page)
        // {
        //     List<Page> pages = context.Pages.Where(o => o.ParentPageId == page.Id).ToList();

        //     foreach (Page p in pages)
        //         p.ChildPages = AddSubpages(p);

        //     return pages;
        // }
    }
}
