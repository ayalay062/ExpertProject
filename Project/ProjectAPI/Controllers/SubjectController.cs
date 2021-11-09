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
    [Route("api/Subjects")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        ISubjectBL _subjectBL;

        public SubjectController(ISubjectBL subjectBL)
        {
            _subjectBL = subjectBL;
        }

        [Route("GetAll")]
        [HttpGet]
        public List<SubjectDTO> GetAllSubjects()
        {
            return _subjectBL.GetAllSubjects();
        }


        [Route("GetSubjectsByParentId/{id}")]
        [HttpGet]
        public List<SubjectDTO> GetSubjectsByParentId(int id)
        {
            return _subjectBL.GetSubjectsByParentId(id);
        }


        [Route("GetAllParents")]
        [HttpGet]
        public List<SubjectDTO> GetAllParents()
        {
            return _subjectBL.GetAllParents();
        }

        // GET api/<SubjectController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SubjectController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SubjectController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SubjectController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
