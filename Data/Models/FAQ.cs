namespace PortfolioWebAPI.Data.Models;

public class FAQ
{
    public int Id { get; set; }
    public string Question { get; set; } = String.Empty;
    public string? Answer { get; set; }
    public int UpVotes { get; set; } = 0;
    public int DownVotes { get; set; } = 0;
    public bool IsSiteReady { get; set; } = false;
}