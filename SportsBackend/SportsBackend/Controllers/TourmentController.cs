using Core.Abstractions;
using Core.DTOs;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Constants;
using System.Data;

namespace SportsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TourmentController : ControllerBase
    {
        private readonly ITourmentService tourmentService;

        public TourmentController(ITourmentService tourmentService)
        {
            this.tourmentService = tourmentService;
        }


        [HttpPost("AddTourmentTeam")]
        [Authorize(Roles = Role.Admin)]
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
        [Authorize(Roles = Role.Admin)]
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


        [HttpDelete("DeleteById/{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> DeleteTourment(int id)
        {
            if (ModelState.IsValid)
            {
                var result = await tourmentService.Delete(id);
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
                var result = await tourmentService.GetAll();
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
                var result = await tourmentService.GetById(id);
                if (result.Success)
                    return Ok(result);
                else
                    return BadRequest(result.Message);
            }
            else
                return BadRequest(ModelState);
        }


        [HttpGet("GetTeamsIDsByTourmentId/{id}")]
        public async Task<IActionResult> GetTeamsIDsByTourmentId(int id)
        {
            if (ModelState.IsValid)
            {
                var result = await tourmentService.GetTeamsIDsByTourmentId(id);
                if (result.Success)
                    return Ok(result);
                else
                    return BadRequest(result.Message);
            }
            else
                return BadRequest(ModelState);
        }


        [HttpGet("GetTeamsByTourmentId/{id}")]
        public async Task<IActionResult> GetTeamsByTourmentId(int id)
        {
            if (ModelState.IsValid)
            {
                var result = await tourmentService.GetTeamsByTourmentId(id);
                if (result.Success)
                    return Ok(result);
                else
                    return BadRequest(result.Message);
            }
            else
                return BadRequest(ModelState);
        }

        [HttpGet("GetMatchsIDsByTourmentId/{id}")]
        public async Task<IActionResult> GetMatchsIDsByTourmentId(int id)
        {
            if (ModelState.IsValid)
            {
                var result = await tourmentService.GetMatchsIDsByTourmentId(id);
                if (result.Success)
                    return Ok(result);
                else
                    return BadRequest(result.Message);
            }
            else
                return BadRequest(ModelState);
        }

        [HttpGet("GetMatchsByTourmentId/{id}")]
        public async Task<IActionResult> GetMatchsByTourmentId(int id)
        {
            if (ModelState.IsValid)
            {
                var result = await tourmentService.GetMatchsByTourmentId(id);
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
