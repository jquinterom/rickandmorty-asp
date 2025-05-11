using System.Text.Json.Serialization;

public class Character
{
  [JsonPropertyName("id")]
  public int Id { get; set; }

  [JsonPropertyName("name")]
  public required string Name { get; set; }

  [JsonPropertyName("status")]
  public required string Status { get; set; }

  [JsonPropertyName("species")]
  public required string Species { get; set; }

  [JsonPropertyName("image")]
  public required string Image { get; set; }

  public bool IsFavorite { get; set; }
}