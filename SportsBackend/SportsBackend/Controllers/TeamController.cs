using Core.Abstractions;
using Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace SportsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpPost("Update")]
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



    }
}
