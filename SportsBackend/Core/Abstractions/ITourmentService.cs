using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstractions
{
    public interface ITourmentService
    {
        Task<ResponseResultDTO> Add(TourmentDTO tourmentDTO);
        Task<ResponseResultDTO> Update(TourmentDTO tourmentDTO);
        Task<ResponseResultDTO> Delete(int id);
        Task<ResponseResultDTO> GetAll();
        Task<ResponseResultDTO> GetById(int id);
        Task<ResponseResultDTO> GetTeamsByTourmentId(int id);

    }
}
