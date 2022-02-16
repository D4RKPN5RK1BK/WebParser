using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace WebParser.Models
{
    public class Page
    {
        private string _legasyURL;

        [Required]
        public string? Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? NormalizedName { get; set; }

        public string? Meta { get; set; }

        public string? Description { get; set; }

        public string? Content { get; set; }
        
        public string? LegasyURL
        {
            get => _legasyURL;
            set
            {
                _legasyURL = value;
                LegasyPath = Regex.Match(_legasyURL, @"^\S*/").Value;
            }
        }

        public string? LegasyPath { get; set; }

        public string? LegasyContent { get; set; }

        [Required]
        public bool IsLegasy { get; set; }

        [Required]
        public bool IsArchive { get; set; }

        [Required]
        public DateTime? Created { get; set; }

        [Required]
        public DateTime? LastModified { get; set; }

        [Required]
        public bool IsConfirmed { get; set; }

        public string? ParentPageId { get; set; }

        [NotMapped]
        public Page? ParentPage { get; set; }

		[NotMapped]
		public List<Page> ChildPages { get; set; }

        public string? GroupId { get; set; }

        public PageGroup? PageGroup { get; set; }

        public List<Document>? PageFiles { get; set; }

        [NotMapped]
        public bool DeadEnd { get; set; }

        public Page()
        {
            DeadEnd = false;
            Id = Guid.NewGuid().ToString();
            Created = DateTime.Now;
            LastModified = DateTime.Now;
            PageFiles = new List<Document>();
            ChildPages = new List<Page>();
        }

        public Page(string name, string legasyURL = "", string parentPageId = null, DateTime? created = null, DateTime? lastModified = null, string? content = null, string? legasyContent = null, string? description = null)
        {
            Id = Guid.NewGuid().ToString();
            DeadEnd = false;
            Name = name;
            NormalizedName = name.Normalize().ToUpper();
            LegasyURL = legasyURL;
            Content = content;
            LegasyContent = legasyContent;
            Description = description;
            Created = created ?? DateTime.Now;
            LastModified = lastModified ?? DateTime.Now;
            PageFiles = new List<Document>();
            ChildPages = new List<Page>();
            ParentPageId = parentPageId;
        }

        [NotMapped]
        public List<Page> FullSubpageList
        {
            get
            {
                List<Page> list = new List<Page>();
                foreach (Page page in ChildPages)
                    list.AddRange(FullSubpageList);
                return list;
            }
        }
    }
}