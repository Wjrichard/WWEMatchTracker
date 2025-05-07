namespace TestAPI.Models
{
    public class Prediction
    {
        public int PredictionId { get; set; }
        public int MatchId { get; set; }
        public int UserId { get; set; }
        public string? MatchWinner { get; set; }
    }
}
