using System.Data.SQLite;
using TestAPI.Models;

namespace TestAPI.Repositories
{
    public class MatchRepository
    {
        public async Task<EventDetails> GetMatches(int eventId)
        {
            string connectionString = "Data Source=Event-Tracker.sqlite;";
            List<EventDetails> details = new List<EventDetails>();


            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                string query = $"select M.*, ifnull(T.TeamId,0) AS TeamId,ifnull(T.TeamName,'') as TeamName from Match M LEFT JOIN MatchTeams MT on MT.MatchId = M.MatchId LEFT JOIN main.Team T on MT.TeamId = T.TeamId LEFT JOIN EventMatches EV on M.MatchId = EV.MatchId WHERE EV.EventId = {eventId};";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                connection.Open();
                using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        var matches = new List<Match>();
                        Event e = new Event();
                        while (reader.Read())
                        {
                            matches.Add(new Match()
                            {
                                MatchId = Convert.ToInt32(reader["MatchId"]),
                                MatchName = reader["MatchName"].ToString()
                            });
                            e.EventId = Convert.ToInt32(reader["EventId"]);
                            e.EventName = reader["EventName"].ToString();
                            e.EventDate = reader["EventDate"].ToString();
                            e.EventImage = reader["EventImage"].ToString();
                        }
                        return new EventDetails() { Event = e, Matches = matches};
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

        public async Task<List<Team>> GetMatchParticipants(int matchId)
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
                        string createTableQuery = "CREATE TABLE Team (Team INTEGER PRIMARY KEY,TeamName TEXT NOT NULL);";

                        using (SQLiteCommand createCommand = new SQLiteCommand(createTableQuery, connection))
                        {
                            createCommand.ExecuteNonQuery();
                        }

                        string insertQuery = "INSERT INTO Team VALUES (NULL,'TeamName');";

                        using (SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, connection))
                        {
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                    string query = $"SELECT T.* FROM Match M INNER JOIN MatchTeams MT ON MT.MatchId = M.MatchId INNER JOIN Team T ON T.TeamId = MT.TeamId WHERE M.MatchId = {matchId}";
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
        }
    }
}
