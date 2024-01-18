using AutoMapper;
using Core.Abstractions;
using Core.DTOs;
using Core.Entities;
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
        private readonly IGenericRepository<TourmentTeam> _TourmentTeamRepository;
        private readonly ITeamService _teamService;
        private readonly IMapper mapper;

        public TourmentService(IGenericRepository<Tourment> tourmentRepository, IGenericRepository<TourmentTeam> tourmentTeamRepository, ITeamService teamService, IMapper mapper)
        {
            _TourmentRepository = tourmentRepository;
            _TourmentTeamRepository = tourmentTeamRepository;
            _teamService = teamService;
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
            throw new NotImplementedException();
        }

        public async Task<ResponseResultDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseResultDTO> GetById(int id)
        {
            throw new NotImplementedException();
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
