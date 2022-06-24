using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPareser.Data;
using WebPareser.Scanner;
using WebParser.CommandVerbs;
using WebParser.Models;

namespace WebParser.CommandExecution
{
    internal static class ScanPageLinks
    {
        private static DatabaseContext _context;
        private static ILogger _logger;
        private static ScannerApi _scanner;

        public static int Run(ScanPageLinksVerb options, DatabaseContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
            _scanner = new ScannerApi(logger);


            if (!options.Save)
            {
                logger.LogInformation("Отчистка базы данных...");
                _context.RemoveRange(_context.Pages);
                _context.PageGroups.RemoveRange(_context.PageGroups);
                _context.SaveChanges();
                logger.LogInformation("Отчистка базы данных успешно завершена.");
            }

            PageGroup headerGroup = new PageGroup("ВЕРХНЕЕ МЕНЮ");
            
            if (!_context.PageGroups.Any(o => o.Name == headerGroup.Name)) {
                _context.Add(headerGroup);
                _context.SaveChanges();
            }
            else {
                headerGroup = _context.PageGroups.First(o => o.Name == headerGroup.Name);
            }


            Page[] pages = _scanner.ScanMainPageHeaderLinks(headerGroup).ToArray();

            logger.LogInformation("Добавление ссылок с навигатора главной страницы в БД...");
            foreach(var p in pages)
                _context.AddPage(p);

            _context.SaveChanges();
            logger.LogInformation("Добавление ссылок с навигатора главной страницы в БД успешно завершено");

            return 0;

        }

        // private static void SingleModeScan()
        // {

        // }

        // private static void InitialModelScan()
        // {
        //     _logger.LogInformation("Scanning root page");
        //     List<PageGroup> pageGroups = _scanner.ScanMainPage();
        //     _logger.LogInformation("Scanning root page complited");

        //     _logger.LogInformation("Saving data to database");
        //     _context.AddRange(pageGroups);
        //     _context.SaveChanges();
        //     _logger.LogInformation("Saving data to database completed");
        // }

        // private static void AllModeScan()
        // {
        //     List<Page> pages = _context.Pages.ToList();
        //     _logger.LogInformation("Scanning all inner pages");
        //     _scanner.ScanPagesLinksTree(pages, WebScanner.HEADER_PAGES);
        //     _logger.LogInformation("Scanning all inner pages completed");

        //     /*List<Page> dbPages = context.Pages.ToList();
        //     foreach (Page page in dbPages)
        //         pages.RemoveAll(o => o == page);

        //     logger.LogInformation("Сохранение данных в БД");
        //     context.Pages.AddRange(pages);
        //     context.SaveChanges();
        //     logger.LogInformation("Сохранение данных в БД завершено");*/
        // }
    }
}
