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
        public string Name { get; set; }

        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime Updated { get; set; }

        public Page[] Pages { get; set; }

    }
}
