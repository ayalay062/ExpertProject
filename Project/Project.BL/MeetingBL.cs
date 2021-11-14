using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using AutoMapper;
using DTO;
using Project.DAL;
using Project.DAL.Models;

namespace Project.BL {
    public class MeetingBL : IMeetingBL {
        IMeetingDAL _meetingDAL;
        IUserDAL _userDAL;
        IExpertDAL _expertDAL;
        IMapper _mapper;
        public MeetingBL (IMeetingDAL meetingDAL,
            IUserDAL userDAL,
            IExpertDAL expertDAL) {
            _meetingDAL = meetingDAL;
            _userDAL = userDAL;
            _expertDAL = expertDAL;
            var config = new MapperConfiguration (cfg => {
                cfg.AddProfile<AutoMapperProfile> ();
            });
            _mapper = config.CreateMapper ();
        }
        public bool ApproveMeeting (int mId) {
            return _meetingDAL.ApproveMeeting (mId);
        }

        public bool CancelMeeting (int mId) {
            return _meetingDAL.CancelMeeting (mId);

        }

        public bool DeleteMeeting (int mId) {
            return _meetingDAL.DeleteMeeting (mId);

        }

        public List<MeetingDTO> GetAllMeetings () {

            var meetings = _meetingDAL.GetAllMeetings ();
            var lMeetings = new List<MeetingDTO> ();
            foreach (var s in meetings) {
                lMeetings.Add (_mapper.Map<Meeting, MeetingDTO> (s));
            }
            return lMeetings;
        }

        public List<MeetingDTO> GetAllMeetingsForExpert (int expId) {

            var meetings = _meetingDAL.GetAllMeetingsForExpert (expId);
            var lMeetings = new List<MeetingDTO> ();
            foreach (var s in meetings) {
                lMeetings.Add (_mapper.Map<Meeting, MeetingDTO> (s));
            }
            return lMeetings.OrderByDescending (x => x.date).ThenBy (x => x.time).ToList ();

        }

        public MeetingDTO getMeetingForUser (int expId, int userId) {
            var s = _meetingDAL.getMeetingForUser (expId, userId);

            return _mapper.Map<Meeting, MeetingDTO> (s);
        }

        public MeetingDTO InsertMeeting (MeetingDTO m) {
            var meeting = _meetingDAL.InsertMeeting (_mapper.Map<MeetingDTO, Meeting> (m));
            return _mapper.Map<Meeting, MeetingDTO> (meeting);

        }

        public bool InviteRecommend (MeetingDTO m) {
            var user = _userDAL.GetUserById (m.idUser);
            var expert = _expertDAL.GetExpertById (m.idProf);

            var sub = "הזמנה להמליץ";

            var body = "<div style='background-color: #dfbd5d; margin: auto; padding: 25px; width: 95%; height: 100%;'>" +
                "   <div dir='rtl' style='background-color: white; margin: auto; width: 100%; height: 100%;'> " +
                "   <p style='text-align: center;font-size: 20px;padding-top: 25px; '>שלום " + user.userName + "</p><p style='text-align: center;'>נשמח אם תיכנס.י&nbsp; " +
                "   <a title='recommend' href='http://localhost:4200/expert/" + m.idProf + "/add-recommend'>לכאן</a>&nbsp;להמליץ על " +
                "  " + expert.businessName + "</p><p style= 'text-align:center;border:none;width:100%;height:30px;margin:10px' " +
                "   ><a style='background-color:#dfbd5d;display:block;width:180px;margin:auto;color:black;text-decoration:none;font-size:14px' title='approve' href='http://localhost:4200/expert/" + m.idProf + "/add-recommend'>להכנס לטופס ההמלצה</a></p></div></div></div>";

            SendEmail (user.email, sub, body);

            return true;
        }

        private static bool SendEmail (string to, string subject, string body) {
            MailMessage mail = new MailMessage ();
            mail.Body = body;
            SmtpClient SmtpServer = new SmtpClient ("smtp.gmail.com");
            mail.From = new MailAddress ("nipageshbismachot2021@gmail.com", "ניפגש בשמחות");
            mail.To.Add (to);
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential ("nipageshbismachot2021@gmail.com", "Nn123456!");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send (mail);
            return true;
        }
        public MeetingDTO UpdateMeeting (MeetingDTO m) {
            var meeting = _meetingDAL.UpdateMeeting (_mapper.Map<MeetingDTO, Meeting> (m));
            return _mapper.Map<Meeting, MeetingDTO> (meeting);

        }

    }
}