using FootballAppSolution.Repository;
using System;
using System.Collections.Generic;
using FootballAppSolution.Model;

namespace FootballAppSolution.Service
{
    public class PlayerService
    {
        PlayerRepository playerRepository = new PlayerRepository();
        
        public void AddPlayer(Player player)
        {
            player.Id = Guid.NewGuid(); 
            playerRepository.AddPlayer(player);
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            return playerRepository.GetAllPlayers();
        }

        public Player GetPlayer(Guid id)
        {
            return playerRepository.GetPlayer(id);
        }

        public void UpdatePlayer(Player player)
        {
            playerRepository.UpdatePlayer(player);
        }

        public void DeletePlayer(Guid id)
        {
            playerRepository.DeletePlayer(id);
        }
    }
}