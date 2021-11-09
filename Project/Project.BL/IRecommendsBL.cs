using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace Project.BL {
    public interface IRecommendsBL {
        List<RecommendDTO> GetAllRecommends ();
        List<RecommendDTO> GetAllApprovedRecommends (int id);
        double GetCountRecommendsById (int id);
        RecommendDTO GetRecommendById (int id);
        RecommendDTO AddRecommend (RecommendDTO r);
        RecommendDTO UpdateRecommend (RecommendDTO r);
        bool changeStatus (int id, bool isApproved);
        bool GetRecommendExistsByUserId (int userId, int expId);
        RecommendDTO GetRecommendByUserId (int userId, int expId);
        void SendInviteAfterMeeting();
    }
}