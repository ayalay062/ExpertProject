using AutoMapper;
using DTO;
using Project.DAL;
using Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.BL
{
    public class SubjectBL : ISubjectBL
    {
        ISubjectDAL _subjectDAL;
        IMapper _mapper;
        public SubjectBL(ISubjectDAL subjectDAL)
        {
            _subjectDAL = subjectDAL;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            _mapper = config.CreateMapper();
        }

        public List<SubjectDTO> GetAllSubjects()
        {
            var subjects = _subjectDAL.GetAllSubjects();
            var lSubjects = new List<SubjectDTO>();
            foreach (var s in subjects)
            {
                lSubjects.Add(_mapper.Map<Subject, SubjectDTO>(s));
            }
            return lSubjects;
        }
        public List<SubjectDTO> GetAllParents()
        {
            var subjects = _subjectDAL.GetAllParents();
            var lSubjects = new List<SubjectDTO>();
            foreach (var s in subjects)
            {
                lSubjects.Add(_mapper.Map<Subject, SubjectDTO>(s));
            }
            return lSubjects;
        }
        public List<SubjectDTO> GetSubjectsByParentId(int id) {

            var subjects = _subjectDAL.GetSubjectsByParentId(id);
            var lSubjects = new List<SubjectDTO>();
            foreach (var s in subjects)
            {
                lSubjects.Add(_mapper.Map<Subject, SubjectDTO>(s));
            }
            return lSubjects;
        }
    }
}
