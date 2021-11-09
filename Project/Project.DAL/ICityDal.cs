using Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL
{
    public interface ICityDal
    {
        List<City> GetAll();
    }
}
