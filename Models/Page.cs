using System;
using System.ComponentModel.DataAnnotations;

namespace WebPareser.Models {
	public class Page {
		[Required]
		public string? Id {get;set;}
	
		[Required]
		public string? Name {get;set;}
		
		[Required]
		public string? NormalizedName {get;set;}
		
		[Required]
		public string? Meta {get;set;}
		
		[Required]
		public string? Description {get;set;}
		
		[Required]
		public string? Content {get;set;}
		
		[Required]
		public string? LegasyContent {get;set;}
		
		[Required]
		public bool IsLegasy {get;set;}
		
		[Required]
		public bool IsArchive {get;set;}
		
		[Required]
		public DateTime? Created {get;set;}
		
		[Required]
		public DateTime? LastModified {get;set;}
		
		[Required]
		public bool IsConfirmed {get;set;}

		
		public Document[]? PageFiles {get;set;}
	}
}