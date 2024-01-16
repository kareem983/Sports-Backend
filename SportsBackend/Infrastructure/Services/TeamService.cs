using AutoMapper;
using Core.Abstractions;
using Core.DTOs;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class TeamService : ITeamService
    {
        private readonly IGenericRepository<Team> _teamRepository;
        private readonly SportsContext sportsContext;
        private readonly IMapper mapper;

        public TeamService(IGenericRepository<Team> teamRepository, IMapper mapper, SportsContext sportsContext)
        {
            _teamRepository = teamRepository;
            this.mapper = mapper;
            this.sportsContext = sportsContext;
        }

        public async Task<ResponseResultDTO> Add(TeamDTO teamDTO)
        {
            try
            {
                var team = await sportsContext.Teams.FirstOrDefaultAsync(x=> x.Name.ToLower() == teamDTO.Name.ToLower());
                if(team is not null)
                    return ResponseResultDTO.Failed("The Team is already Exist");
                

                team = mapper.Map<Team>(teamDTO);
                await _teamRepository.AddAsync(team);

                if(await _teamRepository.Save())
                    return new ResponseResultDTO { Success = true, Message = "The Team Added Successfully", Data = teamDTO};
                else
                    return ResponseResultDTO.Failed("There are a problem occured during adding a team");
            }
            catch (Exception ex)
            {
                return ResponseResultDTO.Failed("There are a problem occured during adding a team");
            }
        }


        public async Task<ResponseResultDTO> Update(TeamDTO teamDTO)
        {
            //try
            //{
            //    var team = await sportsContext.Teams.FirstOrDefaultAsync(x => x.Name.ToLower() == teamDTO.Name.ToLower());
            //    if (team is null)
            //        return ResponseResultDTO.Failed("The Team is not Exist");


            //    //team = mapper.Map<Team>(teamDTO);
            //    //_teamRepository.Update(team);
            //    await _teamRepository.AutoMapperUpdate(teamDTO);
            //    if (await _teamRepository.Save())
            //        return new ResponseResultDTO { Success = true, Message = "The Team Updated Successfully", Data = teamDTO };
            //    else
            //        return ResponseResultDTO.Failed("There are a problem occured during updating a team");
            //}
            //catch (Exception ex)
            //{
            //    return ResponseResultDTO.Failed("There are a problem occured during updating a team");
            //}

            throw new NotImplementedException();
        }

        public async Task<ResponseResultDTO> Delete(string name)
        {
            throw new NotImplementedException();
        }

    }
}
