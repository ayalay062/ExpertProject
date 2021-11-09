using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Project.DAL.Models
{
    public partial class User
    {
        public User()
        {
            Meetings = new HashSet<Meeting>();
            Recommends = new HashSet<Recommend>();
        }
        [Key]
        [Required]
        public int id { get; set; }
        [Required]
        public string userName { get; set; }
        [Required]
        public string userPassword { get; set; }
        [Required]
        public string email { get; set; }
        public int cityId { get; set; }
        [Required]
        public int userType { get; set; }
        public string imgUrl { get; set; }
        //public double lat { get; set; }
        //public double lng { get; set; }
        //[Required]
        //public string address { get; set; }
        [ForeignKey("cityId")]
        public virtual City City { get; set; }
        public virtual ICollection<Meeting> Meetings { get; set; }
        public virtual ICollection<Recommend> Recommends { get; set; }
    }
}
