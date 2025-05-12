namespace TestAPI.Models;

public class MatchDetails
{
    public int EventId { get; set; }
    public Match Match { get; set; }
    public List<Team> Teams { get; set; }
}
