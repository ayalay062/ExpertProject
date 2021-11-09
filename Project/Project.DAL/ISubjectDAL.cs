using Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL
{
    public interface ISubjectDAL
    {
        public List<Subject> GetAllSubjects();
        public List<Subject> GetSubjectsByParentId(int id);
        public List<Subject> GetAllParents();

        //void UpdateExpertsProfession(int id, ExpertsProfessionDTO expertsProfession);
        //void DeleteExpertsProfession(int id);
        //void AddExpertsProfession(ExpertsProfessionDTO expertProfession);
    }
}
