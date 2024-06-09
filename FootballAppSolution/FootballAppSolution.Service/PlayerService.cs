using FootballAppSolution.Common;
using FootballAppSolution.Model;
using FootballAppSolution.Repository.Common;
using FootballAppSolution.Service.Common;

namespace FootballAppSolution.Service
{
    public class PlayerService: IFootballAppService

    {
    
    private readonly IFootballAppRepository playerRepository;
        
    public PlayerService(IFootballAppRepository playerRepository)
    {
        this.playerRepository = playerRepository;
    }
    public async Task AddPlayer(PlayerRequest playerRequest)
    {
        playerRequest.Id = Guid.NewGuid();
        await playerRepository.AddPlayer(playerRequest);
    }

    public async Task<IEnumerable<Player>> GetAllPlayers()
    {
        return await playerRepository.GetAllPlayers();
    }

    public async Task<Player> GetPlayer(Guid id)
    {
        return await playerRepository.GetPlayer(id);
    }

    public async Task<IEnumerable<Player>> GetFilteredPlayers(PlayerFiltering filtering, PlayerSorting sorting, PlayerPaging paging)
    {
        return await playerRepository.GetFilteredPlayers(filtering, sorting, paging);

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