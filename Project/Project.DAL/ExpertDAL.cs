using Microsoft.EntityFrameworkCore;
using Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DAL
{
    public class ExpertDAL : IExpertDAL
    {
        ProjectContext _expertContext;
        public ExpertDAL(ProjectContext expertContext)
        {
            _expertContext = expertContext;
        }

        public List<Expert> GetAllExperts()
        {
            try
            {
                var Experts = _expertContext.Experts.Include("Subject").Include("City").ToList();
                return Experts;
            }
            catch (Exception)
            {

                throw;
            }
        }

          public List<Expert> GetAllActiveExperts()
        {
            try
            {
                var Experts = _expertContext.Experts.Where(x=>x.enable).Include("Subject").Include("City").ToList();
                return Experts;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<Expert> GetExpertsByPage(int skip, int limit)
        {
            try
            {
                var Experts = _expertContext.Experts.Include("Subject").Include("City").Skip(skip).Take(limit).ToList();
                return Experts;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Expert AddExpert(Expert ex)
        {
            try
            {
                _expertContext.Experts.Add(ex);
                _expertContext.SaveChanges();
                return ex;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Expert UpdateExpert(Expert ex)
        {
            try
            {
                var expert = _expertContext.Experts.FirstOrDefault(x => x.id == ex.id);
                expert.firstName = ex.firstName;
                expert.businessName = ex.businessName;
                expert.cityId = ex.cityId;
                expert.description = ex.description;
                expert.email = ex.email;
                expert.enable = ex.enable;
                expert.imgUrl = ex.imgUrl;
                expert.lastName = ex.lastName;
                expert.phone = ex.phone;
                expert.userName = ex.userName;
                expert.userPassword = ex.userPassword;

                _expertContext.SaveChanges();
                return ex;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Expert GetExpertById(int id) {
            {
                try
                {
                    var e = _expertContext.Experts.FirstOrDefault(x=>x.id== id);
                    return e;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

       
    }
}
