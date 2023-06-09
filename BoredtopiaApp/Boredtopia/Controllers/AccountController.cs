using Boredtopia.Views.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Boredtopia.Controllers;

public class AccountController : Controller
{
    private readonly AccountServices _accountServices;
    public AccountController(AccountServices accountServices)
    {
        _accountServices = accountServices;
    }
    [HttpGet("/Login")]
    public IActionResult Login()
    {
        if (User.Identity.IsAuthenticated)
            return RedirectToAction(nameof(Profile));
        return View();
    }
    [HttpPost("/Login")]
    public async Task<IActionResult> Login(LoginVM viewModel)
    {
        if (!ModelState.IsValid)
            return View();
        
        var errorMessage = await _accountServices.TryLogin(viewModel);
        if (errorMessage != null)
        {
            ModelState.AddModelError(string.Empty, errorMessage);
            return View();
        }
        return RedirectToAction(nameof(Profile));
    }
    
    [HttpGet("/Change")]
    public async Task<IActionResult> Change()
    {
        ChangeVM viewModel = new();
        viewModel.Name = User.Identity.Name;
        viewModel.Email = await _accountServices.FetchData("email");
        return View(viewModel);
    }

    [HttpPost("/Change")]
    public async Task<IActionResult> Change(ChangeVM viewModel)
    {
        if (!ModelState.IsValid)
            return View();

        var errorMessage = await _accountServices.ChangeData(viewModel);
        if (errorMessage != null)
        {
            ModelState.AddModelError(string.Empty, errorMessage);
            return View();
        }
        return RedirectToAction(nameof(Logout));
    }

    [HttpGet("/Logout")]
    public IActionResult Logout()
    {
        _accountServices.TryLogoutAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet("/Register")]
    public IActionResult Register()
    {
        if (User.Identity.IsAuthenticated)
            return RedirectToAction(nameof(Profile));
        return View();
    }

    [HttpPost("/Register")]
    public async Task<IActionResult> Register(RegisterVM viewModel)
    {
        if (!ModelState.IsValid)
            return View();
        
        var errorMessage = await _accountServices.TryRegister(viewModel);
        if (errorMessage != null)
        {
            ModelState.AddModelError(string.Empty, errorMessage);
            return View();
        }
        
        return RedirectToAction(nameof(Profile));
    }

    [Authorize]
    [HttpGet("/Profile")]
    public async Task<IActionResult> Profile()
    {
        ProfileVM viewModel = new();
        viewModel.Name = User.Identity.Name;
        viewModel.Email = await _accountServices.FetchData("email");
        var wordleStats = _accountServices.FetchWordleStats();
        var rpsStats = _accountServices.FetchRpsStats();
        var tttStats = _accountServices.FetchTicTacStats();
        viewModel.RockWins = rpsStats.Item1;
        viewModel.PaperWins = rpsStats.Item2;
        viewModel.ScissorWins = rpsStats.Item3;
        viewModel.Highscore = rpsStats.Item4;
        viewModel.TotalGames = rpsStats.Item5;
        viewModel.WinPercentRpsDecimal = rpsStats.Item6;
        viewModel.WordlePlays = wordleStats.Item1;
        viewModel.WordleBest = wordleStats.Item2;
        viewModel.WordleAverage = wordleStats.Item3;
        viewModel.tttGames = tttStats.Item1;
        viewModel.tttWins = tttStats.Item2;
        viewModel.tttTies = tttStats.Item3;
        viewModel.tttLosses = tttStats.Item4;
        viewModel.tttWinsRow = tttStats.Item5;
        viewModel.tttWinPercentDecimal = tttStats.Item6;
        return View(viewModel);
    }

    [HttpPost("/Wordle")]
    public async Task<IActionResult> Wordle([FromBody] int guessedCorrectAtNumber)
    {
        string result = "Done";
        if (User.Identity.IsAuthenticated)
            result = await _accountServices.UpdateWordle(guessedCorrectAtNumber);
        return Ok(result);
    }

    [HttpGet("/RockPaperScissors")]
    public IActionResult RockPaperScissors()
    {
        return View();
    }

    [HttpPost("/RockPaperScissors")]
    public async Task<IActionResult> RockPaperScissors([FromBody] int[] data)
    {
        string result = "Done";
        if (User.Identity.IsAuthenticated)
            result = await _accountServices.UpdateRPS(data);
        return Ok(result);
    }
    
    [HttpPost("/TicTacToe")]
    public async Task<IActionResult> TicTacToe([FromBody] string result)
    {
        string completion = "Done";
        if (User.Identity.IsAuthenticated)
            completion = await _accountServices.UpdateTicTac(result);
        return Ok(completion);
    }
}