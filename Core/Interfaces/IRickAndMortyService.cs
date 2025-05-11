namespace RickAndMorty.Core.Interfaces;

public interface IRickAndMortyService
{
  Task<ApiResponse<Character>> GetCharactersAsync(int page = 1);

  Task<bool> SaveFavoriteCharacterAsync(string characterId);

  Task<List<FavoriteCharacter>> GetFavoriteCharactersAsync();

  Task<bool> DeleteFavoriteCharacterAsync(FavoriteCharacter character);

  Task<FavoriteCharacter?> GetFavoriteCharacterByIdAsync(string characterId);
}