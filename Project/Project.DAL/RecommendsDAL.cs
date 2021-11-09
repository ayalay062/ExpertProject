using Microsoft.EntityFrameworkCore;
using Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DAL
{
    public class RecommendsDAL : IRecommendsDAL
    {
        ProjectContext _RecommendsContext;
        public RecommendsDAL(ProjectContext RecommendsContext)
        {
            _RecommendsContext = RecommendsContext;
        }

        public Recommend AddRecommend(Recommend r)
        {
            try
            {
                var rec = _RecommendsContext.Recommends.FirstOrDefault(x => x.userId ==
                r.userId && x.profId == r.profId);
                if (rec == null)
                {
                    _RecommendsContext.Recommends.Add(r);
                    _RecommendsContext.SaveChanges();
                    return r;

                }
                else
                {
                    r.id = rec.id;
                    return UpdateRecommend(r);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool changeStatus(int id, bool isApproved)
        {
            try
            {
                var rec = _RecommendsContext.Recommends.FirstOrDefault(x => x.id == id);
                rec.isApproved = isApproved;

                _RecommendsContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Recommend> GetAllApprovedRecommends(int id)
        {
            try
            {
                var c = _RecommendsContext.Recommends.Include("Expert").Include("User").Where(x => x.isApproved && x.profId == id).ToList();
                return c;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Recommend> GetAllExpertsRecommends()
        {
            throw new NotImplementedException();
        }

        public List<Recommend> GetAllRecommends()
        {
            try
            {
                var c = _RecommendsContext.Recommends.Include("Expert").Include("User").ToList();
                return c;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCountRecommendsByExpertId(int id)
        {
            try
            {
                var c = _RecommendsContext.Recommends.Where(x => x.isApproved && x.profId == id).Count();

                return c;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public double GetAvgRecommendsByExpertId(int id)
        {
            try
            {
                var c = _RecommendsContext.Recommends.Where(x => x.isApproved && x.profId == id);
                if (c == null || !c.Any())
                    return 0;

                return c.Average(x => x.stars);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Recommend GetRecommendById(int id)
        {
            try
            {
                var rec = _RecommendsContext.Recommends.FirstOrDefault(x => x.id == id);

                return rec;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Recommend UpdateRecommend(Recommend r)
        {
            try
            {
                var rec = _RecommendsContext.Recommends.FirstOrDefault(x => x.id == r.id);
                rec.isApproved = r.isApproved;
                rec.stars = r.stars;
                rec.content = r.content;
                rec.title = r.title;

                _RecommendsContext.SaveChanges();
                return rec;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GetRecommendExistsByUserId(int userId, int expId)
        {
            return _RecommendsContext.Meetings.FirstOrDefault(x => x.idUser == userId && x.idProf == expId) != null;

        }
        public Recommend GetRecommendByUserId(int userId, int expId)
        {
            return _RecommendsContext.Recommends.FirstOrDefault(x => x.userId == userId && x.profId == expId);

        }

        public List<Meeting> GetNoRecommendsAfterMeeting()
        {
            var lMeetings = new List<Meeting>();
            var meetings = new List<Meeting>();
            using (var rmContext = new ProjectContext())
            {
                meetings = rmContext.Meetings.Where(x => x.isApproved && x.date.Date == DateTime.Today.AddDays(-1)).Include("User").Include("Expert").ToList();
            }
            foreach (var meet in meetings)
            {
                try
                {
                    using (var rContext = new ProjectContext())
                    {

                        var rec = rContext.Recommends.FirstOrDefault(x => x.profId == meet.idProf && x.userId == meet.idUser);

                        if (rec == null)
                        {
                            lMeetings.Add(meet);
                        }
                    }
                }
                catch (Exception e)
                { }
            }
            return lMeetings;
        }
    }
}