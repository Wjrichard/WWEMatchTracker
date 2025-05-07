using System.Data.SQLite;
using TestAPI.Models;

namespace TestAPI.Repositories
{
    public class UserRepository
    {
        public async Task<List<Models.User>> GetUsers()
        {
            string connectionString = "Data Source=Event-Tracker.sqlite;";
            List<Models.User> users = new List<User>();


            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string checkTableQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='AppUsers';";
                using (SQLiteCommand checkCommand = new SQLiteCommand(checkTableQuery, connection))
                {
                    var result = checkCommand.ExecuteScalar();
                    if (result == null)
                    {
                        string createTableQuery = "CREATE TABLE AppUsers (UserId INTEGER PRIMARY KEY,Email TEXT NOT NULL,Username TEXT NOT NULL);";

                        using (SQLiteCommand createCommand = new SQLiteCommand(createTableQuery, connection))
                        {
                            createCommand.ExecuteNonQuery();
                        }

                        string insertQuery = "INSERT INTO AppUsers VALUES (NULL,'wyattrichard018@gmail.com','wrichard');";

                        using (SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, connection))
                        {
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                    string query = "SELECT * FROM AppUsers";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                User user = new User
                                {
                                    UserId = Convert.ToInt32(reader["UserId"]),
                                    Email = reader["Email"].ToString(),
                                    Username = reader["Username"].ToString()
                                };
                                users.Add(user);
                            }
                            return users;
                        }
                    }
                }
            }
        }

        public async Task<int> AddUser(Models.User user)
        {
            string connectionString = "Data Source=Event-Tracker.sqlite;";
            
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string checkTableQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='AppUsers';";
                using (SQLiteCommand checkCommand = new SQLiteCommand(checkTableQuery, connection))
                {
                    var result = checkCommand.ExecuteScalar();
                    if (result == null)
                    {
                        string createTableQuery = "CREATE TABLE AppUsers (UserId INTEGER PRIMARY KEY,Email TEXT NOT NULL,Username TEXT NOT NULL);";

                        using (SQLiteCommand createCommand = new SQLiteCommand(createTableQuery, connection))
                        {
                            createCommand.ExecuteNonQuery();
                        }
                    }

                    string insertQuery = $"INSERT INTO AppUsers VALUES (NULL,'{user.Email}','{user.Username}');";

                    using (SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, connection))
                    {
                        return insertCommand.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}