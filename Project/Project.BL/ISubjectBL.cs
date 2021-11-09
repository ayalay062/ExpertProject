using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.BL
{
    public interface ISubjectBL
    {
        public List<SubjectDTO> GetAllSubjects();
        public List<SubjectDTO> GetAllParents();
        public List<SubjectDTO> GetSubjectsByParentId(int id);
    }
    
}
