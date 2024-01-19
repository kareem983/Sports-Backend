using AutoMapper;
using Core.Abstractions;
using Core.DTOs;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class TeamService : ITeamService
    {
        private readonly IGenericRepository<Team> _teamRepository;
        private readonly IGenericRepository<Player> _playerRepository;
        private readonly IMapper mapper;

        public TeamService(IGenericRepository<Team> teamRepository, IGenericRepository<Player> playerRepository, IMapper mapper)
        {
            _teamRepository = teamRepository;
            _playerRepository = playerRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseResultDTO> Add(TeamDTO teamDTO)
        {
            try
            {
                var team = await _teamRepository.GetByExpression(x=> x.Name.ToLower() == teamDTO.Name.ToLower());
                teamDTO.Id = null;
                if(team is not null)
                    return ResponseResultDTO.Failed("The Team is already Exist");
                

                await _teamRepository.AutoMapperAddAsync(teamDTO);
                if (await _teamRepository.Save())
                    return ResponseResultDTO.Succeeded("The Team Added Successfully");
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
            try
            {
                var team = await _teamRepository.GetByIdAsync(teamDTO.Id.Value);
                if (team is null)
                    return ResponseResultDTO.Failed("The Team is not Exist");


                await _teamRepository.AutoMapperUpdateAsync(teamDTO);
                if (await _teamRepository.Save())
                    return new ResponseResultDTO { Success = true, Message = "The Team Updated Successfully", Data = teamDTO };
                else
                    return ResponseResultDTO.Failed("There are a problem occured during updating a team");
            }
            catch (Exception ex)
            {
                return ResponseResultDTO.Failed("There are a problem occured during updating a team\nPlease Enter the Id of the team");
            }
        }

        public async Task<ResponseResultDTO> Delete(int id)
        {
            try
            {
                var team = await _teamRepository.GetByIdAsync(id);
                if (team is null)
                    return ResponseResultDTO.Failed("The Team is not Exist");
               
                var teamDTO = mapper.Map<TeamDTO>(team);
                await _teamRepository.DeleteAsync(team);
                if (await _teamRepository.Save())
                    return new ResponseResultDTO { Success = true, Message = "The Team Deleted Successfully", Data = teamDTO };
                else
                    return ResponseResultDTO.Failed("There are a problem occured during deleting a team");
            }
            catch (Exception ex)
            {
                return ResponseResultDTO.Failed("There are a problem occured during deleting a team\nPlease Enter the Id of the team");
            }
        }

        public async Task<ResponseResultDTO> GetAll()
        {
            try
            {
                var teamList = await _teamRepository.GetAllAsync();
                var teamDTOList = mapper.Map<List<TeamDTO>>(teamList);

                if (teamList.Count() == 0)
                    return ResponseResultDTO.Failed("There are no Teams");
                else
                    return new ResponseResultDTO { Success = true, Message = "The Teams Get All process Done Successfully", Data = teamDTOList };
            }
            catch (Exception ex)
            {
                return ResponseResultDTO.Failed("There are a problem occured during Get All Teams");
            }
        }

        public async Task<ResponseResultDTO> GetById(int id)
        {
            try
            {
                var team = await _teamRepository.GetByIdAsync(id);
                if (team is null)
                    return ResponseResultDTO.Failed("The Team is not Exist");

                var teamDTO = mapper.Map<TeamDTO>(team);
                return new ResponseResultDTO { Success = true, Message = "The Team GetByID Process Done Successfully", Data = teamDTO };
            }
            catch (Exception ex)
            {
                return ResponseResultDTO.Failed("There are a problem occured during getting a team");
            }
        }

        public async Task<ResponseResultDTO> GetPlayersIDsByTeamId(int teamId)
        {
            try
            {
                var team = await _teamRepository.GetByIdAsync(teamId);
                if (team is null)
                    return ResponseResultDTO.Failed("The Team is not Exist");

                var playersIDsList = (await _playerRepository.GetAllByExpression(x => x.TeamId == teamId)).Select(x=> x.Id);
                if (playersIDsList.Count() == 0)
                    return ResponseResultDTO.Failed("The team doesn't have players");

                return new ResponseResultDTO { Success = true, Message = "The Players IDs GetBy Team ID Process Done Successfully", Data = playersIDsList };
            }
            catch (Exception ex)
            {
                return ResponseResultDTO.Failed("There are a problem occured during getting a players IDs by team id");
            }
        }

        public async Task<ResponseResultDTO> GetPlayersByTeamId(int teamId)
        {
            try
            {
                var team = await _teamRepository.GetByIdAsync(teamId);
                if (team is null)
                    return ResponseResultDTO.Failed("The Team is not Exist");

                var playersList = await _playerRepository.GetAllByExpression(x=> x.TeamId == teamId);
                if(playersList.Count() == 0)
                    return ResponseResultDTO.Failed("The team doesn't have players");

                var playerDTOList = mapper.Map<List<PlayerDTO>>(playersList);
                return new ResponseResultDTO { Success = true, Message = "The Players GetBy Team ID Process Done Successfully", Data = playerDTOList };
            }
            catch (Exception ex)
            {
                return ResponseResultDTO.Failed("There are a problem occured during getting a players by team id");
            }
        }


    }
}
