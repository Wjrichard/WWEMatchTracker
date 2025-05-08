using System.Data.SQLite;
using TestAPI.Models;

namespace TestAPI.Repositories
{
    public class EventRepository
    {
        public async Task<List<Event>> GetEvents()
        {
            string connectionString = "Data Source=Event-Tracker.sqlite;";
            List<Event> events = new List<Event>();


            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string checkTableQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='Events';";
                using (SQLiteCommand checkCommand = new SQLiteCommand(checkTableQuery, connection))
                {
                    var result = checkCommand.ExecuteScalar();
                    if (result == null)
                    {
                        string createTableQuery = "CREATE TABLE Events (EventId INTEGER PRIMARY KEY,EventName TEXT NOT NULL,EventImage TEXT, EventDate TEXT NOT NULL);";

                        using (SQLiteCommand createCommand = new SQLiteCommand(createTableQuery, connection))
                        {
                            createCommand.ExecuteNonQuery();
                        }

                        string insertQuery = "INSERT INTO Events VALUES (NULL,'EventName', 'EventImage', strftime('%Y-%m-%d %H:%M:%S', datetime('now')));";

                        using (SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, connection))
                        {
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                    string query = "SELECT * FROM Events";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Event e = new Event
                                {
                                    EventId = Convert.ToInt32(reader["EventId"].ToString()),
                                    EventName = reader["EventName"].ToString(),
                                    EventImage = reader["EventImage"].ToString(),
                                    EventDate = reader["EventDate"].ToString()
                                };
                                events.Add(e);
                            }
                            return events;
                        }
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
