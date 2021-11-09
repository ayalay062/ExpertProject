using Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ExpertDTO, Expert>().ForMember(t => t.City, options => options.MapFrom(source => source.City))
                .ForMember(t => t.Subject, options => options.MapFrom(source => source.Subject));
            CreateMap<Expert, ExpertDTO>();
            CreateMap<City, CityDTO>(); // <======
            CreateMap<CityDTO, City>(); // <======


            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<User, UserDTO>()
        .ReverseMap();

            CreateMap<Subject, SubjectDTO>();
            CreateMap<SubjectDTO, Subject>();

            CreateMap<Recommend, RecommendDTO>();
            CreateMap<RecommendDTO, Recommend>();
            CreateMap<Meeting, MeetingDTO>();
            CreateMap<MeetingDTO, Meeting>();


        }
    }
}
