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
                        string createTableQuery = "CREATE TABLE Match (MatchId INTEGER PRIMARY KEY,ShortName TEXT NOT NULL,EventImage TEXT, Team1 TEXT NOT NULL, Team2 TEXT NOT NULL,Team3 TEXT,Team4 TEXT,Team5 TEXT,Team6 TEXT,Team7 TEXT,Team8 TEXT);";

                        using (SQLiteCommand createCommand = new SQLiteCommand(createTableQuery, connection))
                        {
                            createCommand.ExecuteNonQuery();
                        }

                        string insertQuery = "INSERT INTO Match VALUES (NULL, 0, 'ShortName', '', 'Team1', 'Team2', 'Team3', 'Team4', 'Team5', 'Team6', 'Team7', 'Team8');";

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
                                    ShortName = reader["ShortName"].ToString(),
                                    EventImage = reader["EventImage"].ToString(),
                                    Team1 = reader["Team1"].ToString(),
                                    Team2 = reader["Team2"].ToString(),
                                    Team3 = reader["Team3"].ToString(),
                                    Team4 = reader["Team4"].ToString(),
                                    Team5 = reader["Team5"].ToString(),
                                    Team6 = reader["Team6"].ToString(),
                                    Team7 = reader["Team7"].ToString(),
                                    Team8 = reader["Team8"].ToString()
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
                        string createTableQuery = "CREATE TABLE Match (MatchId INTEGER PRIMARY KEY,ShortName TEXT NOT NULL,EventImage TEXT, Team1 TEXT NOT NULL, Team2 TEXT NOT NULL,Team3 TEXT,Team4 TEXT,Team5 TEXT,Team6 TEXT,Team7 TEXT,Team8 TEXT);";

                        using (SQLiteCommand createCommand = new SQLiteCommand(createTableQuery, connection))
                        {
                            createCommand.ExecuteNonQuery();
                        }
                    }

                    string insertQuery = $"INSERT INTO Match VALUES (NULL,'{match.EventImage}','{match.ShortName}','{match.Team1}','{match.Team2}','{match.Team3}','{match.Team4}','{match.Team5}','{match.Team6}','{match.Team7}','{match.Team8}');";

                    using (SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, connection))
                    {
                        return insertCommand.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
