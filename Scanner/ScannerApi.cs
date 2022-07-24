using System.Net;
using System.Text.RegularExpressions;
using AngleSharp;
using AngleSharp.Dom;
using Microsoft.Extensions.Logging;
using WebParser.Models;

namespace WebParser.Scanner
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
        public const string PAGE_CONTENT_LINKS = ".page-links a";

        // Корневой каталог
        public const string ADDRESS = "http://goreftinsky.ru";

        // Контент страницы
        public const string PAGE_CONTENT = "#main";

        // Заголовок страницы
        public const string PAGE_HEADER = "#header_data";

        // Полседняя дата обновление страницы
        public const string PAGE_UPDATE = "#actual";

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
        private HttpClient _webClient;

        public ScannerApi(ILogger logger)
        {
            _logger = logger;
            config = Configuration.Default.WithDefaultLoader();
            address = ADDRESS;
            browserContext = BrowsingContext.New(config);
            document = browserContext.OpenAsync(address).Result;
            _webClient = new HttpClient();
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
                    if (!Regex.IsMatch(link.GetAttribute("href"), @"http:\/\/|.pdf"))
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
                    {
                        foreach (var link in s.Children)
                            if (!Regex.IsMatch(link.GetAttribute("href"), @"http:\/\/|.pdf"))
                                pageGroups.Last().Pages.Add(new Page(link.TextContent, path + link.GetAttribute("href"), pageGroups.Last().Id));
                    }
                    
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

            _logger.LogInformation(page.LegasyURL);

            try
            {
                document = browserContext.OpenAsync(page.LegasyURL!).Result;
                var links = document.QuerySelectorAll($"{HEADER_PAGES}");

                foreach (var link in links)
                {
                    if (!Regex.IsMatch(link.GetAttribute("href"), @"http:\/\/|.pdf"))
                    {
                        Page p = new Page(link.TextContent, page.LegasyPath + link.GetAttribute("href"), page.PageGroupId!, page.Id!);

                        while (Regex.IsMatch(p.LegasyURL!, @"[^\/]*\/\.\.\/"))
                            p.LegasyURL = p.LegasyURL!.Replace(Regex.Match(p.LegasyURL, @"[^\/]*\/\.\.\/").Value, String.Empty);

                        pages.Add(p);
                    }
                }

                var contentLinks = document.QuerySelectorAll($"{PAGE_CONTENT_LINKS}");
                foreach (var link in contentLinks)
                {
                    if (link.HasAttribute("href"))
                    {
                        // _logger.LogWarning(link.InnerHtml);
                        Page p = new Page(link.TextContent, page.LegasyPath + link.GetAttribute("href"), page.PageGroupId!, page.Id!);

                        while (Regex.IsMatch(p.LegasyURL!, @"[^\/]*\/\.\.\/"))
                            p.LegasyURL = p.LegasyURL!.Replace(Regex.Match(p.LegasyURL, @"[^\/]*\/\.\.\/").Value, String.Empty);

                        pages.Add(p);
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Сканирование ссылок на страницке завершилось с ошибкой!\nИмя:{page.LinkName}\nСсылка:{page.LegasyURL}\nExceptionMessage:{ex.Message}");
            }



            return pages;
        }

        /// <summary>
        /// Возвращает передоваемую страницу с обновленным контентом (заголовок, дата обновления, содержание итд)
        /// </summary>
        public Page ScanPageContent(Page page)
        {
            _logger.LogInformation($"Сканирование \"{page.LinkName}\".");
            try
            {
                document = browserContext.OpenAsync(page.LegasyURL).Result;
                var meta = document.QuerySelectorAll("meta");
                string tags = "";
                string description = "";
                foreach (var m in meta)
                {
                    if (m.GetAttribute("name") == "Keywords")
                        tags = m.GetAttribute("content");

                    if (m.GetAttribute("name") == "Description")
                        description = m.GetAttribute("content");
                }

                page.LegasyContent = document.QuerySelector(PAGE_CONTENT).InnerHtml.Trim();
                page.Header = document.QuerySelector(PAGE_HEADER).TextContent.Trim();
                page.Title = document.Title;
                page.Tags = tags.Trim();
                page.Description = description.Trim();
            }
            catch (NullReferenceException ex)
            {
                _logger.LogCritical($"\nОшибка при сканировании данных!\nИмя:{page.LinkName}\nСсылка:{page.LegasyURL}\nExceptionMessage:{ex.Message}");
                page.LegasyContent = $"<p>Во время сканирования данных произошла ошибка.</p><p>Рекомендуется изменить теги сканируемой страницы согласно настройкам сканнера, после чего повторить сканирование. Или вставить контент вручную</p>";
                page.Header = "Страница " + page.Id;
                page.Title = page.Id;
                page.Tags = "Неизвестно";
                page.Description = "Неизвестно";
            }
            return page;

        }

        // Слишком много всего на одну функцию!!!
        /// <summary>
        /// Сканирует выбранную страницу и сохраняет все файлы страницы в папку с названием id страницы
        /// </summary>
        public Page ScanPageFiles(Page page)
        {
            _logger.LogInformation($"Копирование файлов из \"{page.Header}\" id: {page.Id}");
            try
            {
                document = browserContext.OpenAsync(page.LegasyURL).Result;
                var pageContent = document.QuerySelector(PAGE_CONTENT);
                var pageLinks = pageContent.QuerySelectorAll($"a");
                var pageImages = pageContent.QuerySelectorAll("img");
                var pageFileDir = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/GoreftinskyData/Files/Pages/{page.Id}";
                var pageFileLinks = pageLinks.Where(o => Regex.IsMatch(o.GetAttribute("href"), @"(\.pdf|\.doc|\.xlsx|\.odt|\.docx|\.pptx|\.jpg|\.png|\.jpeg|\.zip|\.rar|\.7z)$"));
                var pageImagesLinks = pageImages.Where(o => Regex.IsMatch(o.GetAttribute("src"), @"(\.jpg|\.png|\.jpeg)$"));

                _logger.LogInformation($"\tНайдено файлов: {pageFileLinks.Count()}");

                foreach (var l in pageFileLinks)
                {
                    var fileName = Regex.Match(l.GetAttribute("href"), @"([a-zA-Z0-9_]*)\.[a-zA-Z0-9]*$").Value;

                    _logger.LogInformation($"\n\tЗагрузка из \"{page.LegasyPath + l.GetAttribute("href")}\"\n\tВ \"{pageFileDir}/{fileName}\"");

                    DownloadFile(page.LegasyPath + l.GetAttribute("href"), page.Id);
                }
                
                foreach (var l in pageImagesLinks)
                {
                    var fileName = Regex.Match(l.GetAttribute("src"), @"([a-zA-Z0-9_]*)\.[a-zA-Z0-9]*$").Value;

                    _logger.LogInformation($"\n\tЗагрузка из \"{page.LegasyPath + l.GetAttribute("src")}\"\n\tВ \"{pageFileDir}/{fileName}\"");

                    DownloadFile(page.LegasyPath + l.GetAttribute("src"), page.Id);
                }

                for (int i = 0; i < pageLinks.Count(); i++)
                {
                    var fileName = Regex.Match(pageLinks[i].GetAttribute("href"), @"([a-zA-Z0-9_]*)\.[a-zA-Z0-9]*$").Value;
                    if (Regex.IsMatch(pageLinks[i].GetAttribute("href"), @"(\.pdf|\.doc|\.xlsx|\.odt|\.docx|\.pptx|\.jpg|\.png|\.jpeg|\.zip|\.rar|\.7z)$"))
                        pageLinks[i].SetAttribute("href", $"/Public/Pages/{page.Id}/{fileName}");
                }
                page.LegasyContentWithUpdatedFiles = pageContent.InnerHtml.ToString().Trim();

            }
            catch(NullReferenceException ex)
            {
                _logger.LogCritical($"Не удалось скопировать Файлы из \"{page.Header}\". Исключение:\n{ex.Message}");
            }
            catch(WebException ex)
            {
                _logger.LogWarning($"Не удалось получить доступ к ресурсу!");

            }

            return page;
        }

        /// <summary>
        /// Возвращает все найденные ссылки в навигаторе и контенте на странице 
        /// </summary>
        public IEnumerable<Page> ScanPageLinks(string path)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Возвращает передоваемую страницу с обновленным контентом (заголовок, дата обновления, содержание итд)
        /// </summary>
        public Page ScanPageContent(string path)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Скачивает файл и помещает его в папку выбраной страницы (используется id страницы)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="id"></param>
        /// <returns>Возвращает новую ссылку на файл.</returns>
        public async Task<string> DownloadFile(string source, string id)
        {
            _logger.LogInformation($"Загрузка файла \"{source}\"...");

            var fileInfo = Regex.Match(source, @"([a-zA-Z0-9_]*)\.[a-zA-Z0-9]*$").Value;
            var resultFileDir = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/GoreftinskyData/Files/Pages/{id}";

            try
            {
                var responce = await _webClient.GetAsync(source);
                responce.EnsureSuccessStatusCode();

                var stream = await responce.Content.ReadAsStreamAsync();
                var fileStream = File.Create($"{resultFileDir}/{fileInfo}");

                stream.CopyTo(fileStream);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Не удалось скачать\n\t{source}\n\t\"{resultFileDir}/{fileInfo}\" файл по следующей причине:\n{ex.Message}");
            }

            _logger.LogInformation($"Копирование файла \"{fileInfo}\" успешно завершено");
            return $"{resultFileDir}/{fileInfo}";
        }

    }
}