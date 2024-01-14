using AutoMapper.Configuration;
using Core.Abstractions;
using Core.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IJWTTokenService jwtTokenService;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IJWTTokenService jwtTokenService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jwtTokenService = jwtTokenService;
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

        public async Task<ResponseResultDTO> Login(UserLoginDTO userLoginDTO, TokenConfigurationDTO tokenConfiguration)
        {
            try
            {
                var result = await signInManager.PasswordSignInAsync(userLoginDTO.UserName, userLoginDTO.Password, false, false);

                if (result.Succeeded)
                {
                    ApplicationUser user = await userManager.FindByNameAsync(userLoginDTO.UserName);
                    return new ResponseResultDTO()
                    {
                        Success = true,
                        Message = "The User Signed In Successfully",
                        Data = await jwtTokenService.CreateJWTToken(tokenConfiguration, user)
                    };
                }
                return ResponseResultDTO.Failed("The username or password is wrong");
            }
            catch (Exception ex)
            {
                return ResponseResultDTO.Failed("There are a problem occurred during Sign In");
            }
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


        #region HELPER METHODS
        private async Task<bool> CheckUserIsExist(string UserName, string Email)
        {
            var existUserByUName = await userManager.FindByNameAsync(UserName);
            var existUserByEmail = await userManager.FindByEmailAsync(Email);

            return (existUserByUName != null || existUserByEmail != null);
        }

        private async Task<ResponseResultDTO> AddUserRole(ApplicationUser user, UserDTO userDTO)
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

        #endregion

    }
}
