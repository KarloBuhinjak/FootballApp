using System;
using System.Collections.Generic;
using FootballAppSolution.Model;

namespace FootballAppSolution.Service.Common
{
    public interface IFootballAppService
    {
        void AddPlayer(Player player);
        IEnumerable<Player> GetAllPlayers();
        Player GetPlayer(Guid id);
        void UpdatePlayer(Player player);
        void DeletePlayer(Guid id);
    }
}