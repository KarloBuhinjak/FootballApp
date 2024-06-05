using FootballAppSolution.Model;
using FootballAppSolution.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace FootballAppSolution.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FootballPlayerController : ControllerBase
    {
        PlayerService playerService = new PlayerService();
        
        [HttpPost]
        public async Task<IActionResult> AddPlayer([FromBody] Player player)
        {
            if (player == null)
            {
                return BadRequest("Player data is null.");
            }

            await playerService.AddPlayer(player);
            return Ok("Player added successfully.");
        }

        [HttpGet]
        public async Task<IActionResult>  GetAllPlayers()
        {
            var players = await playerService.GetAllPlayers();
            if (players == null || players.Count() == 0)
            {
                return NoContent();
            }
            return Ok(players);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlayer(Guid id)
        {
            var player = await playerService.GetPlayer(id);
            if (player == null)
            {
                return NotFound();
            }
            return Ok(player);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlayer(Guid id, [FromBody] Player updatedPlayer)
        {
            var player = await playerService.GetPlayer(id);
            if (player == null)
            {
                return NotFound();
            }

            updatedPlayer.Id = id;
            playerService.UpdatePlayer(updatedPlayer);
            return Ok("Player updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(Guid id)
        {
            var player = await playerService.GetPlayer(id);
            if (player == null)
            {
                return NotFound();
            }

            await playerService.DeletePlayer(id);
            return Ok("Player deleted successfully.");
        }
    }
}
