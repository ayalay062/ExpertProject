using DTO;
using Microsoft.AspNetCore.Mvc;
using Project.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAPI.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserBL _expertBL;
        public UserController(IUserBL _expertBLl)
        {
            _expertBL = _expertBLl;
        }

        [HttpGet]
        [Route("GetUsers")]
        public List<UserDTO> GetUsers()
        {

            return _expertBL.GetAllUser();
        }

        //[HttpGet]
        //[Route("GetUserById/{id}")]
        //public UserDTO GetUserById(int id)
        //{

        //    return _expertBL.GetUserById(id);
        //}
      


        [HttpPost]
        [Route("InsertUser")]
        public UserDTO InsertUser(UserDTO e)
        {

            return _expertBL.AddUser(e);


        }
        [HttpPost]
        [Route("UpdateUser")]
        public UserDTO UpdateUser(UserDTO e)
        {
            return _expertBL.UpdateUser(e);
        }
        //[HttpPost]
        //[Route("ChangeStatus")]
        //public UserDTO ChangeStatus(UserDTO e)
        //{
        //    return _expertBL.ChangeStatus(e);
        //}
     

    }
}
