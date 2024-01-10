using AutoMapper;
using Core.Abstractions;
using Core.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public AccountService(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<ResponseResultDTO> Register(UserDTO userDTO)
        {
            ApplicationUser user = new ApplicationUser();
            //mapper.Map<UserDTO>(user);
            user.UserName = userDTO.UserName;
            user.Email = userDTO.Email;
            IdentityResult result = await userManager.CreateAsync(user, userDTO.Password);
            
            if (result.Succeeded)
            {
                return new ResponseResultDTO
                {
                    Success = true,
                    Message = "The User Registered Successfully",
                    Data = userDTO
                };
            }
            return new ResponseResultDTO {
                Success = false,
                Message = "this user is already registered before\nOR\nThere are a problem occurred during registeration",
            };
        }
    }
}
