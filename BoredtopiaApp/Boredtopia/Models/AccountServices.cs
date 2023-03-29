using Boredtopia.Models;
using Boredtopia.Views.Home;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

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
        return "Failed to create user";
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
        string userId = userManager.GetUserId(accessor.HttpContext.User);
        ApplicationUser user = await userManager.FindByIdAsync(userId);
        return user;
    }
    public async Task<string?> FetchData(string choice)
    {
        ApplicationUser user = await GetUser();
        if (choice.ToLower() == "email")
            return await userManager.GetEmailAsync(user);
        else if (choice.ToLower() == "userid")
            return await userManager.GetUserIdAsync(user);
        return null;
    }
    public async Task<string> ChangeData(ChangeVM viewModel)
    {
        ApplicationUser user = await GetUser();
        user.Email = viewModel.Email;
        user.UserName = viewModel.Name;
        IdentityResult result = await userManager.ChangePasswordAsync(user, viewModel.OldPassword, viewModel.NewPassword);
        
        if (result.Succeeded)
        {
            await userManager.UpdateAsync(user);
            return null;
        }
        return "Failed to change details";
    }

    public async void UpdateWordle(string numberOfGuesses, string userId)
    {

        // if (userId == null)
        // {
        //     
        // }
        var wordleData = context._wordleStats.FirstOrDefault(a => a.UserId == userId);
        int guesses = int.Parse(numberOfGuesses);
        
        if (wordleData.WordleBest > guesses)
            wordleData.WordleBest = guesses;
        wordleData.WordleTotal += guesses;
        wordleData.WordlePlays += 1;
        wordleData.WordleAverage = wordleData.WordleTotal / wordleData.WordlePlays;
        if (wordleData.UserId == null)
            wordleData.UserId = userId;
        context._wordleStats.UpdateRange(wordleData);
        var result = await context.SaveChangesAsync();
    }
}