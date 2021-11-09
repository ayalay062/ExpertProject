using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Project.DAL.Models
{
    public partial class Recommend
    {
        [Key]
        [Required]
        public int id { get; set; }
        [Required]
        public int profId { get; set; }
        [Required]
        public int userId { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string content { get; set; }
        [Required]
        public bool isApproved { get; set; }

        public DateTime date_posted { get; set; }
        public int stars { get; set; }
        [ForeignKey("userId")]

        public virtual User User { get; set; }
        [ForeignKey("profId")]

        public virtual Expert Expert { get; set; }
    }
}
