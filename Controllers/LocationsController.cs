using Microsoft.AspNetCore.Mvc;

namespace RickAndMorty.Controllers;

public class LocationsController : Controller
{
  public IActionResult Index()
  {
    return View();
  }
}