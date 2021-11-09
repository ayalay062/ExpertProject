using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.BL;

namespace ProjectAPI.Controllers {
    [Route ("api/Expert")]
    [ApiController]
    public class ExpertController : ControllerBase {
        IExpertBL _expertBL;
        public ExpertController (IExpertBL _expertBLl) {
            _expertBL = _expertBLl;
        }

        [HttpGet]
        [Route ("GetExperts")]
        public List<ExpertDTO> GetExperts () {

            return _expertBL.GetExperts ();
        }

        [HttpGet]
        [Route ("GetAllExperts")]
        public List<ExpertDTO> GetAllExperts () {

            return _expertBL.GetAllExperts ();
        }

        [HttpGet]
        [Route ("GetExpertById/{id}")]
        public ExpertDTO GetExpertById (int id) {

            return _expertBL.GetExpertById (id);
        }

        [HttpGet]
        [Route ("GetFilteredExperts/{category}/{subject}/{city}/{name}")]
        public List<ExpertDTO> GetFilteredExperts (int category, int subject,
            int city, string name) {

            return _expertBL.GetFilteredExperts (category, subject, city, name);
        }

        [HttpGet]
        [Route ("IsExpert/{id}")]
        public bool IsExpert (int id) {

            return _expertBL.IsExpert (id);
        }

        [HttpPost]
        [Route ("InsertExpert")]
        public ExpertDTO InsertExpert (ExpertDTO e) {

            return _expertBL.InsertExpert (e);

        }

        [HttpPost]
        [Route ("PutExpert")]
        public ExpertDTO PutExpert (ExpertDTO e) {
            return _expertBL.PutExpert (e);
        }

        [HttpPost]
        [Route ("ChangeStatus")]
        public ExpertDTO ChangeStatus (ExpertDTO e) {
            return _expertBL.ChangeStatus (e);
        }

        [HttpPost]
        [Route ("uploadImg/{id}")]
        public bool uploadImg (int id, IFormCollection formdata) {
            var files = HttpContext.Request.Form.Files;
            foreach (var file in files) {
                var uploads = Path.Combine (Directory.GetCurrentDirectory(), "Images");
                if (file.Length > 0) {
                    string FileName = System.Guid.NewGuid().ToString(); // Give file name
                    using (var fileStream = new FileStream (Path.Combine (uploads, FileName + ".png"), FileMode.Create)) {
                        file.CopyToAsync (fileStream);
                        _expertBL.uploadImg (id, FileName+ ".png");
                    }
                }
            }
            return true;
        }

    }
}