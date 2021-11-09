using DTO;
using Project.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.BL
{
    public class LoginBL : ILoginBL
    {
        IExpertDAL _expertDAL;
        IUserDAL _userDAL;
        public LoginBL(IExpertDAL expertDAL, IUserDAL userDAL)
        {
            _expertDAL = expertDAL;
            _userDAL = userDAL;


        }

        public LoginResponse Login(UserDTO userDto)
        {


            var expert = _expertDAL.GetAllExperts().
              FirstOrDefault(o => o.email == userDto.email && o.userPassword == userDto.userPassword);
            if (expert != null)
                return new LoginResponse { id = expert.id, userType = UserType.expert,userName= expert .userName};

            var user = _userDAL.GetAllUser().
            FirstOrDefault(c => c.email == userDto.email && c.userPassword == userDto.userPassword);
            if (user != null)
            {
                if (user.userType == 1) return new LoginResponse { id = user.id, userType = UserType.user, userName = user.userName };
                else
                    return new LoginResponse { id = user.id, userType = UserType.manager, userName = user.userName };
            }
            return null;

        }
    }
}
