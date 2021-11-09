using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Project.DAL.Models
{
    public partial class Expert
    {
        public Expert()
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
        public string businessName { get; set; }
        public int cityId { get; set; }
        public string imgUrl { get; set; }
        public string phone { get; set; }
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        public int proSubject { get; set; }
        public string description { get; set; }
        [Required]
        public bool enable { get; set; }
        [ForeignKey("cityId")]

        public virtual City City { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual ICollection<Meeting> Meetings { get; set; }
        public virtual ICollection<Recommend> Recommends { get; set; }
    }
}
