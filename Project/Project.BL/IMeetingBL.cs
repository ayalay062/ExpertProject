using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.BL
{
    public interface IMeetingBL
    {
        List<MeetingDTO> GetAllMeetings();
        MeetingDTO getMeetingForUser(int expId, int userId);
        MeetingDTO InsertMeeting(MeetingDTO m);
        bool DeleteMeeting(int mId);
        MeetingDTO UpdateMeeting(MeetingDTO m);
        List<MeetingDTO> GetAllMeetingsForExpert(int expId);
        bool ApproveMeeting(int mId);
        bool CancelMeeting(int mId);
        bool InviteRecommend(MeetingDTO m);
    }
}
