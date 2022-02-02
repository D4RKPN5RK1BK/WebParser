using WebPareser.Data;
using System;
using AngleSharp;
using WebPareser.Scanner;
namespace WebPareser {
	class Program {

		private static DatabaseContext? _context;
		
		static void Main() {
			WebScanner scanner = new WebScanner(new DatabaseContext());
			scanner.ScanPage();
		}
	}
}
