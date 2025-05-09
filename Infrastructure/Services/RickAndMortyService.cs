using System.Text.Json;
using RickAndMorty.Core.Interfaces;


public class RickAndMortyService(HttpClient httpClient) : IRickAndMortyService
{
  private readonly HttpClient _httpClient = httpClient;

  public async Task<ApiResponse<Character>> GetCharactersAsync(int page = 1)
  {
    var httpResponse = await _httpClient.GetAsync($"character?page={page}");

    httpResponse.EnsureSuccessStatusCode();

    var json = await httpResponse.Content.ReadAsStringAsync();

    var responseJson = JsonSerializer.Deserialize<ApiResponse<Character>>(json);

    return responseJson ?? throw new Exception("No se pudo obtener la respuesta de la API");
  }
}