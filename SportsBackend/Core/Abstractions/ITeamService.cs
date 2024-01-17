using Core.DTOs;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstractions
{
    public interface ITeamService
    {
        Task<ResponseResultDTO> Add(TeamDTO teamDTO);
        Task<ResponseResultDTO> Update(TeamDTO teamDTO);
        Task<ResponseResultDTO> Delete(int id);
        Task<ResponseResultDTO> GetAll();
        Task<ResponseResultDTO> GetById(int id);
    }
}
