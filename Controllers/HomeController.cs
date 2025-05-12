using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RickAndMorty.Core.Interfaces;
using RickAndMorty.Models;

namespace RickAndMorty.Controllers;

public class HomeController(
  ILogger<HomeController> logger,
  IRickAndMortyService rickAndMortyService
  ) : Controller
{
  private readonly ILogger<HomeController> _logger = logger;

  private readonly IRickAndMortyService _rickAndMortyService = rickAndMortyService;

  public async Task<IActionResult> Index(int page = 1)
  {
    ViewData["CurrentPage"] = page;

    var charactersApiResponse = await _rickAndMortyService.GetCharactersAsync(page);

    var characterIds = await _rickAndMortyService.GetFavoriteCharactersAsync();

    var namesList = characterIds.Select(item => item.Name).ToList();

    var response = new CharacterListResponse(response: charactersApiResponse, favoriteCharacters: namesList);

    return View(response);
  }

  [HttpPost]
  public async Task<IActionResult> SaveFavoriteCharacter(string characterId)
  {
    try
    {
      bool result = await _rickAndMortyService.SaveFavoriteCharacterAsync(characterId);

      if (!result)
      {
        return BadRequest();
      }

      return Ok();
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
