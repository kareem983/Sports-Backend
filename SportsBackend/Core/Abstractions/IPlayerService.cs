using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstractions
{
    public interface IPlayerService
    {
        Task<ResponseResultDTO> Add(PlayerDTO playerDTO);
        Task<ResponseResultDTO> Update(PlayerDTO playerDTO);
        Task<ResponseResultDTO> Delete(int id);
        Task<ResponseResultDTO> GetAll();
        Task<ResponseResultDTO> GetById(int id);
    }
}
