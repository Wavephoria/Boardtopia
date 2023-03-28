using Boredtopia.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Boredtopia.Views.Home;
using Microsoft.AspNetCore.Authorization;

namespace Boredtopia.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AccountServices _accountServices;

        public HomeController(ILogger<HomeController> logger, AccountServices accountServices)
        {
            _accountServices = accountServices;
            _logger = logger;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("/Games")]
        public IActionResult Games()
        {
            return View();
        }
        [HttpGet("/About")]
        public IActionResult About()
        {
            return View();
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
            if (User.Identity.IsAuthenticated)
                return RedirectToAction(nameof(Profile));
            // Check if credentials is valid (and set auth cookie)
            var errorMessage = _accountServices.TryLogin(viewModel);
            if (errorMessage != null)
            {
                // Show error
                ModelState.AddModelError(string.Empty, await errorMessage);
                return View();
            }
            return RedirectToAction(nameof(Profile));
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
            var errorMessage = _accountServices.TryRegister(viewModel);
            if (errorMessage != null)
            {
                // Show error
                ModelState.AddModelError(string.Empty, await errorMessage);
                return View();
            }

            // Redirect user
            return RedirectToAction(nameof(Login));
        }
        [Authorize]
        [HttpGet("/Profile")]
        public IActionResult Profile()
        {
            ProfileVM viewModel = new();
            viewModel.Name = User.Identity.Name;
            return View(viewModel);
        }
        [HttpGet("/Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("/Wordle")]
        public IActionResult Wordle() 
        { 
            return View(); 
        }
    }
}