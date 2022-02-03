using WebPareser.Data;
using System;
using AngleSharp;
using WebPareser.Scanner;
using WebParser.Models;
using Microsoft.EntityFrameworkCore;

namespace WebPareser {
	class Program {

		private static DatabaseContext? _context;
		
		static void Main() {
			_context = new DatabaseContext();

			//WebScanner scanner = new WebScanner(_context);
            //scanner.ScanPageGroups();

			List<PageGroup> pageGroups = _context.PageGroups.Include(o => o.Pages).ToList();

			foreach(PageGroup pg in pageGroups)
            {
				Console.WriteLine(pg.Name);
				foreach(Page p in pg.Pages)
					Console.WriteLine("\t" + p.Name);
				Console.WriteLine();
            }
			
		}
	}
}
