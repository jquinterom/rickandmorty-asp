using System.Text.Json.Serialization;

public class ApiResponse<T>
{
  [JsonPropertyName("results")]
  public List<T> Results { get; set; }

  [JsonPropertyName("info")]
  public PageInfo Info { get; set; }
}
