using Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL
{
    public interface IMeetingDAL
    {
        List<Meeting> GetAllMeetings();
        Meeting getMeetingForUser(int expId,int userId);
        Meeting InsertMeeting(Meeting m);
        bool DeleteMeeting(int mId);
        Meeting UpdateMeeting(Meeting m);
        List<Meeting> GetAllMeetingsForExpert(int expId);
        bool ApproveMeeting(int mId);
        bool CancelMeeting(int mId);
        bool InviteRecommend(Meeting m);

   }
}
