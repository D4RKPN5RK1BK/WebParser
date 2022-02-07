using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebParser.Models
{
    public class PageGroup
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string? Name { get; set; }

        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime LastModified { get; set; }

        public List<Page> Pages { get; set; }

        public PageGroup()
        {
            Id = Guid.NewGuid().ToString();
            Created = DateTime.Now;
            LastModified = DateTime.Now;
            Pages = new List<Page>();
        }

        public PageGroup(string name, DateTime? created = null, DateTime? lastModified = null)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Created = created ?? DateTime.Now;
            LastModified = lastModified ?? DateTime.Now;
            Pages = new List<Page>();
        }

    }
}
