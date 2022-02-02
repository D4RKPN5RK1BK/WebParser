using System;
using System.ComponentModel.DataAnnotations;

namespace WebParser.Models
{
	public class Document {
		[Required]
		public string? Id {get;set;}

		[Required]
		public string? Description {get;set;}
		[Required]
		public string? Name {get;set;}
		
		[Required]
		public string? LegasyURL {get;set;}
		
		[Required]
		public string? URL {get;set;}
		
		[Required]
		public string? Extention {get;set;}
		
		[Required]
		public DateTime Created {get;set;}
		
		[Required]
		public DateTime LastModified {get;set;}
		
		[Required]
		public string? PageId {get;set;}
		
		[Required]
		public Page? Page {get;set;}
	}
}