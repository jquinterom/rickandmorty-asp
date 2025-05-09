namespace RickAndMorty.Core.Interfaces;

public interface IRickAndMortyService
{
  Task<ApiResponse<Character>> GetCharactersAsync(int page = 1);
}