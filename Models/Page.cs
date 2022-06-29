using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using WebParser.Interfaces;

namespace WebParser.Models
{
    public class Page : IHeirarchy<Page>, IComparable
    {
        private string _legasyURL;
        [Required]
        public string? Id { get; set; }

        public string? Title { get; set; }

        private string _header;
        public string? Header 
        {
            get { return _header; }
            set {
                _header = value.ToUpper().First() + value.Remove(0, 1).ToLower(); ;
                NormalizedHeader = _header.Normalize().ToUpper();
            }
        }
        public string? NormalizedHeader { get; set; }


        [Required]
        public string? LinkName { get; set; }

        public string? NormalizedLinkName { get; set; }

        public string? Tags { get; set; }

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
        public string? LegasyContentWithUpdatedFiles { get; set; }

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
            LinkName = name.ToUpper().First() + name.Remove(0, 1).ToLower();
            PageGroupId = groupId;
            NormalizedLinkName = name.Normalize().ToUpper();
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

        public int CompareTo(object? obj)
        {
            Page other = obj as Page;

            if (other.LinkName != this.LinkName || other.LegasyURL != this.LegasyURL)
                return 1;

            return 0;
        }
    }
}