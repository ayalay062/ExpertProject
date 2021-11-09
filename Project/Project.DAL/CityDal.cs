using Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DAL
{
    public class CityDAL : ICityDal
    {
        ProjectContext _cityContext;
        public CityDAL(ProjectContext cityContext)
        {
            _cityContext = cityContext;
        }

        public List<City> GetAll()
        {
            try
            {
                var c = _cityContext.Cities.ToList();
                return c;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
