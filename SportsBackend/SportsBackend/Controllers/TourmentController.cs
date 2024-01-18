using Core.Abstractions;
using Core.DTOs;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SportsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourmentController : ControllerBase
    {
        private readonly ITourmentService tourmentService;

        public TourmentController(ITourmentService tourmentService)
        {
            this.tourmentService = tourmentService;
        }

        [HttpPost("AddTourmentTeam")]
        public async Task<IActionResult> AddTourmentTeam([FromBody] TourmentDTO tourmentDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await tourmentService.Add(tourmentDTO);
                if (result.Success)
                    return Ok(result);
                else
                    return BadRequest(result.Message);
            }
            else
                return BadRequest(ModelState);
        }

        [HttpPut("UpdateTourmentTeam")]
        public async Task<IActionResult> UpdateTourmentTeam([FromBody] TourmentDTO tourmentDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await tourmentService.Update(tourmentDTO);
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
