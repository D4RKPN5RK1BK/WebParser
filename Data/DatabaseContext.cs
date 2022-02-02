using System;
using Microsoft.EntityFrameworkCore;
using WebParser.Models;
using static System.Environment;

namespace WebPareser.Data
{
	public class DatabaseContext : DbContext {
		public DbSet<Page>? Pages {get;set;}

		public DbSet<Document>? Documents {get;set;}

		public DbSet<PageGroup> PageGroups { get; set; }
		
		public string DbPath { get; set; }
		
		protected override void OnConfiguring(DbContextOptionsBuilder builder) {
			
			SpecialFolder folder = Environment.SpecialFolder.LocalApplicationData;
			string path = Environment.GetFolderPath(folder);

			DbPath = Path.Join(path, "WebParser.db");

			builder.UseSqlite($"DataSource={DbPath}");
		} 

	}
}