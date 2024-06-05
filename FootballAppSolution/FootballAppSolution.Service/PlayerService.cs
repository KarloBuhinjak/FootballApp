using FootballAppSolution.Repository;
using System;
using System.Collections.Generic;
using FootballAppSolution.Model;
using FootballAppSolution.Service.Common;

namespace FootballAppSolution.Service
{
    public class PlayerService: IFootballAppService

    {
    PlayerRepository playerRepository = new PlayerRepository();

    public async Task AddPlayer(Player player)
    {
        player.Id = Guid.NewGuid();
        await playerRepository.AddPlayer(player);
    }

    public async Task<IEnumerable<Player>> GetAllPlayers()
    {
        return await playerRepository.GetAllPlayers();
    }

    public async Task<Player> GetPlayer(Guid id)
    {
        return await playerRepository.GetPlayer(id);
    }

    public  async Task UpdatePlayer(Player player)
    {
        await playerRepository.UpdatePlayer(player);
    }

    public  async Task DeletePlayer(Guid id)
    {
        await playerRepository.DeletePlayer(id);
    }
    }
}