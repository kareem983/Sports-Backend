using Core.Abstractions;
using Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace SportsBackend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly IConfiguration config;

        public AccountController(IAccountService accountService, IConfiguration config)
        {
            this.accountService = accountService;
            this.config = config;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            if(ModelState.IsValid)
            {
                ResponseResultDTO result = await accountService.Register(userDTO);
                if (result.Success)
                    return Ok(result);
                else
                    return BadRequest(result.Message);
            }

            return BadRequest(ModelState);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO userLogin)
        {
            if (ModelState.IsValid)
            {
                ResponseResultDTO result = await accountService.Login(userLogin, GetTokenConfiguration());
                if (result.Success)
                    return Ok(result);
                else
                    return Unauthorized(result.Message);
            }

            return BadRequest(ModelState);
        }


        [Authorize]
        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            if (ModelState.IsValid)
            {
                ResponseResultDTO result = await accountService.Logout();
                if (result.Success)
                    return Ok(result);
                else
                    return Unauthorized(result.Message);
            }

            return BadRequest(ModelState);
        }


        #region HELPER METHODS
        private TokenConfigurationDTO GetTokenConfiguration() => new TokenConfigurationDTO{
                ValidIssuer = config["JWT:ValidIssuer"],
                ValidAudience = config["JWT:ValidAudience"],
                SecretKey = config["JWT:SecretKey"]
        };
        #endregion

    }
}
