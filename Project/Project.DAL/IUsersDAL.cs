using Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL
{
    public interface IUserDAL
    {
        List<User> GetAllUser();
        User AddUser(User user);
        User GetUserById(int id);
        User UpdateUser(User user);
        bool DeleteUser(int id);
    }
}
