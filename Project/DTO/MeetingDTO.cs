using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class MeetingDTO
    {
        public int id { get; set; }
        public int idProf { get; set; }
        public int idUser { get; set; }
        public string title { get; set; }
        public DateTime date { get; set; }
        public TimeSpan time { get; set; }
        public string content { get; set; }
        public bool isApproved { get; set; }

    }
}
