using Microsoft.Extensions.Logging;
using System.Data.SQLite;
using TestAPI.Models;

namespace TestAPI.Repositories
{
    public class EventRepository
    {
        public async Task<List<EventDetails>> GetEventDetails()
        {
            string connectionString = "Data Source=Event-Tracker.sqlite;";
            List<EventDetails> details = new List<EventDetails>();


            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = $"select e.*,IFNULL(M.MatchId, 0) as MatchId,IFNULL(M.MatchName, '') as MatchName from Events e left join main.EventMatches EM on e.EventId = EM.EventId left join main.Match M on EM.MatchId = M.MatchId Order by e.EventId, m.MatchId;";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        var matches = new List<Match>();
                        Event e = new Event();
                        int curEventId = 0;
                        while (reader.Read())
                        {
                            if (curEventId != Convert.ToInt32(reader["EventId"]))
                            {
                                curEventId = Convert.ToInt32(reader["EventId"]);
                                if (e.EventId > 0)
                                {
                                    details.Add(new EventDetails() { Event = e, Matches = matches });
                                }
                                e = new Event();
                                matches = new List<Match>();
                                e.EventId = Convert.ToInt32(reader["EventId"]);
                                e.EventName = reader["EventName"].ToString();
                                e.EventDate = reader["EventDate"].ToString();
                                e.EventImage = reader["EventImage"].ToString();
                            }

                            var matchId = Convert.ToInt32(reader["MatchId"] ?? 0);
                            var matchName = reader["MatchName"].ToString();
                            if (matchId > 0)
                                matches.Add(new Match()
                                {
                                    MatchId = matchId,
                                    MatchName = matchName
                                });

                        }
                        details.Add(new EventDetails() { Event = e, Matches = matches });
                        return details;
                    }
                }
            }
        }

        public async Task<int> AddEvent(Event e)
        {
            string connectionString = "Data Source=Event-Tracker.sqlite;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string checkTableQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='Events';";
                using (SQLiteCommand checkCommand = new SQLiteCommand(checkTableQuery, connection))
                {
                    var result = checkCommand.ExecuteScalar();
                    if (result == null)
                    {
                        string createTableQuery = "CREATE TABLE Events (EventId INTEGER PRIMARY KEY,EventName TEXT NOT NULL,EventImage TEXT, EventDate TEXT NOT NULL;";

                        using (SQLiteCommand createCommand = new SQLiteCommand(createTableQuery, connection))
                        {
                            createCommand.ExecuteNonQuery();
                        }
                    }

                    string insertQuery = $"INSERT INTO Events VALUES (NULL,'{e.EventName}','{e.EventImage}','{e.EventDate}');";

                    using (SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, connection))
                    {
                        return insertCommand.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
