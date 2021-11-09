using AutoMapper;
using DTO;
using Project.DAL;
using Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.BL
{
    public class CityBL : ICityBL
    {
        ICityDal _cityDAL;
        IMapper _mapper;

        public CityBL(ICityDal cityDAL)
        {
            _cityDAL = cityDAL;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
             _mapper = config.CreateMapper();
        }

     
        public List<CityDTO> GetAll()
        {
            var citys = _cityDAL.GetAll();
            var lCities = new List<CityDTO>();
            foreach (var c in citys)
            {
                lCities.Add(_mapper.Map<City, CityDTO>(c));
            }
            return lCities;
        }

      
    }
}
