using System;
using System.Collections.Generic;
using System.Text;
using Project.DAL.Models;

namespace Project.DAL {
    public interface IRecommendsDAL {
        List<Recommend> GetAllRecommends ();

        List<Recommend> GetAllApprovedRecommends (int id);

        int GetCountRecommendsByExpertId (int id);
        double GetAvgRecommendsByExpertId (int id);

        Recommend GetRecommendById (int id);
        Recommend AddRecommend (Recommend r);
        Recommend UpdateRecommend (Recommend r);

        bool GetRecommendExistsByUserId (int userId, int expId);
        Recommend GetRecommendByUserId (int userId, int expId);

        bool changeStatus (int id, bool isApproves);
        List<Meeting> GetNoRecommendsAfterMeeting();
        //int countRecommends(int id);
    }
}