using FootballAppSolution.Model;
using Npgsql;
using System;
using System.Collections.Generic;

namespace FootballAppSolution.Repository
{
    public class PlayerRepository
    {
        private readonly string connectionString = "Host=localhost;Port=5432;Database=Football;Username=postgres;Password=lozinka;";

        public void AddPlayer(Player player)
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            var commandText = "INSERT INTO \"Player\" (\"Id\", \"Name\", \"Age\", \"Position\", \"ClubId\") VALUES (@Id, @Name, @Age, @Position, @ClubId);";

            using var command = new NpgsqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@Id", player.Id);
            command.Parameters.AddWithValue("@Name", player.Name);
            command.Parameters.AddWithValue("@Age", player.Age);
            command.Parameters.AddWithValue("@Position", player.Position);
            command.Parameters.AddWithValue("@ClubId", player.ClubId);

            command.ExecuteNonQuery();
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            var players = new List<Player>();

            using var connection = new NpgsqlConnection(connectionString);
            var commandText = "SELECT * FROM \"Player\";";

            using var command = new NpgsqlCommand(commandText, connection);
            connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
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

        public Player GetPlayer(Guid id)
        {
            using var connection = new NpgsqlConnection(connectionString);
            var commandText = "SELECT * FROM \"Player\" WHERE \"Id\" = @Id;";
            using var command = new NpgsqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            using var reader = command.ExecuteReader();
            if (reader.Read())
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

        public void UpdatePlayer(Player player)
        {
            using var connection = new NpgsqlConnection(connectionString);
            var commandText = "UPDATE \"Player\" SET \"Name\" = @Name, \"Age\" = @Age, \"Position\" = @Position, \"ClubId\" = @ClubId WHERE \"Id\" = @Id;";
            using var command = new NpgsqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@Id", player.Id);
            command.Parameters.AddWithValue("@Name", player.Name);
            command.Parameters.AddWithValue("@Age", player.Age);
            command.Parameters.AddWithValue("@Position", player.Position);
            command.Parameters.AddWithValue("@ClubId", player.ClubId);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void DeletePlayer(Guid id)
        {
            using var connection = new NpgsqlConnection(connectionString);
            var commandText = "DELETE FROM \"Player\" WHERE \"Id\" = @Id;";
            using var command = new NpgsqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
