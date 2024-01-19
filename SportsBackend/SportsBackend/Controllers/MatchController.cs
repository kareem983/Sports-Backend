using Core.Abstractions;
using Core.DTOs;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SportsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MatchController : ControllerBase
    {
        private readonly IMatchService matchService;

        public MatchController(IMatchService matchService)
        {
            this.matchService = matchService;
        }


        [HttpPost("Add")]
        public async Task<IActionResult> AddMatch([FromBody] MatchDTO matchDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await matchService.Add(matchDTO);
                if (result.Success)
                    return Ok(result);
                else
                    return BadRequest(result.Message);
            }
            else
                return BadRequest(ModelState);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateMatch([FromBody] MatchDTO matchDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await matchService.Update(matchDTO);
                if (result.Success)
                    return Ok(result);
                else
                    return BadRequest(result.Message);
            }
            else
                return BadRequest(ModelState);
        }


        [HttpDelete("DeleteById/{id}")]
        public async Task<IActionResult> DeleteMatch(int id)
        {
            if (ModelState.IsValid)
            {
                var result = await matchService.Delete(id);
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
                var result = await matchService.GetAll();
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
                var result = await matchService.GetById(id);
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
