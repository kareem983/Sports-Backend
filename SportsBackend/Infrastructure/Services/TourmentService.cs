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
    public class TourmentService : ITourmentService
    {
        private readonly IGenericRepository<Tourment> _TourmentRepository;
        private readonly IGenericRepository<Team> _TeamRepository;
        private readonly IGenericRepository<TourmentTeam> _TourmentTeamRepository;
        private readonly ITeamService _teamService;
        private readonly SportsContext sportsContext;
        private readonly IMapper mapper;

        public TourmentService(IGenericRepository<Tourment> tourmentRepository, IGenericRepository<TourmentTeam> tourmentTeamRepository,
            ITeamService teamService, SportsContext sportsContext, IMapper mapper, IGenericRepository<Team> teamRepository)
        {
            _TourmentRepository = tourmentRepository;
            _TourmentTeamRepository = tourmentTeamRepository;
            _teamService = teamService;
            _TeamRepository = teamRepository;
            this.sportsContext = sportsContext;
            this.mapper = mapper;
        }


        public async Task<ResponseResultDTO> Add(TourmentDTO tourmentDTO)
        {
            try
            {
                tourmentDTO.Id = null;
                if (await GetTourmentByName(tourmentDTO.Name) is not null)
                    return ResponseResultDTO.Failed("The Tourment is already Exist");
                

                await _TourmentRepository.AutoMapperAddAsync(tourmentDTO);
                if (await _TourmentRepository.Save())
                {
                    var addedTourment = await GetTourmentByName(tourmentDTO.Name);
                    var tourTeamResult = await AddTeamsinTourment(addedTourment.Id, tourmentDTO.TeamsIds);
                    tourmentDTO.Id = addedTourment.Id;
                    return new ResponseResultDTO { Success = tourTeamResult.Success, Message = tourTeamResult.Message, Data = tourmentDTO };
                }
                else
                    return ResponseResultDTO.Failed("There are a problem occured during adding a tourment");
            }
            catch (Exception ex)
            {
                return ResponseResultDTO.Failed("There are a problem occured during adding a tourment");
            }
        }


        public async Task<ResponseResultDTO> Update(TourmentDTO tourmentDTO)
        {
            try
            {
                var tourment = await _TourmentRepository.GetByIdAsync(tourmentDTO.Id.Value);
                if (tourment is null)
                    return ResponseResultDTO.Failed("The Tourment is not Exist");


                var deleteTourmentTeamResult = await DeleteTourmentTeamByTourmentID(tourmentDTO.Id.Value);
                if (!deleteTourmentTeamResult.Success)
                    return deleteTourmentTeamResult;

                var addTourmentTeamResult = await AddTeamsinTourment(tourmentDTO.Id.Value, tourmentDTO.TeamsIds);
                if (!addTourmentTeamResult.Success)
                    return addTourmentTeamResult;

                await _TourmentRepository.AutoMapperUpdateAsync(tourmentDTO);
                if (await _TourmentRepository.Save())
                    return new ResponseResultDTO { Success = true, Message = "The Tourment Updated Successfully", Data = tourmentDTO };
                else
                    return ResponseResultDTO.Failed("There are a problem occured during updating a tourment");
            }
            catch (Exception ex)
            {
                return ResponseResultDTO.Failed("There are a problem occured during updating a tourment\nPlease Enter the Id of the tourment");
            }
        }

        public async Task<ResponseResultDTO> Delete(int id)
        {
            try
            {
                var tourment = await _TourmentRepository.GetByIdAsync(id);
                if (tourment is null)
                    return ResponseResultDTO.Failed("The Tourment is not Exist");

                var tourmentDTO = mapper.Map<TourmentDTO>(tourment);
                tourmentDTO.TeamsIds = (List<int>)(await GetTeamsIDsByTourmentId(tourment.Id)).Data;

                var deleteTourmentTeamResult = await DeleteTourmentTeamByTourmentID(tourmentDTO.Id.Value);
                if (!deleteTourmentTeamResult.Success)
                    return deleteTourmentTeamResult;

                await _TourmentRepository.DeleteAsync(tourment);
                if (await _TourmentRepository.Save())
                    return new ResponseResultDTO { Success = true, Message = "The Tourment Deleted Successfully", Data = tourmentDTO };
                else
                    return ResponseResultDTO.Failed("There are a problem occured during deleting a tourment");
            }
            catch (Exception ex)
            {
                return ResponseResultDTO.Failed("There are a problem occured during deleting a tourment\nPlease Enter the Id of the tourment");
            }
        }

        
        public async Task<ResponseResultDTO> GetAll()
        {
            try
            {
                var tourmentList = await _TourmentRepository.GetAllAsync();
                var tourmentDTOList = mapper.Map<List<TourmentDTO>>(tourmentList);

                int tourmentIndex = 0;
                foreach (var tourment in tourmentList)
                {
                    tourmentDTOList[tourmentIndex++].TeamsIds = (List<int>)(await GetTeamsIDsByTourmentId(tourment.Id)).Data;
                }

                if (tourmentList.Count() == 0)
                    return ResponseResultDTO.Failed("There are no Tourments");
                else
                    return new ResponseResultDTO { Success = true, Message = "The Tourments Get All process Done Successfully", Data = tourmentDTOList };
            }
            catch (Exception ex)
            {
                return ResponseResultDTO.Failed("There are a problem occured during Get All Tourments");
            }
        }

        public async Task<ResponseResultDTO> GetById(int id)
        {
            try
            {
                var tourment = await _TourmentRepository.GetByIdAsync(id);
                if (tourment is null)
                    return ResponseResultDTO.Failed("The Tourment is not Exist");

                var tourmentDTO = mapper.Map<TourmentDTO>(tourment);
                tourmentDTO.TeamsIds = (List<int>)(await GetTeamsIDsByTourmentId(tourment.Id)).Data;

                return new ResponseResultDTO { Success = true, Message = "The Team GetByID Process Done Successfully", Data = tourmentDTO };
            }
            catch (Exception ex)
            {
                return ResponseResultDTO.Failed("There are a problem occured during getting a tourment");
            }
        }

        public async Task<ResponseResultDTO> GetTeamsIDsByTourmentId(int tourmentId)
        {
            try
            {
                var tourment = await _TourmentRepository.GetByIdAsync(tourmentId);
                if (tourment is null)
                    return ResponseResultDTO.Failed("The Tourment is not Exist");

                TourmentDTO tourmentDTO = new TourmentDTO();
                var tourmentTeamList = await sportsContext.TourmentTeams.Include(c => c.Tourment).Where(x => x.TourmentId == tourmentId).ToListAsync();
                foreach (var item in tourmentTeamList)
                {
                    tourmentDTO.TeamsIds.Add(item.TeamId);
                }

                return new ResponseResultDTO { Success = true, Message = "The Teams IDs GetBy Tourment ID Process Done Successfully", Data = tourmentDTO.TeamsIds };
            }
            catch (Exception ex)
            {
                return ResponseResultDTO.Failed("There are a problem occured during deleting a TourmentTeams\nPlease Enter the Id of the tourment");
            }
        }

        public async Task<ResponseResultDTO> GetTeamsByTourmentId(int tourmentId)
        {
            try
            {
                var tourment = await _TourmentRepository.GetByIdAsync(tourmentId);
                if (tourment is null)
                    return ResponseResultDTO.Failed("The Tourment is not Exist");


                var teamsIDs = (List<int>)(await GetTeamsIDsByTourmentId(tourmentId)).Data;

                List<TeamDTO> teamsDTOList = new List<TeamDTO>();
                foreach (var teamsID in teamsIDs)
                {
                    var team = await _TeamRepository.GetByExpression(x => x.Id == teamsID);
                    var teamDTO = mapper.Map<TeamDTO>(team);
                    teamsDTOList.Add(teamDTO);
                }

                return new ResponseResultDTO { Success = true, Message = "The Teams GetBy Tourment ID Process Done Successfully", Data = teamsDTOList};
            }
            catch (Exception ex)
            {
                return ResponseResultDTO.Failed("There are a problem occured during deleting a TourmentTeams\nPlease Enter the Id of the tourment");
            }
        }


        #region HELPER METHODS
        private async Task<Tourment> GetTourmentByName(string name) => await _TourmentRepository.
            GetByExpression(x => x.Name.ToLower() == name.ToLower());

        private async Task<bool> CheckTeamIsExist(int teamId)
        {
            var teamReslut = await _teamService.GetById(teamId);
            return teamReslut.Success;
        }

        private async Task<ResponseResultDTO> AddTeamsinTourment(int tourmentId, List<int> teamsIds)
        {
            try
            {
                if (teamsIds.Count() == 0)
                    return ResponseResultDTO.Failed("PLease Enter the Assigned Teams Ids");

                else
                {
                    foreach (var teamId in teamsIds)
                    {
                        if (!await CheckTeamIsExist(teamId))
                            return ResponseResultDTO.Failed($"The Team Id {teamId} is not exist");

                        TourmentTeamDTO tourmentTeamDTO = new TourmentTeamDTO { TourmentId = tourmentId, TeamId = teamId };
                        await _TourmentTeamRepository.AutoMapperAddAsync(tourmentTeamDTO);
                    }
                    if (await _TourmentTeamRepository.Save())
                        return ResponseResultDTO.Succeeded("The Tourment Added with Assigned Teams Successfully");
                    else
                        return ResponseResultDTO.Failed("There are a problem occured during adding a tourment with Assigned Teams");
                }
            }
            catch (Exception ex)
            {
                return ResponseResultDTO.Failed("There are a problem occured during adding a tourment with Assigned Teams");
            }
        }

        private async Task<ResponseResultDTO> DeleteTourmentTeamByTourmentID(int tourmentId)
        {
            try
            {
                var tourmentTeamList = await _TourmentTeamRepository.GetAllByExpression(x => x.TourmentId == tourmentId);
                if (tourmentTeamList.Count() == 0)
                    return ResponseResultDTO.Failed("The Tourment ID is not Exist");

                foreach (var tourmentTeam in tourmentTeamList)
                {
                    await _TourmentTeamRepository.DeleteAsync(tourmentTeam);
                }

                if (await _TourmentTeamRepository.Save())
                    return new ResponseResultDTO { Success = true, Message = "The TourmentTeam Deleted Successfully" };
                else 
                    return ResponseResultDTO.Failed("There are a problem occured during deleting a TourmentTeam");
            }
            catch (Exception ex)
            {
                return ResponseResultDTO.Failed("There are a problem occured during deleting a TourmentTeam\nPlease Enter the Id of the team");
            }
        }

        #endregion

    }
}
