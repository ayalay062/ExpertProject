using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Project.DAL.Models
{
    public partial class City
    {
       
        public City()
        {
            Users = new HashSet<User>();
            Experts = new HashSet<Expert>();
        
        }
 [Key]
        [Required]
        public int id { get; set; }
        [Required]
        public string name { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Expert> Experts { get; set; }

        
    }
}
