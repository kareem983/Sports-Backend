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
    public class JWTTokenService : IJWTTokenService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public JWTTokenService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<UserTokenDTO> CreateJWTToken(TokenConfigurationDTO tokenConfiguration, ApplicationUser user)
        {
            JwtSecurityToken JWTToken = new JwtSecurityToken(
                issuer: tokenConfiguration.ValidIssuer,
                audience: tokenConfiguration.ValidAudience,
                claims: await GetUserClaims(user),
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: await GetSigningCredentials(tokenConfiguration));

            return new UserTokenDTO()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(JWTToken),
                ExpirationDate = JWTToken.ValidTo
            };
        }

        private async Task<List<Claim>> GetUserClaims(ApplicationUser user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var roles = await userManager.GetRolesAsync(user);
            foreach (var roleName in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, roleName));
            }

            return claims;
        }

        private async Task<SigningCredentials> GetSigningCredentials(TokenConfigurationDTO tokenConfiguration)
        {
            SigningCredentials signingCred = new SigningCredentials(
                await GetSecurityKey(tokenConfiguration), SecurityAlgorithms.HmacSha256);

            return signingCred;
        }

        private async Task<SecurityKey> GetSecurityKey(TokenConfigurationDTO tokenConfiguration)
        {
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfiguration.SecretKey));
            return securityKey;
        }

    }
}
