using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Project.DAL.Models
{
    public partial class Meeting
    {
        [Key]
        [Required]
        public int id { get; set; }
        public int idProf { get; set; }
        public int idUser { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public DateTime date { get; set; }
        [Required]
        public TimeSpan time { get; set; }
        [Required]
        public string content { get; set; }
        [Required]
        public bool isApproved { get; set; }
        [ForeignKey("idUser")]
        public virtual User User { get; set; }
        [ForeignKey("idProf")]
        public virtual Expert Expert { get; set; }
    }
}
