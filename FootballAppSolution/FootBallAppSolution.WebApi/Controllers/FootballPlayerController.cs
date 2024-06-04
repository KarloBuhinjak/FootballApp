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
        public IActionResult AddPlayer([FromBody] Player player)
        {
            if (player == null)
            {
                return BadRequest("Player data is null.");
            }

            playerService.AddPlayer(player);
            return Ok("Player added successfully.");
        }

        [HttpGet]
        public IActionResult GetAllPlayers()
        {
            var players = playerService.GetAllPlayers();
            if (players == null || players.Count() == 0)
            {
                return NoContent();
            }
            return Ok(players);
        }

        [HttpGet("{id}")]
        public IActionResult GetPlayer(Guid id)
        {
            var player = playerService.GetPlayer(id);
            if (player == null)
            {
                return NotFound();
            }
            return Ok(player);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePlayer(Guid id, [FromBody] Player updatedPlayer)
        {
            var player = playerService.GetPlayer(id);
            if (player == null)
            {
                return NotFound();
            }

            updatedPlayer.Id = id;
            playerService.UpdatePlayer(updatedPlayer);
            return Ok("Player updated successfully.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePlayer(Guid id)
        {
            var player = playerService.GetPlayer(id);
            if (player == null)
            {
                return NotFound();
            }

            playerService.DeletePlayer(id);
            return Ok("Player deleted successfully.");
        }
    }
}
