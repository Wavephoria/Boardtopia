using Boredtopia.Controllers;

namespace Boredtopia.Models;

public class Wordle
{
    public int Id { get; set; }
    public int WordlePlays { get; set; }
    public double WordleAverage { get; set; } 
    public int WordleBest { get; set; }
    public int WordleTotal { get; set; }
    public string UserId { get; set; }

    // public Wordle()
    // {
    //     WordleBest = 0;
    //     WordleAverage = 0;
    //     WordleBest = 0;
    //     WordleTotal = 0;
    // }
}
