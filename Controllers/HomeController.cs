using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RickAndMorty.Core.Interfaces;
using RickAndMorty.Models;

namespace RickAndMorty.Controllers;

public class HomeController(ILogger<HomeController> logger, IRickAndMortyService rickAndMortyService) : Controller
{
  private readonly ILogger<HomeController> _logger = logger;

  private readonly IRickAndMortyService _rickAndMortyService = rickAndMortyService;

  public async Task<IActionResult> Index(int page = 1)
  {
    var response = await _rickAndMortyService.GetCharactersAsync(page);
    ViewData["CurrentPage"] = page;
    return View(response);
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
