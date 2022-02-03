using WebPareser.Data;
using AngleSharp;
using AngleSharp.Dom;
using WebParser.Models;

namespace WebPareser.Scanner {
	public class WebScanner {
		private Dictionary<bool, Page> pageCheckList;
		private List<PageGroup> pageGroups;
		private DatabaseContext _databaseContext;
		private IConfiguration _configuration;
		private IBrowsingContext _browserContext;
		private string _address;
		private IDocument _document;
		public WebScanner(DatabaseContext databaseContext) {
			_databaseContext = databaseContext;
			_configuration = Configuration.Default.WithDefaultLoader();
			_address = "http://goreftinsky.ru";
			_browserContext = BrowsingContext.New(_configuration);
			_document = _browserContext.OpenAsync(_address).Result;
		}

		public void ScanAllPageLinks()
        {
			pageCheckList = new Dictionary<bool, Page>();
        }

		public void ScanPageGroups()
        {
			
			pageGroups = new List<PageGroup>();
			pageGroups.Add(new PageGroup()
			{
				Id = Guid.NewGuid().ToString(),
				Name = "Верхнее меню",
				Created = DateTime.Now,
				Updated = DateTime.Now
			}
			);

			string query = "#header3 div div";
			IHtmlCollection<IElement> divs = _document.QuerySelectorAll(query);

			for (int i = 0; i < divs.Count(); i+=2)
            {
				if(!_databaseContext.PageGroups.Any(o => o.Name == divs[i].TextContent))
                {
					PageGroup tempGroup = new PageGroup()
					{
						Id = Guid.NewGuid().ToString(),
						Name = divs[i].TextContent,
						Created = DateTime.Now,
						Updated = DateTime.Now,
						Pages = new List<Page>()
					};


					foreach (var n in divs[i + 1].ChildNodes)
					{
						if (!_databaseContext.Pages.Any(o => o.Name == n.TextContent))
                        {
							tempGroup.Pages.Add(new Page()
							{
								Id = Guid.NewGuid().ToString(),
								Name = n.TextContent,
								Created = DateTime.Now,
								LastModified = DateTime.Now
							});
						}
						
					}

					pageGroups.Add(tempGroup);
				}
				
			}

			_databaseContext.AddRange(pageGroups);
			_databaseContext.SaveChanges();

        }

		public void ScanPageLinks(string path = "")
        {

        }

		public void ScanPage(string path = "/") {
			

			string query = "#header2_right a";
			IHtmlCollection<IElement> headers = _document.QuerySelectorAll(query);
			foreach(IElement header in headers) {
				Console.WriteLine(header.TextContent);
				Console.WriteLine(header.Attributes["href"]?.TextContent);
			}

		}

		public void Scan(string page = "/") {

		}

		public void ScanPageDoc(string page = "/") {

		}

		public void ScanPageFiles(string page = "/")
        {

        }

	}
}