using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BlockChainLogging.Models;

namespace BlockChainLogging.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        _logger.LogInformation("Calling Index");

        var temperatures = GenerateTemperatures();

        return View(temperatures);
    }

    private IEnumerable<Temperature> GenerateTemperatures()
    {
        _logger.LogInformation("Generating temperatures");

        string[] cities = ["London", "Paris", "New York", "Los Angeles", "Berlin"];

        var temperatures = cities
            .Select(city => new Temperature()
            {
                City = city,
                Degree = Random.Shared.Next(-45, 46),
            });

        return temperatures;
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}