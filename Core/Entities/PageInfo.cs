
using System.Text.Json.Serialization;

public class PageInfo
{
  [JsonPropertyName("count")]
  public int Count { get; set; }

  [JsonPropertyName("pages")]
  public int Pages { get; set; }

  [JsonPropertyName("next")]
  public string Next { get; set; }

  [JsonPropertyName("previous")]
  public string Previous { get; set; }
}