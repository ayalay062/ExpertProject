using Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DAL
{
    public class MeetingDAL : IMeetingDAL
    {
        ProjectContext _meetingContext;
        public MeetingDAL(ProjectContext meetingContext)
        {
            _meetingContext = meetingContext;
        }

        public bool ApproveMeeting(int mId)
        {
            try
            {
                var meet = _meetingContext.Meetings.FirstOrDefault(x => x.id == mId);
                meet.isApproved = true;

                _meetingContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool CancelMeeting(int mId)
        {
            try
            {
                var meet = _meetingContext.Meetings.FirstOrDefault(x => x.id == mId);
                meet.isApproved = false;

                _meetingContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteMeeting(int mId)
        {
            try
            {
                var meet = _meetingContext.Meetings.FirstOrDefault(x => x.id == mId);

                _meetingContext.Meetings.Remove(meet);
                _meetingContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Meeting> GetAllMeetings()
        {
            try
            {
                var mAll = _meetingContext.Meetings.ToList();
                return mAll;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Meeting> GetAllMeetingsForExpert(int expId)
        {
            try
            {
                var mAll = _meetingContext.Meetings.Where(x=>x.idProf== expId && x.date>= DateTime.Today).ToList();
                return mAll;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Meeting getMeetingForUser(int expId, int userId)
        {
            try
            {
                var mAll = _meetingContext.Meetings.
                    FirstOrDefault(x => x.idProf == expId && x.idUser == userId && x.date >= DateTime.Today);
                return mAll;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Meeting InsertMeeting(Meeting m)
        {
            try
            {
                _meetingContext.Meetings.Add(m);
                _meetingContext.SaveChanges();
                return m;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool InviteRecommend(Meeting m)
        {
            throw new NotImplementedException();
        }

        public Meeting UpdateMeeting(Meeting m)


        {
            try
            {
                var meet = _meetingContext.Meetings.
                    FirstOrDefault(x => x.id == m.id);
                meet.content = m.content;
                meet.date = m.date;
                meet.title = m.title;
                meet.time = m.time;


                _meetingContext.SaveChanges();
                return meet;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
