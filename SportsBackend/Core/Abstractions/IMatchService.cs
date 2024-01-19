using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstractions
{
    public interface IMatchService
    {
        Task<ResponseResultDTO> Add(MatchDTO matchDTO);
        Task<ResponseResultDTO> Update(MatchDTO matchDTO);
        Task<ResponseResultDTO> Delete(int id);
        Task<ResponseResultDTO> GetAll();
        Task<ResponseResultDTO> GetById(int id);
    }
}
