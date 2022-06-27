using Microsoft.Extensions.Logging;
using WebPareser.Data;
using WebPareser.Models;
using WebParser.Models;
using WebPareser.Scanner;
using WebParser.CommandVerbs;

namespace WebParser.CommandExecution
{
    internal static class ScanPageLinks
    {
        private static DatabaseContext _context;
        private static ILogger _logger;
        private static ScannerApi _scanner;

        private static Storage<Page> Pages;
        private static Storage<PageGroup> PageGroups;


        public static int Run(ScanPageLinksVerb options, DatabaseContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
            _scanner = new ScannerApi(logger);

            Pages = new Storage<Page>();
            PageGroups = new Storage<PageGroup>();


            if (!options.Save)
            {
                logger.LogInformation("Отчистка базы данных...");
                _context.RemoveRange(_context.Pages);
                _context.RemoveRange(_context.PageGroups);
                _context.SaveChanges();
                logger.LogInformation("Отчистка базы данных успешно завершена.");
            }

            Pages.Elements = _context.Pages.ToList();
            PageGroups.Elements = _context.PageGroups.ToList();


            PageGroup headerGroup = new PageGroup("ВЕРХНЕЕ МЕНЮ");

            // Добавление группы навигатора главной
            if (!PageGroups.Check(headerGroup))
                PageGroups.Add(headerGroup);
            else
                headerGroup = PageGroups.Elements.First(o => o.Name == headerGroup.Name);

            // Сканирование навигатора на главной
            foreach (var p in _scanner.ScanMainPageHeaderLinks(headerGroup))
                Pages.Add(p);


            var pageGroups = _scanner.ScanMainPageGroups();

            PageGroups.AddRange(pageGroups);

            foreach (var pg in pageGroups)
            {
                PageGroups.Add(pg);
                foreach (var p in pg.Pages)
                    Pages.Add(p);
            }


            for (int i = 0; i < Pages.Elements.Count(); i++)
            {
                ScanPageTree(Pages.Elements[i]);
            }

            _logger.LogWarning($"Количество страниц: {Pages.Elements.Count()}");
            _logger.LogWarning($"Количество страниц: {PageGroups.Elements.Count()}");

            logger.LogInformation("Добавление элементов в базу данных...");
            _context.AddRange(Pages.Elements);
            _context.AddRange(PageGroups.Elements);
            _context.SaveChanges();
            logger.LogInformation("Добавление элементов в базу данных успешно завершено");



            return 0;

        }

        private static void ScanPageTree(Page page)
        {
            List<Page> subpages = _scanner.ScanPageLinks(page) as List<Page>;

            foreach (var p in subpages)
            {
                bool untoched = !Pages.Check(p);
                Pages.Add(p);
                if (untoched)
                    _scanner.ScanPageLinks(page);
            }
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
