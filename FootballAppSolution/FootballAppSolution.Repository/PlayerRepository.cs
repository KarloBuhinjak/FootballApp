using FootballAppSolution.Model;
using Npgsql;
using System.Text;
using FootballAppSolution.Common;
using FootballAppSolution.Repository.Common;

namespace FootballAppSolution.Repository
{
    public class PlayerRepository: IFootballAppRepository
    {
        private readonly string connectionString = "Host=localhost;Port=5432;Database=Football;Username=postgres;Password=lozinka;";

        public async Task AddPlayer(PlayerRequest player)
        {
            await using var connection = new NpgsqlConnection(connectionString);
            await  connection.OpenAsync();

            var commandText = "INSERT INTO \"Player\" (\"Id\", \"Name\", \"Age\", \"Position\", \"ClubId\") VALUES (@Id, @Name, @Age, @Position, @ClubId);";

            await using var command = new NpgsqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@Id", player.Id);
            command.Parameters.AddWithValue("@Name", player.Name);
            command.Parameters.AddWithValue("@Age", player.Age);
            command.Parameters.AddWithValue("@Position", player.Position);
            command.Parameters.AddWithValue("@ClubId", player.ClubId);

            await command.ExecuteNonQueryAsync();
        }

        public async Task<IEnumerable<Player>> GetAllPlayers()
        {
            var players = new List<Player>();

            await using var connection = new NpgsqlConnection(connectionString);
            var commandText = "SELECT * FROM \"Player\";";

            await using var command = new NpgsqlCommand(commandText, connection);
            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                var player = new Player
                {
                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Age = reader.GetInt32(reader.GetOrdinal("Age")),
                    Position = reader.GetString(reader.GetOrdinal("Position")),
                    ClubId = reader.GetGuid(reader.GetOrdinal("ClubId"))
                };
                players.Add(player);
            }

            return players;
        }

        public async Task<Player> GetPlayer(Guid id)
        {
            await using var connection = new NpgsqlConnection(connectionString);
            var commandText = "SELECT * FROM \"Player\" WHERE \"Id\" = @Id;";
            await using var command = new NpgsqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@Id", id);

            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Player
                {
                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Age = reader.GetInt32(reader.GetOrdinal("Age")),
                    Position = reader.GetString(reader.GetOrdinal("Position")),
                    ClubId = reader.GetGuid(reader.GetOrdinal("ClubId"))
                };
            }

            return null;
        }

        public async Task<IEnumerable<Player>> GetFilteredPlayers(PlayerFiltering filtering, PlayerSorting sorting,
            PlayerPaging paging)
        {
                    
            var queryBuilder = new StringBuilder("SELECT * FROM \"Player\" WHERE 1 = 1");
            
            if (filtering.NameId != null)
            {
                queryBuilder.Append(" AND \"Id\" = @NameId");
            }
            if (filtering.Age.HasValue)
            {
                queryBuilder.Append(" AND \"Age\" = @Age");
            }
            if (!string.IsNullOrEmpty(filtering.Position))
            {
                queryBuilder.Append(" AND \"Position\" = @Position");
            }
            if (filtering.ClubId != null)
            {
                queryBuilder.Append(" AND \"ClubId\" IN (SELECT \"Id\" FROM \"Club\" WHERE \"Id\" = @ClubName)");
            }
            
            queryBuilder.Append($" ORDER BY \"{sorting.SortBy}\" {sorting.SortOrder}");
            
            queryBuilder.Append($" OFFSET {paging.PageSize * (paging.PageNumber - 1)} LIMIT {paging.PageSize};");
            
            using var connection = new NpgsqlConnection(connectionString);
            var players = new List<Player>();

            await connection.OpenAsync();
            using var command = new NpgsqlCommand(queryBuilder.ToString(), connection);
            
            if (filtering.NameId != null)
            {
                command.Parameters.AddWithValue("@NameId", filtering.NameId);
            }
            if (filtering.Age.HasValue)
            {
                command.Parameters.AddWithValue("@Age", filtering.Age);
            }
            if (!string.IsNullOrEmpty(filtering.Position))
            {
                command.Parameters.AddWithValue("@Position", filtering.Position);
            }
            if (filtering.ClubId != null)
            {
                command.Parameters.AddWithValue("@ClubName", filtering.ClubId);
            }

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var player = new Player
                {
                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Age = reader.GetInt32(reader.GetOrdinal("Age")),
                    Position = reader.GetString(reader.GetOrdinal("Position")),
                    ClubId = reader.GetGuid(reader.GetOrdinal("ClubId"))
                };
                players.Add(player);
            }

            return players;
            
        }

        public async Task UpdatePlayer(Player player)
        {
            await using var connection = new NpgsqlConnection(connectionString);
            var commandText = "UPDATE \"Player\" SET \"Name\" = @Name, \"Age\" = @Age, \"Position\" = @Position, \"ClubId\" = @ClubId WHERE \"Id\" = @Id;";
            await using var command = new NpgsqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@Id", player.Id);
            command.Parameters.AddWithValue("@Name", player.Name);
            command.Parameters.AddWithValue("@Age", player.Age);
            command.Parameters.AddWithValue("@Position", player.Position);
            command.Parameters.AddWithValue("@ClubId", player.ClubId);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

        }

        public async Task DeletePlayer(Guid id)
        {
            await using var connection = new NpgsqlConnection(connectionString);
            var commandText = "DELETE FROM \"Player\" WHERE \"Id\" = @Id;";
            await using var command = new NpgsqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@Id", id);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }
}
