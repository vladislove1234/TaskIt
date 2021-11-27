using System;
using AutoMapper;
using Taskit_server.Model.Entities.TeamModels;

namespace Taskit_server.Mapping
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            CreateMap<Team, TeamInfo>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.TeamId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
