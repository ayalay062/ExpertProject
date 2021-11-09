using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DTO;
using Project.DAL;
using Project.DAL.Models;

namespace Project.BL {
    public class ExpertBL : IExpertBL {
        IExpertDAL _expertDAL;
        IRecommendsDAL _recDAL;
        IMapper _mapper;
        public ExpertBL (IExpertDAL expertDAL, IRecommendsDAL recDal) {
            _expertDAL = expertDAL;
            _recDAL = recDal;
            var config = new MapperConfiguration (cfg => {
                cfg.AddProfile<AutoMapperProfile> ();
            });
            _mapper = config.CreateMapper ();
        }

        public ExpertDTO ChangeStatus (ExpertDTO expert) {
            var ex = _expertDAL.GetExpertById (expert.id);
            ex.enable = expert.enable;
            _expertDAL.UpdateExpert (ex);
            return _mapper.Map<Expert, ExpertDTO> (ex);
        }

        public ExpertDTO GetExpertById (int id) {
            var ex = _expertDAL.GetExpertById (id);
            var exD = _mapper.Map<Expert, ExpertDTO> (ex);
            exD.scores = _recDAL.GetAvgRecommendsByExpertId (exD.id);
            exD.numRecommends = _recDAL.GetCountRecommendsByExpertId (exD.id);
            return exD;
        }

        public List<ExpertDTO> GetExperts () {

            var experts = _expertDAL.GetAllExperts ();
            var lExperts = new List<ExpertDTO> ();
            foreach (var e in experts) {
                if (e.enable == false) continue;
                var exD = _mapper.Map<Expert, ExpertDTO> (e);
                exD.scores = _recDAL.GetAvgRecommendsByExpertId (exD.id);
                exD.numRecommends = _recDAL.GetCountRecommendsByExpertId (exD.id);

                lExperts.Add (exD);
            }
            return lExperts.OrderByDescending (x => x.scores).ToList ();
        }

        public List<ExpertDTO> GetAllExperts () {

            var experts = _expertDAL.GetAllExperts ();
            var lExperts = new List<ExpertDTO> ();
            foreach (var e in experts) {
                lExperts.Add (_mapper.Map<Expert, ExpertDTO> (e));
            }
            return lExperts;
        }

        //public List<ExpertDTO> GetExpertsByPage(int page, int limit)
        //{
        //    if (page == 0)
        //        page = 1;

        //    if (limit == 0)
        //        limit = int.MaxValue;

        //    var skip = (page - 1) * limit;

        //    var date = _expertDAL.GetExpertsByPage(skip, limit);
        //}

        public List<ExpertDTO> GetFilteredExperts (int category, int subject, int city, string name) {
            var experts = _expertDAL.GetAllExperts ();
            var lExperts = new List<ExpertDTO> ();
            foreach (var e in experts) {
                if (e.enable == false) continue;

                if (city != 0 && e.cityId != city)
                    continue;

                if (subject != 0 && e.proSubject != subject)
                    continue;
                if (!string.IsNullOrEmpty (name) && name != "-" &&
                    !e.firstName.Contains (name) && !e.lastName.Contains (name))
                    continue;
                if (category != 0 && e.proSubject != category &&
                    e.Subject.parent != category)
                    continue;

                var exD = _mapper.Map<Expert, ExpertDTO> (e);
                exD.scores = _recDAL.GetAvgRecommendsByExpertId (exD.id);
                exD.numRecommends = _recDAL.GetCountRecommendsByExpertId (exD.id);

                lExperts.Add (exD);

            }
            return lExperts.OrderByDescending (x => x.scores).ToList ();
        }

        public ExpertDTO InsertExpert (ExpertDTO expert) {
            var exp = _mapper.Map<ExpertDTO, Expert> (expert);
            var expRes = _expertDAL.AddExpert (exp);
            return _mapper.Map<Expert, ExpertDTO> (expRes);
        }
        bool uploadImg (int id, string filename) {
            var ex = _expertDAL.GetExpertById (id);
            ex.imgUrl = filename;
            _expertDAL.UpdateExpert (ex);
            return true;
        }
        public bool IsExpert (int id) {
            var ex = _expertDAL.GetExpertById (id);
            return ex != null;
        }

        public ExpertDTO PutExpert (ExpertDTO expert) {
            var exp = _mapper.Map<ExpertDTO, Expert> (expert);
            var expRes = _expertDAL.UpdateExpert (exp);
            return _mapper.Map<Expert, ExpertDTO> (expRes);
        }

        bool IExpertBL.uploadImg(int id, string filename)
        {
            var ex = _expertDAL.GetExpertById (id);
            ex.imgUrl = filename;
            _expertDAL.UpdateExpert (ex);
            return true;
        }
    }
}