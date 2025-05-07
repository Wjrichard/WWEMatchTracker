using System.Data.SQLite;
using TestAPI.Models;

namespace TestAPI.Repositories
{
    public class PredictionRepository
    {
        public async Task<List<Prediction>> GetAllPredictions()
        {
            string connectionString = "Data Source=Event-Tracker.sqlite;";
            List<Prediction> predictions = new List<Prediction>();


            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string checkTableQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='Prediction';";
                using (SQLiteCommand checkCommand = new SQLiteCommand(checkTableQuery, connection))
                {
                    var result = checkCommand.ExecuteScalar();
                    if (result == null)
                    {
                        string createTableQuery = "CREATE TABLE Prediction (PredictionId INTEGER PRIMARY KEY,MatchId INTEGER NOT NULL,UserId INTEGER NOT NULL,MatchWinner TEXT);";

                        using (SQLiteCommand createCommand = new SQLiteCommand(createTableQuery, connection))
                        {
                            createCommand.ExecuteNonQuery();
                        }

                        string insertQuery = "INSERT INTO Prediction VALUES (NULL, 0, 0, 'MatchWinner');";

                        using (SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, connection))
                        {
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                    string query = "SELECT * FROM Prediction";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Prediction prediction = new Prediction
                                {
                                    PredictionId = Convert.ToInt32(reader["PredictionId"]),
                                    MatchId = Convert.ToInt32(reader["MatchId"]),
                                    UserId = Convert.ToInt32(reader["UserId"]),
                                    MatchWinner = reader["MatchWinner"].ToString()
                                };
                                predictions.Add(prediction);
                            }
                            return predictions;
                        }
                    }
                }
            }
        }

        public async Task<List<Prediction>> GetMatchPredictions(int matchId)
        {
            string connectionString = "Data Source=Event-Tracker.sqlite;";
            List<Prediction> predictions = new List<Prediction>();


            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string checkTableQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='Prediction';";
                using (SQLiteCommand checkCommand = new SQLiteCommand(checkTableQuery, connection))
                {
                    var result = checkCommand.ExecuteScalar();
                    if (result == null)
                    {
                        string createTableQuery = "CREATE TABLE Prediction (PredictionId INTEGER PRIMARY KEY,MatchId INTEGER NOT NULL,UserId INTEGER NOT NULL,MatchWinner TEXT);";

                        using (SQLiteCommand createCommand = new SQLiteCommand(createTableQuery, connection))
                        {
                            createCommand.ExecuteNonQuery();
                        }

                        string insertQuery = "INSERT INTO Prediction VALUES (NULL, 0, 0, 'MatchWinner');";

                        using (SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, connection))
                        {
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                    string query = $"SELECT * FROM Prediction WHERE MatchId = {matchId}";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Prediction prediction = new Prediction
                                {
                                    PredictionId = Convert.ToInt32(reader["PredictionId"]),
                                    MatchId = Convert.ToInt32(reader["MatchId"]),
                                    UserId = Convert.ToInt32(reader["UserId"]),
                                    MatchWinner = reader["MatchWinner"].ToString()
                                };
                                predictions.Add(prediction);
                            }
                            return predictions;
                        }
                    }
                }
            }
        }

        public async Task<int> AddPrediction(Prediction prediction)
        {
            string connectionString = "Data Source=Event-Tracker.sqlite;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string checkTableQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='Prediction';";
                using (SQLiteCommand checkCommand = new SQLiteCommand(checkTableQuery, connection))
                {
                    var result = checkCommand.ExecuteScalar();
                    if (result == null)
                    {
                        string createTableQuery = "CREATE TABLE Prediction (PredictionId INTEGER PRIMARY KEY,MatchId INTEGER NOT NULL,UserId INTEGER NOT NULL,MatchWinner TEXT);";

                        using (SQLiteCommand createCommand = new SQLiteCommand(createTableQuery, connection))
                        {
                            createCommand.ExecuteNonQuery();
                        }
                    }

                    string insertQuery = $"INSERT INTO Prediction VALUES (NULL,{prediction.PredictionId},{prediction.MatchId},{prediction.UserId},'{prediction.MatchWinner}');";

                    using (SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, connection))
                    {
                        return insertCommand.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
