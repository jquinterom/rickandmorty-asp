using System.Text.Json.Serialization;

public class ApiResponse<T>
{
  [JsonPropertyName("results")]
  public required List<T> Results { get; set; }

  [JsonPropertyName("info")]
  public required PageInfo Info { get; set; }
}
