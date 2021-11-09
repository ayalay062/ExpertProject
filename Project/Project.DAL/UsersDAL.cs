using Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DAL
{
    public class UserDAL: IUserDAL
    {
        ProjectContext _userContext;
        
        public UserDAL(ProjectContext userContext)
        {
            _userContext = userContext;
           
        }
        public User  AddUser(User user)
        {
            try
            {
                _userContext.Users.Add(user);
                _userContext.SaveChanges();
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteUser(int id)
        {
            try
            {
                User c = _userContext.Users.Where(i => i.id == id).FirstOrDefault();
                _userContext.Users.Remove(c);
                _userContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<User> GetAllUser()
        {
            try
            {
                var users = _userContext.Users.ToList();
              
                return users; 
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public User GetUserById(int id)
        {
            try
            {
                var users = _userContext.Users.FirstOrDefault(x=>x.id== id);

                return users;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public User UpdateUser(User user)
        {
            try
            {
              
                User c = _userContext.Users.Where(u => u.id == user.id).FirstOrDefault();
                c.userName = user.userName;
                c.userPassword = user.userPassword;
                c.imgUrl = user.imgUrl;
                c.cityId = user.cityId;

                _userContext.SaveChanges();
                return c;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
