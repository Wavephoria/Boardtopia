using System.Security.Claims;
using Boredtopia.Models;
using Boredtopia.Views.Home;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Boredtopia.Controllers;

public class AccountServices
{
    private UserManager<ApplicationUser> userManager;
    private SignInManager<ApplicationUser> signInManager;
    private RoleManager<IdentityRole> roleManager;
    private IHttpContextAccessor accessor;
    private ApplicationContext context;

    public AccountServices(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        IHttpContextAccessor accessor,
        ApplicationContext context
    )
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.roleManager = roleManager;
        this.accessor = accessor;
        this.context = context;
    }

    public async Task<string> TryRegister(RegisterVM viewModel)
    {
        var user = new ApplicationUser()
        {
            UserName = viewModel.Username,
            Email = viewModel.Email
        };
        IdentityResult result = await userManager.CreateAsync(user, viewModel.Password);

        if (result.Succeeded)
        {
            await signInManager.SignInAsync(user, isPersistent: false);
            return null;
        }

        return "Password needs to be at least 5 characters including a number and both an uppercase and lowercase letter";
    }

    public async Task<string> TryLogin(LoginVM viewModel)
    {
        SignInResult result = await signInManager.PasswordSignInAsync(
            viewModel.Username,
            viewModel.Password,
            isPersistent: false,
            lockoutOnFailure: false
        );

        if (result.Succeeded)
            return null;
        return "Login failed";
    }

    public async void TryLogoutAsync()
    {
        await signInManager.SignOutAsync();
    }

    private async Task<ApplicationUser?> GetUser()
    {
        // string userId = userManager.GetUserId(accessor.HttpContext.User);
        string userId = FetchUserId();
        ApplicationUser user = await userManager.FindByIdAsync(userId);
        return user;
    }
    private string? FetchUserId()
    {
        return accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
    public async Task<string?> FetchData(string choice)
    {
        ApplicationUser user = await GetUser();
        if (choice.ToLower() == "email")
            return await userManager.GetEmailAsync(user);
        return null;
    }
    
    public async Task<string> ChangeData(ChangeVM viewModel)
    {
        ApplicationUser user = await GetUser();
        user.Email = viewModel.Email;
        user.UserName = viewModel.Name;
        IdentityResult result =
            await userManager.ChangePasswordAsync(user, viewModel.OldPassword, viewModel.NewPassword);

        if (result.Succeeded)
        {
            await userManager.UpdateAsync(user);
            return null;
        }
        return "Failed to change details";
    }
    
    
    
    public Wordle? CheckForUserWordle(string userId)
    {
        return context._wordleStats.SingleOrDefault(a => a.UserId == userId);
    }
    public async Task<string> UpdateWordle(int guesses)
    {
        string? userId = FetchUserId();
        var user = CheckForUserWordle(userId);
        if (user == null)
        {
            Wordle wordle = new();
            wordle.UserId = userId;
            wordle.WordleBest = guesses;
            wordle.WordlePlays = 1;
            wordle.WordleTotal = guesses;
            wordle.WordleAverage = (double)wordle.WordleTotal / wordle.WordlePlays;
            context._wordleStats.Update(wordle);
        }
        else if (user != null)
        {
            var wordleData = user;
            if (wordleData.WordleBest > guesses)
                wordleData.WordleBest = guesses;
            wordleData.WordleTotal += guesses;
            wordleData.WordlePlays += 1;
            wordleData.WordleAverage = Math.Round((double)wordleData.WordleTotal / wordleData.WordlePlays, 3);
            context._wordleStats.Update(wordleData);
        }
        await context.SaveChangesAsync();
        return "Done";
    }
    public Tuple<int, int, double> FetchWordleStats()
    {
        string? userId = FetchUserId();
        var user = CheckForUserWordle(userId);
        if (user == null)
        {
            return new Tuple<int, int, double>(0, 0, 0);
        }
        return new Tuple<int, int, double>(user.WordlePlays, user.WordleBest, user.WordleAverage);
    }



    public RockPaperScissor? CheckForUserRps(string userId)
    {
        return context._rpsStats.SingleOrDefault(a => a.UserId == userId);
    }
    public async Task<string> UpdateRPS(int[] data)
    {
        int rockWins = data[0];
        int paperWins = data[1];
        int scissorWins = data[2];
        int highScore = data[3];
        int totalGames = data[4];
        string? userId = FetchUserId();
        var user = CheckForUserRps(userId);
        if (user == null)
        {
            RockPaperScissor rps = new();
            rps.UserId = userId;
            rps.RockWins = rockWins;
            rps.PaperWins = paperWins;
            rps.ScissorWins = scissorWins;
            rps.Highscore = highScore;
            rps.TotalGames = totalGames;
            rps.WinPercentDecimal = (rockWins + paperWins + scissorWins) / (double)totalGames; 

            context._rpsStats.Update(rps);
        }
        else if (user != null)
        {
            var rps = user;
            if (rps.Highscore < highScore)
                rps.Highscore = highScore;

            rps.RockWins += rockWins;
            rps.PaperWins += paperWins;
            rps.ScissorWins += scissorWins;
            rps.TotalGames += totalGames;
            rps.WinPercentDecimal = (rps.RockWins + rps.PaperWins + rps.ScissorWins) / (double)rps.TotalGames;

            context._rpsStats.Update(rps);
        }
        await context.SaveChangesAsync();
        return "Done";
    }
    public Tuple<int, int, int, int, int, double> FetchRpsStats()
    {
        string? userId = FetchUserId();
        var user = CheckForUserRps(userId);
        if (user == null)
        {
            return new Tuple<int, int,int,int,int, double>(0, 0, 0, 0, 0, 0);
        }
        return new Tuple<int, int, int, int, int, double>(user.RockWins, user.PaperWins, user.ScissorWins, user.Highscore, user.TotalGames, user.WinPercentDecimal);
    }

    
    public async Task<string> UpdateTicTac(string result)
    {
        string? userId = FetchUserId();
        var user = CheckForUserTicTac(userId);
        if (user == null)
        {
            TicTacToe ttt = new();
            ttt.UserId = userId;
            if (result.ToLower() == "win")
                ttt.Wins++;
            else if (result.ToLower() == "tie")
                ttt.Ties++;
            else
                ttt.Losses++;

            ttt.TotalGames++;
            ttt.WinPercentDecimal = ttt.Wins/ (double)ttt.TotalGames;
            if (result.ToLower() == "win")
                ttt.WinStreak = 1;
            else
                ttt.WinStreak = 0;
            
            context._tttStats.Update(ttt);
            
        }
        else if (user != null)
        {
            var ttt = user;
            if (result.ToLower() == "win")
                ttt.Wins++;
            else if (result.ToLower() == "tie")
                ttt.Ties++;
            else
                ttt.Losses++;

            ttt.TotalGames++;
            ttt.WinPercentDecimal = ttt.Wins /(double)ttt.TotalGames;
            if (result.ToLower() == "win")
                ttt.WinStreak++;
            else
                ttt.WinStreak = 0;
            
            context._tttStats.Update(ttt);
        }
        
        await context.SaveChangesAsync();
        return "Done";
    }
    public TicTacToe? CheckForUserTicTac(string userId)
    {
        return context._tttStats.SingleOrDefault(a => a.UserId == userId);
    }
    public Tuple<int, int, int, int, int, double> FetchTicTacStats()
    {
        string? userId = FetchUserId();
        var user = CheckForUserTicTac(userId);
        if (user == null)
        {
            return new Tuple<int, int, int, int, int, double>(0, 0, 0, 0, 0, 0);
        }
        return new Tuple<int, int, int, int, int, double>(user.TotalGames, user.Wins, user.Ties, user.Losses, user.WinStreak, user.WinPercentDecimal);
    }
    
    
}