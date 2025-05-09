using System.Text.Json.Serialization;

public class Character
{
  [JsonPropertyName("id")]
  public int Id { get; set; }

  [JsonPropertyName("name")]
  public string Name { get; set; }

  [JsonPropertyName("status")]
  public string Status { get; set; }

  [JsonPropertyName("species")]
  public string Species { get; set; }

  [JsonPropertyName("image")]
  public string Image { get; set; }
}