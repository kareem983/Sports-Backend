using Core.Abstractions;
using Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace SportsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService teamService;

        public TeamController(ITeamService teamService)
        {
            this.teamService = teamService;
        }


        [HttpPost("Add")]
        public async Task<IActionResult> AddTeam([FromBody]TeamDTO teamDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await teamService.Add(teamDTO);
                if (result.Success)
                    return Ok(result);
                else
                    return BadRequest(result.Message);
            }
            else 
                return BadRequest(ModelState);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateTeam([FromBody] TeamDTO teamDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await teamService.Update(teamDTO);
                if (result.Success)
                    return Ok(result);
                else
                    return BadRequest(result.Message);
            }
            else
                return BadRequest(ModelState);
        }

        
        [HttpDelete("DeleteById/{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            if (ModelState.IsValid)
            {
                var result = await teamService.Delete(id);
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
                var result = await teamService.GetAll();
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
                var result = await teamService.GetById(id);
                if (result.Success)
                    return Ok(result);
                else
                    return BadRequest(result.Message);
            }
            else
                return BadRequest(ModelState);
        }


        [HttpGet("GetPlayersIDsByTeamId/{id}")]
        public async Task<IActionResult> GetPlayersIDsByTeamId(int id)
        {
            if (ModelState.IsValid)
            {
                var result = await teamService.GetPlayersIDsByTeamId(id);
                if (result.Success)
                    return Ok(result);
                else
                    return BadRequest(result.Message);
            }
            else
                return BadRequest(ModelState);
        }


        [HttpGet("GetPlayersByTeamId/{id}")]
        public async Task<IActionResult> GetPlayersByTeamId(int id)
        {
            if (ModelState.IsValid)
            {
                var result = await teamService.GetPlayersByTeamId(id);
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
