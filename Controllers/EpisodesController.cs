using Microsoft.AspNetCore.Mvc;

namespace RickAndMorty.Controllers;

public class EpisodesController : Controller
{
  public IActionResult Index()
  {
    return View();
  }
}