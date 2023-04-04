namespace Boredtopia.Models;

public class TicTacToe
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int Wins { get; set; }
    public int Ties { get; set; }
    public int Losses { get; set; }
    public int TotalGames { get; set; }
    public double WinPercentDecimal { get; set; }
    public int WinStreak { get; set; }
}