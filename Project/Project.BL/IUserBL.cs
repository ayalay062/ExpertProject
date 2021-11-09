using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.BL
{
    public interface IUserBL
    {
        List<UserDTO> GetAllUser();
        UserDTO AddUser(UserDTO user);
        UserDTO UpdateUser(UserDTO user);
        void DeleteUser(int id);
    }
}
