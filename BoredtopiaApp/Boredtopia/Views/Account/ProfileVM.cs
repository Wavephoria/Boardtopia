namespace Boredtopia.Views.Home;

public class ProfileVM
{
    public string Name { get; set; }
    public string Email { get; set; }
    
    public int WordlePlays { get; set; }
    public int WordleBest { get; set; }
    public double WordleAverage { get; set; }
    
    public int RockWins { get; set; }
    public int PaperWins { get; set; }
    public int ScissorWins { get; set; }
    public int Highscore { get; set; }
    public int TotalGames { get; set; }
    public double WinPercentRpsDecimal { get; set; }

    public int tttGames { get; set; }
    public int tttWins { get; set; }
    public int tttTies { get; set; }
    public int tttLosses { get; set; }
    public int tttWinsRow { get; set; }
    public double tttWinPercentDecimal { get; set; }

}