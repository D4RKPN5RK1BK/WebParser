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
        /// Возвращает все найденые группы страниц на главной странице сайта
        /// </summary>
        public IEnumerable<PageGroup> ScanMainPageGroups()
        {

            return new List<PageGroup>();
        }

        /// <summary>
        /// Возвращает все найденые группы страниц на главной странице сайта вместе со сложенными в них страницами
        /// </summary>
        public IEnumerable<PageGroup> ScanMainPageGroupsWithLinks()
        {

            return new List<PageGroup>();
        }

        /// <summary>
        /// Возвращает все найденные ссылки в навигаторе и контенте на странице 
        /// </summary>
        public IEnumerable<Page> ScanPageLinks(Page page)
        {

            return new List<Page>();
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