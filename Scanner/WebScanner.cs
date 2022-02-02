using WebPareser.Data;
using AngleSharp;
using AngleSharp.Dom;

namespace WebPareser.Scanner {
	public class WebScanner {
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
			var task = _browserContext.OpenAsync(_address);
			_document = task.Result;
			_

		}

		public void ScanPage() {
			string query = "#header2_right a";
			IHtmlCollection<IElement> headers = _document.QuerySelectorAll(query);
			foreach(IElement header in headers) {
				Console.WriteLine(header.TextContent);
				Console.WriteLine(header.Attributes["href"]?.TextContent);
			}

		}

		public void ScanSite() {

		}

		private void ScanDoc() {

		}

		public void ReadContent() {

		}

		public void ReadContent(string page) {

		}

	}
}