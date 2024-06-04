namespace FootballAppSolution.WebApi.Models;

public class Club
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    
    public ICollection<Player> Players { get; set; }
}