using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstractions
{
    public interface IAccountService
    {
        Task<ResponseResultDTO> Register(UserDTO userDTO);
        Task<ResponseResultDTO> Login(UserLoginDTO userLoginDTO);
        Task<ResponseResultDTO> Logout();

    }
}
