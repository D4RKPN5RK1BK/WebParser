using WebPareser.Data;
using AngleSharp;
using AngleSharp.Dom;
using WebParser.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

namespace WebPareser.Scanner {
	
	public class WebScanner {

		private IConfiguration configuration;
		private IBrowsingContext browserContext;
		private string address;
		private ILogger logger;
		private IDocument document;

		public const string HEADER_PAGES = "#header2_right a";
		public const string PAGE_GROUPS = "#header3 div div";
		public const string PAGE_CONTENT_LINKS = ".content .page-links a";

		private const string MAIN_PAGE = "/";

		public WebScanner(ILogger logger = null) {
			this.logger = logger;
			configuration = Configuration.Default.WithDefaultLoader();
			address = "http://goreftinsky.ru";
			browserContext = BrowsingContext.New(configuration);
			document = browserContext.OpenAsync(address).Result;
		}

		/// <summary>
		/// Сканирование ссылок на главной странице, определяет группы страниц, и задает начальные страницы для дальнейшего поиска
		/// </summary>
		/// <returns>Группы страниц внутри которых находятся все ссылки на главной странице</returns>
		public List<PageGroup> ScanMainPage()
        {
			document = browserContext.OpenAsync(address + MAIN_PAGE).Result;
			string documentPath = Regex.Match(document.Url, @"^\S*/").Value;
			List<PageGroup> pageGroups = new List<PageGroup>();

			// Top menu section
			var headerGroup = new PageGroup("ВЕРХНЕЕ МЕНЮ");
			var headerLinks = document.QuerySelectorAll(HEADER_PAGES);

			foreach (var link in headerLinks)
				headerGroup.Pages.Add(new Page(link.TextContent, documentPath + link.GetAttribute("href")));
			
			pageGroups.Add(headerGroup);

			// Bottom menu section
			var divs = document.QuerySelectorAll(PAGE_GROUPS);
			foreach (var div in divs)
				if (div.GetAttribute("id") == "zag")
					pageGroups.Add(new PageGroup(div.TextContent));
				else
					foreach(var link in div.Children)
						pageGroups.Last().Pages.Add(new Page(link.TextContent, documentPath + link.GetAttribute("href")));

			return pageGroups;
        }

		/// <summary>
		/// Сканирует все ссылки в верхнем меню для выбраной страницы
		/// </summary>
		/// <param name="page">Страница для сканирования ссылок из верхнего меню</param>
		/// <returns>Возвращает группу дочерних страниц</returns>
		public List<Page> ScanPage(Page page, string query = HEADER_PAGES)
        {
			List<Page> pages = new List<Page>();
			
			if (page.DeadEnd)
				logger.LogWarning("\t" + page.Name);
			else
				logger.LogInformation("\t" + page.Name);

			document = browserContext.OpenAsync(page.LegasyURL).Result;
			IHtmlCollection<IElement> links = document.QuerySelectorAll(query);

			foreach (var link in links)
            {
                Page p = new Page(link.TextContent, page.LegasyPath + link.GetAttribute("href"), page.Id);

				if (link.GetAttribute("href").Contains(".ru"))
					p.LegasyURL = link.GetAttribute("href");

				while (Regex.IsMatch(p.LegasyURL, @"[^\/]*\/\.\.\/"))
					p.LegasyURL = p.LegasyURL.Replace(Regex.Match(p.LegasyURL, @"[^\/]*\/\.\.\/").Value, "");

				pages.Add(p);
            }

			return pages;
        }

		/// <summary>
		/// Сканирует полное древо ссылок верхнего меню для введеных страниц. Все внутренние ссылки добавляются к введенному массиву.
		/// </summary>
		/// <param name="pages">Группа страниц для сканировения дрвева ссылок</param>
		public void ScanPagesLinksTree(List<Page> pages, string query = HEADER_PAGES)
        {
			var tempPages =new List<Page>();
			foreach(var page in pages.Where(o => !o.DeadEnd))
			{
				tempPages.AddRange(ScanPage(page, query)
					.Where(o => !pages.Any(n => n.Name == o.Name && n.LegasyURL == o.LegasyURL)));
				page.DeadEnd = true;
            }

			foreach (var tp in tempPages)
				logger.LogInformation("Добавлена страница \t" + tp.Name);

			pages.AddRange(tempPages);
			if (tempPages.Count > 0)
				ScanPagesLinksTree(pages, query);

		}
	}
}