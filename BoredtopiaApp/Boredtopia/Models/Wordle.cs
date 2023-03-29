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
    public string UserId { get; set; }

    public Wordle()
    {
        GetUserId();
    }

    private async void GetUserId()
    {
        UserId = await _accountServices.FetchData("userId");
    }
}
