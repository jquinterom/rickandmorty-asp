using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RickAndMorty.Models;

namespace RickAndMorty.Controllers;

public class HomeController : Controller
{
  private readonly ILogger<HomeController> _logger;

  private readonly RickAndMortyService _rickAndMortyService;

  public HomeController(ILogger<HomeController> logger, RickAndMortyService rickAndMortyService)
  {
    _logger = logger;
    _rickAndMortyService = rickAndMortyService;
  }

  public async Task<IActionResult> Index()
  {
    var characters = await _rickAndMortyService.GetCharactersAsync();
    return View(characters);
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
