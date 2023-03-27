﻿using Boredtopia.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Boredtopia.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
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
        [HttpGet("/Profile")]
        public IActionResult Profile()
        {
            return View();
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