using AutoMapper;
using DTO;
using Project.DAL;
using Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.BL
{
    public class UserBL : IUserBL
    {
        IUserDAL _userDAL;
        IMapper _mapper;

        public UserBL(IUserDAL userDAL)
        {
            _userDAL = userDAL;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            _mapper = config.CreateMapper();
        }

        public UserDTO AddUser(UserDTO user)
        {
          
            var u = _mapper.Map<User>(user);
           var userAdded=  _userDAL.AddUser(u);
            return _mapper.Map<User, UserDTO>(userAdded);
        }

       

        public void DeleteUser(int id)
        {
            _userDAL.DeleteUser(id);
        }

        public List<UserDTO> GetAllUser()
        {
            var users = _userDAL.GetAllUser();
           var userDTOList = new List<UserDTO>();

            foreach (var u in users)
            {
                userDTOList.Add(_mapper.Map<User, UserDTO>(u));
            //    userDTOList.Add(new UserDTO()
            //    {
            //        AddressId = (int)item.AddressId,
            //        UserId = item.UserId,
            //        FirstName = item.FirstName,
            //        LastName = item.LastName,
            //        Mail = item.Mail,
            //        Password = item.Password.ToString(),
            //        Phone = item.Phon,
            //        Telephone = item.Telephon,
            //        UserName = item.UserName,
            //    });
            }
            //return userDTOList;
            ////List<User> users = _userDAL.GetAllUser();
            return userDTOList;        
        }

        public UserDTO UpdateUser(UserDTO user)
        {
            var u = _mapper.Map<UserDTO, User>(user);
            var userAdded = _userDAL.UpdateUser(u);
            return _mapper.Map<User, UserDTO>(userAdded);
        }
    }
}
