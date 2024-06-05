using System;
using System.Collections.Generic;
using FootballAppSolution.Model;

namespace FootballAppSolution.Repository.Common
{
    public interface IFootballAppRepository
    {
        Task AddPlayer(Player player);
        Task<IEnumerable<Player>> GetAllPlayers();
        Task<Player> GetPlayer(Guid id);
        Task UpdatePlayer(Player player);
        Task DeletePlayer(Guid id);
    }
}