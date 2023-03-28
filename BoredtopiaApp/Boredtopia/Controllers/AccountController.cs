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
        return View();
    }
    [HttpPost("/Login")]
    public async Task<IActionResult> Login(LoginVM viewModel)
    {
        if (!ModelState.IsValid)
            return View();
        // if (User.Identity.IsAuthenticated)
        //     return RedirectToAction(nameof(Profile));
        // Check if credentials is valid (and set auth cookie)
        var errorMessage = await _accountServices.TryLogin(viewModel);
        if (errorMessage != null)
        {
            // Show error
            ModelState.AddModelError(string.Empty, errorMessage);
            return View();
        }

        return RedirectToAction(nameof(Profile));
    }

    [HttpGet("/Change")]
    public async Task<IActionResult> Change()
    {
        ChangeVM viewModel = new();
        viewModel.Email = await _accountServices.FetchData("email");
        return View(viewModel);
    }

    [HttpPost("/Change")]
    public async Task<IActionResult> Change(ProfileVM viewModel)
    {
        if (!ModelState.IsValid)
            return View();

        var errorMessage = await _accountServices.ChangeData(viewModel);
        if (errorMessage == null)
        {
            // Show error
            ModelState.AddModelError(string.Empty, errorMessage);
            return View();
        }

        // Redirect user
        return RedirectToAction(nameof(Profile));
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
        return View();
    }

    [HttpPost("/Register")]
    public async Task<IActionResult> Register(RegisterVM viewModel)
    {
        if (!ModelState.IsValid)
            return View();

        // Try to register user
        var errorMessage = await _accountServices.TryRegister(viewModel);
        if (errorMessage != null)
        {
            // Show error
            ModelState.AddModelError(string.Empty, errorMessage);
            return View();
        }

        // Redirect user
        return RedirectToAction(nameof(Profile));
    }

    [Authorize]
    [HttpGet("/Profile")]
    public async Task<IActionResult> Profile()
    {
        ProfileVM viewModel = new();
        viewModel.Name = User.Identity.Name;
        viewModel.Email = await _accountServices.FetchData("email");
        return View(viewModel);
    }
}