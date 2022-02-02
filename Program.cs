using WebPareser.Data;
using System;
using AngleSharp;
using WebPareser.Scanner;
using WebParser.Models;

namespace WebPareser {
	class Program {

		private static DatabaseContext? _context;
		
		static void Main() {
            WebScanner scanner = new WebScanner(new DatabaseContext());
            scanner.ScanPage("/");

			
		}
	}
}
