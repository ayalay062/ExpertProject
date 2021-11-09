using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Project.BL;

namespace ProjectAPI.Controllers {
    [Route ("api/Meeting")]
    [ApiController]
    public class MeetingController : ControllerBase {
        IMeetingBL _meetingBL;
        public MeetingController (IMeetingBL _meetingBLl) {
            _meetingBL = _meetingBLl;
        }

        [HttpGet]
        [Route ("GetAllMeetings")]
        public List<MeetingDTO> GetAllMeetings () {

            return _meetingBL.GetAllMeetings ();
        }

        [HttpGet]
        [Route ("GetAllMeetingsForExpert/{id}")]
        public List<MeetingDTO> GetAllMeetingsForExpert (int id) {

            return _meetingBL.GetAllMeetingsForExpert (id);
        }

        [HttpGet]
        [Route ("getMeetingForUser/{expId}/{userId}")]
        public MeetingDTO getMeetingForUser (int expId, int userId) {

            return _meetingBL.getMeetingForUser (expId, userId);
        }

        [HttpPost]
        [Route ("InsertMeeting")]
        public MeetingDTO InsertMeeting (MeetingDTO e) {

            return _meetingBL.InsertMeeting (e);

        }

        [HttpPost]
        [Route ("UpdateMeeting")]
        public MeetingDTO UpdateMeeting (MeetingDTO e) {
            return _meetingBL.UpdateMeeting (e);
        }

        [HttpDelete]
        [Route ("DeleteMeeting/{id}")]
        public bool DeleteMeeting (int id) {
            return _meetingBL.DeleteMeeting (id);
        }

        [HttpPost]
        [Route ("InviteRecommend")]
        public bool InviteRecommend (MeetingDTO e) {
            return _meetingBL.InviteRecommend (e);
        }

        [HttpPost]
        [Route ("ApproveMeeting")]
        public bool ApproveMeeting (MeetingDTO e) {
            return _meetingBL.ApproveMeeting (e.id);
        }

        [HttpPost]
        [Route ("CancelMeeting")]
        public bool CancelMeeting (MeetingDTO e) {
            return _meetingBL.CancelMeeting (e.id);
        }

    }
}