using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class RecommendDTO
    {
        public int id { get; set; }
        public int profId { get; set; }
        public int userId { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public bool isApproved { get; set; }

        public DateTime date_posted { get; set; }
        public int stars { get; set; }
          public  UserDTO User { get; set; }

        public  ExpertDTO Expert { get; set; }
    }
}
