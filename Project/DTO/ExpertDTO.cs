using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class ExpertDTO
    {
        public int id { get; set; }
        public string userName { get; set; }
        public string userPassword { get; set; }
        public string email { get; set; }
        public string businessName { get; set; }
        public int cityId { get; set; }
        public string imgUrl { get; set; }
        public string phone { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int proSubject { get; set; }
        public string description { get; set; }
        public bool enable { get; set; }
        public double scores { get; set; }
        public int numRecommends { get; set; }

        
        public SubjectDTO Subject { get; set; }
        public CityDTO City { get; set; }

    }
}
