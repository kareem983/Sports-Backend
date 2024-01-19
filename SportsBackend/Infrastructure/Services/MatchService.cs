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
    public class MatchService : IMatchService
    {
        private readonly IGenericRepository<Match> _matchRepository;
        private readonly ITeamService teamService;
        private readonly ITourmentService tourmentService;
        private readonly IMapper mapper;

        public MatchService(IGenericRepository<Match> matchRepository, ITeamService teamService, ITourmentService tourmentService, IMapper mapper)
        {
            _matchRepository = matchRepository;
            this.teamService = teamService;
            this.tourmentService = tourmentService;
            this.mapper = mapper;
        }


        public async Task<ResponseResultDTO> Add(MatchDTO matchDTO)
        {
            try
            {
                var match = await _matchRepository.GetByExpression(x => x.HomeTeamId == matchDTO.HomeTeamId
                 && x.HostedTeamId == matchDTO.HostedTeamId);
                if (match is not null)
                    return ResponseResultDTO.Failed("The Match is already Exist");

                var checkResult = await CheckMatchConstraints(matchDTO);
                if (!checkResult.Success)
                    return checkResult;

                matchDTO.Id = null;
                await _matchRepository.AutoMapperAddAsync(matchDTO);
                if (await _matchRepository.Save())
                    return ResponseResultDTO.Succeeded("The Match Added Successfully");
                else
                    return ResponseResultDTO.Failed("There are a problem occured during adding a match");
            }
            catch (Exception ex)
            {
                return ResponseResultDTO.Failed("There are a problem occured during adding a match");
            }
        }

        public async Task<ResponseResultDTO> Update(MatchDTO matchDTO)
        {
            try
            {
                var match = await _matchRepository.GetByIdAsync(matchDTO.Id.Value);
                if (match is null)
                    return ResponseResultDTO.Failed("The Match is not Exist");

                var checkResult = await CheckMatchConstraints(matchDTO);
                if (!checkResult.Success)
                    return checkResult;

                await _matchRepository.AutoMapperUpdateAsync(matchDTO);
                if (await _matchRepository.Save())
                    return new ResponseResultDTO { Success = true, Message = "The Match Updated Successfully", Data = matchDTO };
                else
                    return ResponseResultDTO.Failed("There are a problem occured during updating a match");
            }
            catch (Exception ex)
            {
                return ResponseResultDTO.Failed("There are a problem occured during updating a match\nPlease Enter the Id of the match");
            }
        }

        public async Task<ResponseResultDTO> Delete(int id)
        {
            try
            {
                var match = await _matchRepository.GetByIdAsync(id);
                if (match is null)
                    return ResponseResultDTO.Failed("The Match is not Exist");

                var matchDTO = mapper.Map<MatchDTO>(match);
                await _matchRepository.DeleteAsync(match);
                if (await _matchRepository.Save())
                    return new ResponseResultDTO { Success = true, Message = "The Match Deleted Successfully", Data = matchDTO };
                else
                    return ResponseResultDTO.Failed("There are a problem occured during deleting a match");
            }
            catch (Exception ex)
            {
                return ResponseResultDTO.Failed("There are a problem occured during deleting a match\nPlease Enter the Id of the match");
            }
        }

        public async Task<ResponseResultDTO> GetAll()
        {
            try
            {
                var matchList = await _matchRepository.GetAllAsync();
                var matchDTOList = mapper.Map<List<MatchDTO>>(matchList);

                if (matchList.Count() == 0)
                    return ResponseResultDTO.Failed("There are no Matchs");
                else
                    return new ResponseResultDTO { Success = true, Message = "The Matchs Get All process Done Successfully", Data = matchDTOList };
            }
            catch (Exception ex)
            {
                return ResponseResultDTO.Failed("There are a problem occured during Get All Matchs");
            }
        }

        public async Task<ResponseResultDTO> GetById(int id)
        {
            try
            {
                var match = await _matchRepository.GetByIdAsync(id);
                if (match is null)
                    return ResponseResultDTO.Failed("The Match is not Exist");

                var matchDTO = mapper.Map<MatchDTO>(match);
                return new ResponseResultDTO { Success = true, Message = "The Match GetByID Process Done Successfully", Data = matchDTO };
            }
            catch (Exception ex)
            {
                return ResponseResultDTO.Failed("There are a problem occured during getting a match");
            }
        }




        #region HELPER METHODS
        private async Task<ResponseResultDTO> CheckMatchConstraints(MatchDTO matchDTO)
        {
            try {
                if (matchDTO.HomeTeamId == matchDTO.HostedTeamId)
                    return ResponseResultDTO.Failed("Wrong!! Please make sure that The Home Team Id is different about Hosted Team Id");

                if (!(await tourmentService.GetById(matchDTO.TourmentId)).Success)
                    return ResponseResultDTO.Failed("the Assigned Tourment Id doesn't exist");

                if (!(await teamService.GetById(matchDTO.HomeTeamId)).Success)
                    return ResponseResultDTO.Failed("the Assigned Home Team Id doesn't exist");

                if (!(await teamService.GetById(matchDTO.HostedTeamId)).Success)
                    return ResponseResultDTO.Failed("the Assigned Hosted Team Id doesn't exist");

                return ResponseResultDTO.Succeeded("The Match Contraints are Done");
            }
            catch(Exception ex) {
                return ResponseResultDTO.Failed("There are a problem occured during Checking the match constraints");
            }
        }
        
        #endregion
    }
}
