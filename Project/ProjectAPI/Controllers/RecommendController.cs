using DTO;
using Microsoft.AspNetCore.Mvc;
using Project.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAPI.Controllers
{
    [Route("api/Recommend")]
    [ApiController]
    public class RecommendController : ControllerBase
    {
        IRecommendsBL _recommendBL;
        public RecommendController(IRecommendsBL _recommendBLl)
        {
            _recommendBL = _recommendBLl;
        }

        [HttpGet]
        [Route("GetAllRecommends")]
        public List<RecommendDTO> GetAllRecommends()
        {

            return _recommendBL.GetAllRecommends();
        }

        [HttpGet]
        [Route("GetAllRecommendsForExpert/{id}")]
        public List<RecommendDTO> GetAllApprovedRecommends(int id)
        {

            return _recommendBL.GetAllApprovedRecommends(id);
        }
        [HttpGet]
        [Route("getRecommendForUser/{id}")]
        public RecommendDTO getRecommendForUser(int id)
        {

            return _recommendBL.GetRecommendById(id);
        }


        [HttpPost]
        [Route("AddRecommend")]
        public RecommendDTO AddRecommend(RecommendDTO e)
        {

            return _recommendBL.AddRecommend(e);


        }
        [HttpPost]
        [Route("UpdateRecommend")]
        public RecommendDTO UpdateRecommend(RecommendDTO e)
        {
            return _recommendBL.UpdateRecommend(e);
        }
        [HttpGet]
        [Route("GetRecommendExistsByUserId/{userId}/{expId}")]
        public bool GetRecommendExistsByUserId(int userId,int expId)
        {
            return _recommendBL.GetRecommendExistsByUserId(userId, expId);
        }
   
        [HttpGet]
        [Route("GetRecommendByUserId/{userId}/{expId}")]
        public RecommendDTO GetRecommendByUserId(int userId,int expId)
        {
            return _recommendBL.GetRecommendByUserId(userId, expId);
        }
        [HttpPost]
        [Route("ChangeStatus")]
        public bool ChangeStatus(RecommendDTO e)
        {
            return _recommendBL.changeStatus(e.id, e.isApproved);
        }
    }
}
