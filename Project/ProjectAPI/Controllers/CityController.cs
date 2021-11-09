using DTO;
using Microsoft.AspNetCore.Mvc;
using Project.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectAPI.Controllers
{
    [Route("api/City")]
    [ApiController]
    public class CityController : ControllerBase
    {
        ICityBL _cityBL;
        public CityController(ICityBL _cityBLl)
        {
            _cityBL = _cityBLl;
        }

        [HttpGet]
        [Route("GetAllCities")]
        public List<CityDTO> GetAllCities()
        {

            return _cityBL.GetAll();


        }





    }
}
