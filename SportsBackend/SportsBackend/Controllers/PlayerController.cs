using Core.Abstractions;
using Core.DTOs;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Constants;

namespace SportsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService playerService;

        public PlayerController(IPlayerService playerService)
        {
            this.playerService = playerService;
        }


        [HttpPost("Add")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> AddPlayer([FromBody] PlayerDTO playerDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await playerService.Add(playerDTO);
                if (result.Success)
                    return Ok(result);
                else
                    return BadRequest(result.Message);
            }
            else
                return BadRequest(ModelState);
        }

        [HttpPut("Update")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> UpdatePlayer([FromBody] PlayerDTO playerDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await playerService.Update(playerDTO);
                if (result.Success)
                    return Ok(result);
                else
                    return BadRequest(result.Message);
            }
            else
                return BadRequest(ModelState);
        }


        [HttpDelete("DeleteById/{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            if (ModelState.IsValid)
            {
                var result = await playerService.Delete(id);
                if (result.Success)
                    return Ok(result);
                else
                    return BadRequest(result.Message);
            }
            else
                return BadRequest(ModelState);
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            if (ModelState.IsValid)
            {
                var result = await playerService.GetAll();
                if (result.Success)
                    return Ok(result);
                else
                    return BadRequest(result.Message);
            }
            else
                return BadRequest(ModelState);
        }


        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (ModelState.IsValid)
            {
                var result = await playerService.GetById(id);
                if (result.Success)
                    return Ok(result);
                else
                    return BadRequest(result.Message);
            }
            else
                return BadRequest(ModelState);
        }


    }
}
