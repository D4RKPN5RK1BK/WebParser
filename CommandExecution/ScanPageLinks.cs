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
        private static WebScanner _scanner;

        public static int Run(ScanPageLinksVerb options, DatabaseContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
            _scanner = new WebScanner(logger);
            
            if (options.ClearDatabase)
            {
                if (options.Mode != "initial" || options.Mode != "full")
                {
                    logger.LogWarning("Clear database option can be added only to full and initial scan mode");
                    return 1;
                }

                logger.LogInformation("Database refreshing");
                context.Pages.RemoveRange(context.Pages);
                context.PageGroups.RemoveRange(context.PageGroups);
                context.SaveChanges();
                logger.LogInformation("Database refreshing complited");
            }

            switch (options.Mode)
            {
                case "full":
                    InitialModelScan();
                    AllModeScan();
                    break;
                case "single":
                    SingleModeScan();
                    break;
                case "initial":
                    InitialModelScan();
                    break;
                case "all":
                    AllModeScan();
                    break;
            }

            return 0;

        }

        private static void SingleModeScan()
        {

        }

        private static void InitialModelScan()
        {
            _logger.LogInformation("Scanning root page");
            List<PageGroup> pageGroups = _scanner.ScanMainPage();
            _logger.LogInformation("Scanning root page complited");

            _logger.LogInformation("Saving data to database");
            _context.AddRange(pageGroups);
            _context.SaveChanges();
            _logger.LogInformation("Saving data to database completed");
        }

        private static void AllModeScan()
        {
            List<Page> pages = _context.Pages.ToList();
            _logger.LogInformation("Scanning all inner pages");
            _scanner.ScanPagesLinksTree(pages, WebScanner.HEADER_PAGES);
            _logger.LogInformation("Scanning all inner pages completed");

            /*List<Page> dbPages = context.Pages.ToList();
            foreach (Page page in dbPages)
                pages.RemoveAll(o => o == page);

            logger.LogInformation("Сохранение данных в БД");
            context.Pages.AddRange(pages);
            context.SaveChanges();
            logger.LogInformation("Сохранение данных в БД завершено");*/
        }
    }
}
