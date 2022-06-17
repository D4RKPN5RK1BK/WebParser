using AngleSharp;
using AngleSharp.Dom;
using Microsoft.Extensions.Logging;
using WebParser.Models;

namespace WebPareser.Scanner
{
    public class ScannerApi
    {
        private IConfiguration config;
        private IBrowsingContext browserContext;
        private string address;
        private ILogger _logger;
        private IDocument docuemnt;

        public ScannerApi(ILogger logger)
        {
            _logger = logger;
            config = Configuration.Default.WithDefaultLoader();
            address = "http://goreftinsky.ru";
            browserContext = BrowsingContext.New(config);
            docuemnt = browserContext.OpenAsync(address).Result;
        }

        /// <summary>
        /// Возвращает все ссылки из навигатора на главной странице внутри сайта
        /// </summary>
        public IEnumerable<Page> ScanMainPageLinks()
        {
            return new List<Page>();
        }

        /// <summary>
        /// Возвращает все найденые группы страниц на главной странице сайта
        /// </summary>
        public IEnumerable<PageGroup> ScanMainPageGroups()
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