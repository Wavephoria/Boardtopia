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

    public async Task<string> UpdateWordle(int guesses)
    {
        string? userId = FetchUserId();
        var user = CheckForUser(userId);
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
            var wordleData = context._wordleStats.FirstOrDefault(a => a.UserId == userId);
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
        var user = CheckForUser(userId);
        if (user == null)
        {
            return new Tuple<int, int, double>(0, 0, 0);
        }
        return new Tuple<int, int, double>(user.WordlePlays, user.WordleBest, user.WordleAverage);
    }

    public Wordle? CheckForUser(string userId)
    {
        return context._wordleStats.SingleOrDefault(a => a.UserId == userId);
    }

    //public async Task<string> UpdateRPS(int[] data)
    //{
    //    int rockWins = data[0];
    //    int paperWins = data[1];
    //    int scissorWins = data[2];
    //    int highScore = data[3];
    //    string? userId = FetchUserId();
    //    var user = CheckForUserRPS(userId);
    //    if (user == null)
    //    {
    //        RockPaperScissor rps = new();
    //        rps.UserId = userId;
    //        rps.RockWins = rockWins;
    //        rps.PaperWins = paperWins;
    //        rps.ScissorWins = scissorWins;
    //        rps.Highscore = highScore;
    //        rps.TotalGames = t

    //        context._rpsStats.Update(rps);
    //    }
    //    else if (user != null)
    //    {
    //        var rpscheck = user;
    //        if (rpscheck.Highscore > data[3])
    //            wordleData.WordleBest = guesses;
    //        wordleData.WordleTotal += guesses;
    //        wordleData.WordlePlays += 1;
    //        wordleData.WordleAverage = Math.Round((double)wordleData.WordleTotal / wordleData.WordlePlays, 3);
    //        context._wordleStats.Update(wordleData);
    //    }

    //    await context.SaveChangesAsync();
    //    return "Done";
    //}

    //public RockPaperScissor? CheckForUserRPS(string userId)
    //{
    //    return context._rpsStats.SingleOrDefault(a => a.UserId == userId);
    //}
}