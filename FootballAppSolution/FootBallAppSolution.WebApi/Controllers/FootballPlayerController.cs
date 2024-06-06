using FootballAppSolution.Model;
using Microsoft.AspNetCore.Mvc;
using FootballAppSolution.Common;
using FootballAppSolution.Service.Common;

namespace FootballAppSolution.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FootballPlayerController : ControllerBase
    {
        private readonly IFootballAppService playerService;
        
        public FootballPlayerController(IFootballAppService playerService)
        {
            this.playerService = playerService;
        }
        
        [HttpPost]
        public async Task<IActionResult> AddPlayer([FromBody] Player player)
        {
            try
            {
                if (player == null)
                {
                    return BadRequest("Player data is null.");
                }

                await playerService.AddPlayer(player);
                return Ok("Player added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlayers()
        {
            try
            {
                var players = await playerService.GetAllPlayers();
                if (players == null || !players.Any())
                {
                    return NoContent();
                }
                return Ok(players);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlayer(Guid id)
        {
            try
            {
                var player = await playerService.GetPlayer(id);
                if (player == null)
                {
                    return NotFound();
                }
                return Ok(player);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        [HttpGet("/players")]
        public async Task<IActionResult> GetFilteredPlayers([FromQuery] Guid? nameId, [FromQuery] int? age, [FromQuery] string? position, [FromQuery] Guid? clubId, [FromQuery] string sortBy = "Name", [FromQuery] string sortOrder = "asc", [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            
            try
            {
                var filtering = new PlayerFiltering
                {
                    NameId = nameId,
                    Age = age,
                    Position = position,
                    ClubId = clubId
                };

                var sorting = new PlayerSorting
                {
                    SortBy = sortBy,
                    SortOrder = sortOrder
                };

                var paging = new PlayerPaging
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                var players = await playerService.GetFilteredPlayers(filtering, sorting, paging);

                return Ok(players);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlayer(Guid id, [FromBody] Player updatedPlayer)
        {
            try
            {
                var player = await playerService.GetPlayer(id);
                if (player == null)
                {
                    return NotFound();
                }

                updatedPlayer.Id = id;
                await playerService.UpdatePlayer(updatedPlayer);
                return Ok("Player updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(Guid id)
        {
            try
            {
                var player = await playerService.GetPlayer(id);
                if (player == null)
                {
                    return NotFound();
                }

                await playerService.DeletePlayer(id);
                return Ok("Player deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
