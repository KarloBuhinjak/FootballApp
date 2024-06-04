using System;
using System.Collections.Generic;
using FootballAppSolution.Model;

namespace FootballAppSolution.Repository.Common
{
    public interface IFootballAppRepository
    {
        void AddPlayer(Player player);
        IEnumerable<Player> GetAllPlayers();
        Player GetPlayer(Guid id);
        void UpdatePlayer(Player player);
        void DeletePlayer(Guid id);
    }
}