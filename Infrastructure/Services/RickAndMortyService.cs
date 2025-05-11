using System.Text.Json;
using RickAndMorty.Core.Interfaces;


public class RickAndMortyService(HttpClient httpClient, MongoDbRepository<FavoriteCharacter> mongoRepo, ILogger<RickAndMortyService> logger) : IRickAndMortyService
{
  private readonly HttpClient _httpClient = httpClient;
  private readonly MongoDbRepository<FavoriteCharacter> _mongoRepo = mongoRepo;

  private readonly ILogger<RickAndMortyService> _logger = logger;

  public async Task<ApiResponse<Character>> GetCharactersAsync(int page = 1)
  {
    try
    {
      var httpResponse = await _httpClient.GetAsync($"character?page={page}");

      httpResponse.EnsureSuccessStatusCode();

      var json = await httpResponse.Content.ReadAsStringAsync();

      var responseJson = JsonSerializer.Deserialize<ApiResponse<Character>>(json);

      return responseJson ?? throw new Exception("Error getting response from API");
    }
    catch (Exception ex)
    {
      throw new Exception("Error getting characters from API", ex);
    }
  }

  public async Task<bool> SaveFavoriteCharacterAsync(string characterId)
  {
    try
    {
      FavoriteCharacter? character = await GetFavoriteCharacterByIdAsync(characterId);

      if (character != null)
      {
        bool result = await DeleteFavoriteCharacterAsync(character);
        if (!result)
        {
          throw new Exception("Error deleting character from favorites");
        }

        return true;
      }

      await _mongoRepo.InsertAsync(new FavoriteCharacter { Name = characterId });
      return true;
    }
    catch (Exception ex)
    {
      throw new Exception("Error saving character to favorites", ex);
    }
  }

  public async Task<List<FavoriteCharacter>> GetFavoriteCharactersAsync()
  {
    var characters = await _mongoRepo.GetAllAsync();
    return characters;
  }

  public async Task<bool> DeleteFavoriteCharacterAsync(FavoriteCharacter character)
  {
    try
    {
      return await _mongoRepo.DeleteOneAsync(character);
    }
    catch (Exception ex)
    {
      throw new Exception("Error deleting character from favorites", ex);
    }
  }

  public async Task<FavoriteCharacter?> GetFavoriteCharacterByIdAsync(string characterId)
  {
    return await _mongoRepo.GetByIdAsync(new FavoriteCharacter { Id = characterId });
  }
}