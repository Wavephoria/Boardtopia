namespace Boredtopia.Views.Home;

public class ProfileVM
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int WordlePlays { get; set; }
    public int WordleBest { get; set; }
    public double WordleAverage { get; set; }
    public double WinPercentRockPaperInDecimals { get; set; }
    public string RockPaperMostUsedHand { get; set; }
}