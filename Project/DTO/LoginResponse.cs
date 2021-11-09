using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public enum UserType
    {
        user =1,
        manager = 3,
        expert = 2

    }
   public class LoginResponse
    {

        public int id { get; set; }
        public string userName { get; set; }
        public UserType userType { get; set; }
        public string imgUrl { get; set; }
    }
}
