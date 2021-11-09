using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAPI.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        ILoginBL _loginBL;
        public LoginController(ILoginBL loginBL)
        {
            _loginBL = loginBL;
        }

        [HttpPost]
        [Route("Login")]

        public LoginResponse Login([FromBody] UserDTO user)
        {
            
              return  _loginBL.Login(user);
            

        }

    }
}
