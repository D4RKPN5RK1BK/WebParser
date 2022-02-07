using WebPareser.Data;
using AngleSharp;
using AngleSharp.Dom;
using WebParser.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace WebPareser.Scanner {
	
	public class WebScanner {

		private IConfiguration configuration;
		private IBrowsingContext browserContext;
		private string address;
		private IDocument document;

		private const string HEADER_PAGES_QUERY = "#header2_right a";
		private const string PAGE_GROUPS_QUERY = "#header3 div div";

		private const string MAIN_PAGE = "/";

		public WebScanner() {
			configuration = Configuration.Default.WithDefaultLoader();
			address = "http://goreftinsky.ru";
			browserContext = BrowsingContext.New(configuration);
			document = browserContext.OpenAsync(address).Result;
		}

		public List<PageGroup> ScanMainPage()
        {
			document = browserContext.OpenAsync(address + MAIN_PAGE).Result;
			string documentPath = Regex.Match(document.Url, @"^\S*/").Value;
			List<PageGroup> pageGroups = new List<PageGroup>();

			// Top menu section
			var headerGroup = new PageGroup("бепумее лемч");
			var headerLinks = document.QuerySelectorAll(HEADER_PAGES_QUERY);

			foreach (var link in headerLinks)
				headerGroup.Pages.Add(new Page(link.TextContent, documentPath + link.GetAttribute("href")));
			
			pageGroups.Add(headerGroup);

			// Bottom menu section
			var divs = document.QuerySelectorAll(PAGE_GROUPS_QUERY);
			foreach (var div in divs)
				if (div.GetAttribute("id") == "zag")
					pageGroups.Add(new PageGroup(div.TextContent));
				else
					foreach(var link in div.Children)
						pageGroups.Last().Pages.Add(new Page(link.TextContent, documentPath + link.GetAttribute("href")));

			return pageGroups;
        }

		public List<Page> ScanPage(Page page)
        {
			document = browserContext.OpenAsync(page.LegasyURL).Result;
			var links = document.QuerySelectorAll(HEADER_PAGES_QUERY);

			foreach (var link in links)
				page.ChildPages.Add(new Page(link.TextContent, page.LegasyPath + link.GetAttribute("html"), page.Id));

			return page.ChildPages;
        }

		public List<Page>ScanPagesRangeBranch(List<Page> pages)
        {
			var tempPages = new List<Page>();

			foreach(var p in pages)
				tempPages.AddRange(ScanPage(p)
					.Where(o => !pages.Any(n => n.Name == o.Name) && !tempPages.Any(n => n.Name == o.Name)));

			foreach (var p in tempPages)
				Console.WriteLine("\t" + p.Name);

			pages.AddRange(tempPages);
			
			if (tempPages.Count > 0)
				pages.AddRange(ScanPagesRangeBranch(pages));

			return pages;
		}
	}
}