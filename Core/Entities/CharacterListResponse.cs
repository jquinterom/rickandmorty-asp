public class CharacterListResponse(ApiResponse<Character> response, List<string?>? favoriteCharacters)
{
  public ApiResponse<Character> Response { get; set; } = response;
  public List<string?>? FavoriteCharacters { get; set; } = favoriteCharacters;

}