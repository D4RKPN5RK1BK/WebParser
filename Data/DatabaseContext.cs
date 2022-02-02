using WebPareser.Models;
using System;
using Microsoft.EntityFrameworkCore;

namespace WebPareser.Data
{
	public class DatabaseContext : DbContext {
		public DbSet<Page>? Pages {get;set;}
		public DbSet<Document>? Documents {get;set;}
		
		protected override void OnConfiguring(DbContextOptionsBuilder builder) {
			builder.UseMySql("server=localhost;user=root;password=;database=test", new MySqlServerVersion(new Version(5, 6, 0)));
		} 

	}
}