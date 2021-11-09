using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Project.DAL.Models
{
    public partial class Subject
    {
        public Subject()
        {
            Experts = new HashSet<Expert>();
        }
        [Key]
        [Required]
        public int id { get; set; }
        [Required]
        public string subName { get; set; }
        public int? parent { get; set; }

        public virtual ICollection<Expert> Experts { get; set; }
    }
}
