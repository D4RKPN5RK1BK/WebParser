using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using WebParser.Interfaces;

namespace WebParser.Models
{
    public class Page : IHeirarchy<Page>
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
        public DateTime? Updated { get; set; }

        [Required]
        public bool IsConfirmed { get; set; }

        public string? ParentId { get; set; }

        public string? PageGroupId { get; set; }

        public PageGroup? PageGroup { get; set; }

        public IEnumerable<Document>? PageFiles { get; set; }

        public Page()
        {
            Id = Guid.NewGuid().ToString();
            Created = DateTime.Now;
            Updated = DateTime.Now;
            PageFiles = new List<Document>();
            Children = new List<Page>();
        }

        public Page(string name, string legasyURL = "", string groupId = null, string parentId = null, DateTime? created = null, DateTime? lastModified = null, string? content = null, string? legasyContent = null, string? description = null)
        {
            Id = Guid.NewGuid().ToString();
            Name = name.ToUpper().First() + name.Remove(0, 1).ToLower();
            PageGroupId = groupId;
            NormalizedName = name.Normalize().ToUpper();
            LegasyURL = legasyURL;
            Content = content;
            LegasyContent = legasyContent;
            Description = description;
            Created = created ?? DateTime.Now;
            Updated = lastModified ?? DateTime.Now;
            PageFiles = new List<Document>();
            Children = new List<Page>();
            ParentId = parentId;
        }


        public Page Parent { get; set; }
        public IEnumerable<Page> Children { get; set; }
    }
}