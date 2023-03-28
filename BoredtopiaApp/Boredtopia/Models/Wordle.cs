using Boredtopia.Controllers;

namespace Boredtopia.Models;

public class Wordle
{
    private AccountServices _accountServices;

    public int Id { get; set; }
    public int WordlePlays { get; set; }
    public double WordleAverage { get; set; }
    public int WordleBest { get; set; }
    public int WordleTotal { get; set; }
    public int UserId { get; set; }

    public Wordle()
    {
        GetUserId();
    }

    private async void GetUserId()
    {
        string? userId = await _accountServices.FetchData("userId");
        UserId = Int32.Parse(userId);
    }
}
