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
    public class PlayerService : IPlayerService
    {
        private readonly IGenericRepository<Player> _playerRepository;
        private readonly ITeamService teamService;
        private readonly IMapper mapper;

        public PlayerService(IGenericRepository<Player> playerRepository, ITeamService teamService, IMapper mapper)
        {
            _playerRepository = playerRepository;
            this.teamService = teamService;
            this.mapper = mapper;
        }


        public async Task<ResponseResultDTO> Add(PlayerDTO playerDTO)
        {
            try
            {
                var player = await _playerRepository.GetByExpression(x => x.FirstName.ToLower() == playerDTO.FirstName.ToLower()
                 && x.LastName.ToLower() == playerDTO.LastName.ToLower());

                playerDTO.Id = null;
                if (player is not null)
                    return ResponseResultDTO.Failed("The Player is already Exist");

                if(!(await teamService.GetById(playerDTO.TeamId)).Success)
                    return ResponseResultDTO.Failed("the Assigned Team Id doesn't exist");


                await _playerRepository.AutoMapperAddAsync(playerDTO);
                if (await _playerRepository.Save())
                    return ResponseResultDTO.Succeeded("The Player Added Successfully");
                else
                    return ResponseResultDTO.Failed("There are a problem occured during adding a player");
            }
            catch (Exception ex)
            {
                return ResponseResultDTO.Failed("There are a problem occured during adding a player");
            }
        }

        public async Task<ResponseResultDTO> Update(PlayerDTO playerDTO)
        {
            try
            {
                var player = await _playerRepository.GetByIdAsync(playerDTO.Id.Value);
                if (player is null)
                    return ResponseResultDTO.Failed("The Player is not Exist");

                if (!(await teamService.GetById(playerDTO.TeamId)).Success)
                    return ResponseResultDTO.Failed("the Assigned Team Id doesn't exist");


                await _playerRepository.AutoMapperUpdateAsync(playerDTO);
                if (await _playerRepository.Save())
                    return new ResponseResultDTO { Success = true, Message = "The Player Updated Successfully", Data = playerDTO };
                else
                    return ResponseResultDTO.Failed("There are a problem occured during updating a player");
            }
            catch (Exception ex)
            {
                return ResponseResultDTO.Failed("There are a problem occured during updating a player\nPlease Enter the Id of the player");
            }
        }


        public async Task<ResponseResultDTO> Delete(int id)
        {
            try
            {
                var player = await _playerRepository.GetByIdAsync(id);
                if (player is null)
                    return ResponseResultDTO.Failed("The Player is not Exist");

                var playerDTO = mapper.Map<PlayerDTO>(player);
                await _playerRepository.DeleteAsync(player);
                if (await _playerRepository.Save())
                    return new ResponseResultDTO { Success = true, Message = "The Player Deleted Successfully", Data = playerDTO };
                else
                    return ResponseResultDTO.Failed("There are a problem occured during deleting a player");
            }
            catch (Exception ex)
            {
                return ResponseResultDTO.Failed("There are a problem occured during deleting a player\nPlease Enter the Id of the player");
            }
        }

        public async Task<ResponseResultDTO> GetAll()
        {
            try
            {
                var playerList = await _playerRepository.GetAllAsync();
                var playerDTOList = mapper.Map<List<PlayerDTO>>(playerList);

                if (playerList.Count() == 0)
                    return ResponseResultDTO.Failed("There are no Players");
                else
                    return new ResponseResultDTO { Success = true, Message = "The Players Get All process Done Successfully", Data = playerDTOList };
            }
            catch (Exception ex)
            {
                return ResponseResultDTO.Failed("There are a problem occured during Get All Players");
            }
        }

        public async Task<ResponseResultDTO> GetById(int id)
        {
            try
            {
                var player = await _playerRepository.GetByIdAsync(id);
                if (player is null)
                    return ResponseResultDTO.Failed("The Player is not Exist");

                var playerDTO = mapper.Map<PlayerDTO>(player);
                return new ResponseResultDTO { Success = true, Message = "The Player GetByID Process Done Successfully", Data = playerDTO };
            }
            catch (Exception ex)
            {
                return ResponseResultDTO.Failed("There are a problem occured during getting a player");
            }
        }

        
    }
}
