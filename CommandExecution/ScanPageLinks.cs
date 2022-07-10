using Microsoft.Extensions.Logging;
using WebPareser.Data;
using WebPareser.Models;
using WebParser.Models;
using WebParser.Scanner;
using WebParser.CommandVerbs;
using Microsoft.Data.Sqlite;

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
                ClearDatabase();

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


            // Сканирование нижнего меню главной страницы
            var pageGroups = _scanner.ScanMainPageGroups();
            PageGroups.AddRange(pageGroups);
            foreach (var pg in pageGroups)
            {
                PageGroups.Add(pg);
                foreach (var p in pg.Pages)
                    Pages.Add(p);
            }

            // Поиск и сканирование всех вложенных страниц
            for (int i = 0; i < Pages.Elements.Count(); i++)
            {
                ScanPage(Pages.Elements[i]);
            }

            _logger.LogWarning($"Количество найденых страниц\t: {Pages.Elements.Count()}");
            _logger.LogWarning($"Количество найденых груп\t: {PageGroups.Elements.Count()}");

            UpdateDatabase();

            return 0;

        }


        /// <summary>
        /// Добавляет все найденные ссылки в заголовке страницы
        /// </summary>
        private static void ScanPage(Page page)
        {
            List<Page> subpages = _scanner.ScanPageLinks(page) as List<Page>;
            Pages.AddRange(subpages);
        }

        /// <summary>
        /// Удаляет всю информацию из базы данных о страницах и их группах
        /// </summary>
        private static void ClearDatabase()
        {
            _logger.LogInformation("Отчистка базы данных...");
            try
            {
                _context.RemoveRange(_context.Pages);
                _context.RemoveRange(_context.PageGroups);
                _context.SaveChanges();
            }
            catch (SqliteException ex)
            {
                _logger.LogCritical($"При отчистке базы данных произошла ошибка:\n{ex}");
            }
            _logger.LogInformation("Отчистка базы данных успешно завершена.");

        }

        /// <summary>
        /// Сохраняет всю информацию о страницах и их группах в базе данных
        /// </summary>
        private static void UpdateDatabase()
        {
            _logger.LogInformation("Добавление элементов в базу данных...");
            try
            {
                _context.AddRange(Pages.Elements);
                _context.AddRange(PageGroups.Elements);
                _context.SaveChanges();
            }
            catch (SqliteException ex)
            {
                _logger.LogCritical($"При сохранении данных произошла ошибка:\n{ex}");
            }
            _logger.LogInformation("Добавление элементов в базу данных успешно завершено");
        }
    }
}
