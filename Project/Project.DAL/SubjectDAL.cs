using Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DAL
{
    public class SubjectDAL : ISubjectDAL
    {
        ProjectContext _subjectContext;
        public SubjectDAL(ProjectContext subjectContext)
        {
            _subjectContext = subjectContext;
        }

        public List<Subject> GetAllSubjects()
        {
            return _subjectContext.Subjects.ToList();
        }
        public List<Subject> GetAllParents()
        {
            return _subjectContext.Subjects.Where(x => x.parent == null).ToList();
        }
        public List<Subject> GetSubjectsByParentId(int id) {

            return _subjectContext.Subjects.Where(x => x.parent == id).ToList();
        }
    }
}
