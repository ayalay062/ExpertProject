using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using AutoMapper;
using DTO;
using Project.DAL;
using Project.DAL.Models;

namespace Project.BL
{
    public class RecommendsBL : IRecommendsBL
    {

        IRecommendsDAL _recommendDAL;
        IMapper _mapper;
        public RecommendsBL(
            IRecommendsDAL recDAL)
        {
            _recommendDAL = recDAL;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            _mapper = config.CreateMapper();
        }

        public RecommendDTO AddRecommend(RecommendDTO r)
        {

            var rec = _recommendDAL.AddRecommend(_mapper.Map<RecommendDTO, Recommend>(r));
            return _mapper.Map<Recommend, RecommendDTO>(rec);

        }

        public bool changeStatus(int id, bool isApproved)
        {
            return _recommendDAL.changeStatus(id, isApproved);

        }

        public List<RecommendDTO> GetAllApprovedRecommends(int id)
        {
            var recommends = _recommendDAL.GetAllApprovedRecommends(id);
            var lRecommends = new List<RecommendDTO>();
            foreach (var s in recommends)
            {
                lRecommends.Add(_mapper.Map<Recommend, RecommendDTO>(s));
            }
            return lRecommends;
        }

        public List<RecommendDTO> GetAllRecommends()
        {
            var recommends = _recommendDAL.GetAllRecommends();
            var lRecommends = new List<RecommendDTO>();
            foreach (var s in recommends)
            {
                lRecommends.Add(_mapper.Map<Recommend, RecommendDTO>(s));
            }
            return lRecommends;
        }

        public double GetCountRecommendsById(int id)
        {
            return _recommendDAL.GetCountRecommendsByExpertId(id);

        }

        public RecommendDTO GetRecommendById(int id)
        {
            var rec = _recommendDAL.GetRecommendById(id);
            return _mapper.Map<Recommend, RecommendDTO>(rec);
        }

        public bool GetRecommendExistsByUserId(int userId, int expId)
        {
            return _recommendDAL.GetRecommendExistsByUserId(userId, expId);

        }
        public RecommendDTO GetRecommendByUserId(int userId, int expId)
        {

            var rec = _recommendDAL.GetRecommendByUserId(userId, expId);
            return _mapper.Map<Recommend, RecommendDTO>(rec);

        }

        public RecommendDTO UpdateRecommend(RecommendDTO r)
        {
            var rec = _recommendDAL.UpdateRecommend(_mapper.Map<RecommendDTO, Recommend>(r));
            return _mapper.Map<Recommend, RecommendDTO>(rec);

        }

        public void SendInviteAfterMeeting()
        {
            var meetings = _recommendDAL.GetNoRecommendsAfterMeeting();
            var sub = "הזמנה להמליץ";

            foreach (var m in meetings)
            {
                var body = "<div style='background-color: #dfbd5d; margin: auto; padding: 25px; width: 95%; height: 100%;'>" +
            "   <div dir='rtl' style='background-color: white; margin: auto; width: 100%; height: 100%;'> " +
            "   <p style='text-align: center;font-size: 20px;padding-top: 25px; '>שלום " + m.User.userName+ "</p><p style='text-align: center;'>נשמח אם תיכנס.י&nbsp; " +
            "   <a title='recommend' href='http://localhost:4200/expert/" + m.idProf + "/add-recommend'>לכאן</a>&nbsp;להמליץ על " +
            "  "+ m.Expert.businessName+ "</p><p style= 'text-align:center;border:none;width:100%;height:30px;margin:10px' " +
            "   ><a style='background-color:#dfbd5d;display:block;width:180px;margin:auto;color:black;text-decoration:none;font-size:14px' title='approve' href='http://localhost:4200/expert/" + m.idProf + "/add-recommend'>להכנס לטופס ההמלצה</a></p></div></div></div>";

                SendEmail(m.User.email, sub, body);
            }


        }


        private static bool SendEmail(string to, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            mail.Body = body;
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("nipageshbismachot2021@gmail.com",                "ניפגש בשמחות");
            mail.To.Add(to);
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("nipageshbismachot2021@gmail.com", "Nn123456!");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
            return true;
        }

    }
}
