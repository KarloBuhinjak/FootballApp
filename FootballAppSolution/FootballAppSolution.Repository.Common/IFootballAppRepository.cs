using System;
using System.Collections.Generic;
using FootballAppSolution.Common;
using FootballAppSolution.Model;

namespace FootballAppSolution.Repository.Common
{
    public interface IFootballAppRepository
    {
        Task AddPlayer(Player player);
        Task<IEnumerable<Player>> GetAllPlayers();
        Task<Player> GetPlayer(Guid id);
        Task<IEnumerable<Player>> GetFilteredPlayers(PlayerFiltering filtering, PlayerSorting sorting, PlayerPaging paging);
        Task UpdatePlayer(Player player);
        Task DeletePlayer(Guid id);
    }
}