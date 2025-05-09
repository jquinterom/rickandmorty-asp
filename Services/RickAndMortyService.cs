public class RickAndMortyService
{
  private readonly HttpClient _httpClient;

  public RickAndMortyService(HttpClient httpClient)
  {
    _httpClient = httpClient;
  }

  public async Task<List<Character>> GetCharactersAsync()
  {
    var response = await _httpClient.GetFromJsonAsync<ApiResponse>("character");
    return response?.Results ?? new List<Character>();
  }
}

// Modelos para la API
public class ApiResponse
{
  public List<Character> Results { get; set; }
}

public class Character
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Status { get; set; }
  public string Species { get; set; }
  public string Image { get; set; }
}