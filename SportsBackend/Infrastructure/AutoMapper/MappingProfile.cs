using AutoMapper;
using Core.DTOs;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            MapAccount();
            MapTeam();
            MapTourment();
            MapTourmentTeam();
            MapPlayer();
            MapMatch();
        }

        private void MapAccount()
        {
            //CreateMap<UserDTO, ApplicationUser>()
            //    .ForMember(dest =>
            //    dest.UserName,
            //    opt => opt.MapFrom(src => src.UserName)
            //    )
            //    .ForMember(dest=>
            //    dest.Email,
            //    opt=> opt.MapFrom(src=> src.Email)
            //    ).ReverseMap();
        }

        private void MapTeam()
        {
            CreateMap<TeamDTO, Team>().ReverseMap();
        }

        private void MapTourment()
        {
            CreateMap<TourmentDTO, Tourment>().ReverseMap();
        }

        private void MapTourmentTeam()
        {
            CreateMap<TourmentTeamDTO, TourmentTeam>().ReverseMap();
        }

        private void MapPlayer()
        {
            CreateMap<PlayerDTO, Player>().ReverseMap();
        }

        private void MapMatch()
        {
            CreateMap<MatchDTO, Match>().ReverseMap();
        }
    }
}
