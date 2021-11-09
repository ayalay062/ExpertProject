using Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL
{
    public interface IExpertDAL
    {
        List<Expert> GetAllExperts();

        Expert GetExpertById(int id);
        Expert AddExpert(Expert ex);
        Expert UpdateExpert(Expert ex);

        //List<Expert> GetExpertsByPage(int skip, int limit);
    }
}
