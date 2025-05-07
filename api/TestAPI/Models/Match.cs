using System.Reflection.Metadata;

namespace TestAPI.Models;

public class Match
{
    public int MatchId { get; set; }
    public string ShortName { get; set; }
    public string? EventImage { get; set; }
    public string Team1 { get; set; }
    public string Team2 { get; set; }
    public string? Team3 { get; set; }
    public string? Team4 { get; set; }
    public string? Team5 { get; set; }
    public string? Team6 { get; set; }
    public string? Team7 { get; set; }
    public string? Team8 { get; set; }
}
