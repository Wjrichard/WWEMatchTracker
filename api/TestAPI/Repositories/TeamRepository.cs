using Microsoft.Extensions.Logging;
using System.Data.SQLite;
using TestAPI.Models;

namespace TestAPI.Repositories
{
    public class TeamRepository
    {
        public async Task<List<Team>> GetTeams()
        {
            string connectionString = "Data Source=Event-Tracker.sqlite;";
            List<Team> teams = new List<Team>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string checkTableQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='Team';";
                using (SQLiteCommand checkCommand = new SQLiteCommand(checkTableQuery, connection))
                {
                    var result = checkCommand.ExecuteScalar();
                    if (result == null)
                    {
                        string createTableQuery = "CREATE TABLE Team (TeamId INTEGER PRIMARY KEY, TeamName TEXT NOT NULL);";
                        using (SQLiteCommand createCommand = new SQLiteCommand(createTableQuery, connection))
                        {
                            createCommand.ExecuteNonQuery();
                        }
                    }
                }

                string query = "SELECT * FROM Team;";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Team team = new Team
                            {
                                TeamId = Convert.ToInt32(reader["TeamId"]),
                                TeamName = reader["TeamName"].ToString(),
                            };
                            teams.Add(team);
                        }
                        return teams;
                    }
                }
            }
        }

        public async Task<int> AddTeam(Team team)
        {
            string connectionString = "Data Source=Event-Tracker.sqlite;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string checkTableQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='Team';";
                using (SQLiteCommand checkCommand = new SQLiteCommand(checkTableQuery, connection))
                {
                    var result = checkCommand.ExecuteScalar();
                    if (result == null)
                    {
                        string createTableQuery = "CREATE TABLE Team (TeamId INTEGER PRIMARY KEY, TeamName TEXT NOT NULL);";
                        using (SQLiteCommand createCommand = new SQLiteCommand(createTableQuery, connection))
                        {
                            createCommand.ExecuteNonQuery();
                        }
                    }
                }

                string insertQuery = $"INSERT INTO Team VALUES (NULL, '{team.TeamName}');";
                using (SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, connection))
                {
                    return insertCommand.ExecuteNonQuery();
                }
            }
        }

        public async Task<int> UpdateTeam(Team team)
        {
            string connectionString = "Data Source=Event-Tracker.sqlite;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string updateQuery = $"UPDATE Team SET TeamName = @TeamName WHERE TeamId = @TeamId;";
                using (SQLiteCommand updateCommand = new SQLiteCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@TeamName", team.TeamName);
                    updateCommand.Parameters.AddWithValue("@TeamId", team.TeamId);
                    return await updateCommand.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
