using Core.DTOs;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstractions
{
    public interface IJWTTokenService
    {
        Task<UserTokenDTO> CreateJWTToken(TokenConfigurationDTO tokenConfiguration, ApplicationUser user);
      
    }
}
