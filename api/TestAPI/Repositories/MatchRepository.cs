using System.Data.SQLite;
using TestAPI.Models;

namespace TestAPI.Repositories
{
    public class MatchRepository
    {
        public async Task<List<Match>> GetMatches()
        {
            string connectionString = "Data Source=Event-Tracker.sqlite;";
            List<Match> matches = new List<Match>();


            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string checkTableQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='Match';";
                using (SQLiteCommand checkCommand = new SQLiteCommand(checkTableQuery, connection))
                {
                    var result = checkCommand.ExecuteScalar();
                    if (result == null)
                    {
                        string createTableQuery = "CREATE TABLE Match (MatchId INTEGER PRIMARY KEY,MatchName TEXT NOT NULL);";

                        using (SQLiteCommand createCommand = new SQLiteCommand(createTableQuery, connection))
                        {
                            createCommand.ExecuteNonQuery();
                        }

                        string insertQuery = "INSERT INTO Match VALUES (NULL,'MatchName');";

                        using (SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, connection))
                        {
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                    string query = "SELECT * FROM Match";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Match match = new Match
                                {
                                    MatchId = Convert.ToInt32(reader["MatchId"]),
                                    MatchName = reader["MatchName"].ToString(),
                                };
                                matches.Add(match);
                            }
                            return matches;
                        }
                    }
                }
            }
        }

        public async Task<int> AddMatch(Match match)
        {
            string connectionString = "Data Source=Event-Tracker.sqlite;";
            
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string checkTableQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='Match';";
                using (SQLiteCommand checkCommand = new SQLiteCommand(checkTableQuery, connection))
                {
                    var result = checkCommand.ExecuteScalar();
                    if (result == null)
                    {
                        string createTableQuery = "CREATE TABLE Match (MatchId INTEGER PRIMARY KEY, MatchName TEXT NOT NULL);";

                        using (SQLiteCommand createCommand = new SQLiteCommand(createTableQuery, connection))
                        {
                            createCommand.ExecuteNonQuery();
                        }
                    }

                    string insertQuery = $"INSERT INTO Match VALUES (NULL,'{match.MatchName}');";

                    using (SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, connection))
                    {
                        return insertCommand.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
