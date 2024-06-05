using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FootballAppSolution.Common;
using FootballAppSolution.Model;

namespace FootballAppSolution.Service.Common
{
    public interface IFootballAppService
    {
        Task AddPlayer(Player player);
        Task<IEnumerable<Player>> GetAllPlayers();
        Task<Player> GetPlayer(Guid id);
        Task<IEnumerable<Player>> GetFilteredPlayers(PlayerFiltering filtering, PlayerSorting sorting, PlayerPaging paging);
        Task UpdatePlayer(Player player);
        Task DeletePlayer(Guid id);
    }
}