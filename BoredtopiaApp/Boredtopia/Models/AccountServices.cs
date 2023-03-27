using Boredtopia.Views.Home;
using Microsoft.AspNetCore.Identity;

namespace Boredtopia.Controllers;

public class AccountServices
{
    private UserManager<ApplicationUser> userManager;
    private SignInManager<ApplicationUser> signInManager;
    private RoleManager<IdentityRole> roleManager;

    public AccountServices(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.roleManager = roleManager;
    }
    
    public async Task<string> TryRegister(RegisterVM viewModel)
    {
        var user = new ApplicationUser()
        {
            UserName = viewModel.Username,
        };
        IdentityResult result = await userManager.CreateAsync(user, viewModel.Password);
        bool wasUserCreated = result.Succeeded;
        
        if (wasUserCreated)
            return "User is created";
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
        bool wasUserSignedIn = result.Succeeded;
        if (wasUserSignedIn)
            return "Login was successful";
        return "Login failed";
    }
}