using System.Text.RegularExpressions;
using AngleSharp;
using AngleSharp.Dom;
using Microsoft.Extensions.Logging;
using WebParser.Models;

namespace WebPareser.Scanner
{
    public class ScannerApi
    {
        /**
         *  Константы (селекторы адреса и пр.)
         **/

        // Ссылки вверху страницы
        public const string HEADER_PAGES = "#header2_right a";

        // Группы страниц внизу страницы
        public const string PAGE_GROUPS = "#header3 div div";

        // Ссылки внутри контента страницы
        public const string PAGE_CONTENT_LINKS = ".content .page-links a";

        // Корневой каталог
        public const string ADDRESS = "http://goreftinsky.ru";

        // Стартовая страница
        public const string INDEX = "/";

        // Группа стартового навигатора
        public const string NAV = "ВЕРХНЕЕ МЕНЮ";

        /**
         *  Ридонли параметры передаваемые при инициалзиции 
         **/

        private IConfiguration config;
        private IBrowsingContext browserContext;
        private string address;
        private ILogger _logger;
        private IDocument document;

        public ScannerApi(ILogger logger)
        {
            _logger = logger;
            config = Configuration.Default.WithDefaultLoader();
            address = ADDRESS;
            browserContext = BrowsingContext.New(config);
            document = browserContext.OpenAsync(address).Result;
        }

        /// <summary>
        /// Возвращает все ссылки из навигатора на главной странице внутри сайта
        /// </summary>
        public IEnumerable<Page> ScanMainPageHeaderLinks(PageGroup headerGroup)
        {
            _logger.LogInformation("Сканирование навигатора на главной странице...");
            List<Page> pages = new List<Page>();

            try
            {
                document = browserContext.OpenAsync(ADDRESS + INDEX).Result;
                string documentPath = Regex.Match(document.Url, @"^\S*/").Value;

                var headerLinks = document.QuerySelectorAll(HEADER_PAGES);

                foreach (var link in headerLinks)
                    pages.Add(new Page(link.TextContent, documentPath + link.GetAttribute("href"), headerGroup.Id));
            }
            catch (NullReferenceException ex)
            {
                _logger.LogCritical($"Сканирование навигатора на главной странице завершилось с ошибкой:\n{ex.Message}");
            }

            _logger.LogInformation("Сканирование навигатора на главной странице успешно завершено.");
            return pages;

        }

        /// <summary>
        /// Возвращает все найденые группы страниц на главной странице сайта вместе со сложенными в них страницами
        /// </summary>
        public IEnumerable<PageGroup> ScanMainPageGroups()
        {
            List<PageGroup> pageGroups = new List<PageGroup>();
            _logger.LogInformation("Сканирование нижнего меню главной страницы...");

            try
            {
                var path = Regex.Match(document.Url, @"^\S*/").Value;
                var sections = document.QuerySelectorAll(PAGE_GROUPS);

                foreach (var s in sections)
                {
                    if (s.GetAttribute("id") == "zag")
                        pageGroups.Add(new PageGroup(s.TextContent));
                    
                    if (s.GetAttribute("id") == "link")
                        foreach (var link in s.Children)
                            pageGroups.Last().Pages.Add(new Page(link.TextContent, path + link.GetAttribute("href"), pageGroups.Last().Id));
                    
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Сканирование нижней панели на главной странице завершилось с ошибкой:\n{ex.Message}");
            }
            _logger.LogInformation("Сканирование нижнего меню главной страницы успешно завершено.");
            return pageGroups;
        }

        /// <summary>
        /// Возвращает все найденные ссылки в навигаторе и контенте на странице 
        /// </summary>
        public IEnumerable<Page> ScanPageLinks(Page page)
        {
            List<Page> pages = new List<Page>();

            _logger.LogInformation(page.LegasyURL + (page.PageGroupId == null ? "\n НЕ НАЙДЕНА ГРУППА СТРАНИЦЫ" : ""));

            try
            {
                if (Regex.IsMatch(page.LegasyURL!, $@"{ADDRESS}"))
                {
                    document = browserContext.OpenAsync(page.LegasyURL!).Result;
                    IHtmlCollection<IElement> links = document.QuerySelectorAll($"{HEADER_PAGES}, {PAGE_CONTENT_LINKS}");

                    foreach (var link in links)
                    {
                        Page p = new Page(link.TextContent, page.LegasyPath + link.GetAttribute("href"), page.PageGroupId!, page.Id!);

                        if (link.GetAttribute("href")!.Contains(".ru"))
                            p.LegasyURL = link.GetAttribute("href");
                        
                        while (Regex.IsMatch(p.LegasyURL!, @"[^\/]*\/\.\.\/"))
                            p.LegasyURL = p.LegasyURL!.Replace(Regex.Match(p.LegasyURL, @"[^\/]*\/\.\.\/").Value, "");

                        // _logger.LogInformation("\t" + p.LegasyURL);
                        pages.Add(p);
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Сканирование ссылок на странице {page.LinkName} завершилось с ошибкой:\n{ex.Message}");
            }



            return pages;
        }

        /// <summary>
        /// Возвращает все найденные ссылки в навигаторе и контенте на странице 
        /// </summary>
        public IEnumerable<Page> ScanPageLinks(string path)
        {

            return new List<Page>();
        }

        /// <summary>
        /// Возвращает передоваемую страницу с обновленным контентом (заголовок, дата обновления, содержание итд)
        /// </summary>
        public Page ScanPageContent(Page page)
        {

            return new Page();
        }

        /// <summary>
        /// Возвращает передоваемую страницу с обновленным контентом (заголовок, дата обновления, содержание итд)
        /// </summary>
        public Page ScanPageContent(string path)
        {

            return new Page();
        }

    }
}