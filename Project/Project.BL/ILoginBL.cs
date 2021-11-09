using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.BL
{
   public interface ILoginBL
    {
        LoginResponse Login(UserDTO user);
    }
}
