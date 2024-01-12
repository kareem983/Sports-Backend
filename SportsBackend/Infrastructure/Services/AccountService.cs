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
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMapper mapper;

        public AccountService(UserManager<ApplicationUser> userManager, IMapper mapper, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.signInManager = signInManager;
        }


        public async Task<ResponseResultDTO> Register(UserDTO userDTO)
        {
            try
            {
                if (await CheckUserIsExist(userDTO.UserName, userDTO.Email))
                    return ResponseResultDTO.Failed("this user is already registered before");

                ApplicationUser user = new ApplicationUser();
                user.UserName = userDTO.UserName;
                user.Email = userDTO.Email;

                IdentityResult result = await userManager.CreateAsync(user, userDTO.Password);
                if (result.Succeeded)
                    return await AddUserRole(user, userDTO);
                else
                    return ResponseResultDTO.Failed("The User Data isn't valid, Please make sure the username doesn't have any spaces");
                
            }
            catch (Exception ex)
            {
                return ResponseResultDTO.Failed("There are a problem occurred during registeration");
            }

        }

        public async Task<ResponseResultDTO> Login(UserLoginDTO userLoginDTO)
        {
            var result = await signInManager.PasswordSignInAsync(userLoginDTO.UserName, userLoginDTO.Password, false, false);

            if (result.Succeeded)
            {
                return ResponseResultDTO.Succeeded("The User Signed In Successfully");
                // TODO --> replace the response and Create The Token
            }
            return ResponseResultDTO.Failed("The username or password is wrong");
        }


        public async Task<ResponseResultDTO> Logout()
        {
            try
            {
                await signInManager.SignOutAsync();
                return ResponseResultDTO.Succeeded("The user Signed Out Successfully");
            }
            catch (Exception)
            {
                return ResponseResultDTO.Failed("There are a problem occurred during Sign Out");
            }
        }


        private async Task<bool> CheckUserIsExist(string UserName, string Email)
        {
            var existUserByUName = await userManager.FindByNameAsync(UserName);
            var existUserByEmail = await userManager.FindByEmailAsync(Email);

            return (existUserByUName != null || existUserByEmail != null);
        }

        public async Task<ResponseResultDTO> AddUserRole(ApplicationUser user, UserDTO userDTO)
        {
            var roleResult = await userManager.AddToRoleAsync(user, "User");
            if (roleResult.Succeeded)
            {
                userDTO.Id = user.Id;
                return new ResponseResultDTO
                {
                    Success = true,
                    Message = "The User Registered Successfully",
                    Data = userDTO
                };
            }
            else
            {
                await userManager.DeleteAsync(user);
                return ResponseResultDTO.Failed("There are a problem occurred in the user role during registeration");
            }
        }


    }
}
