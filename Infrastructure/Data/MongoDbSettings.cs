public class MongoDbSettings
{
  public required string ConnectionString { get; set; }
  public required string DatabaseName { get; set; }

  public string CollectionName { get; set; } = string.Empty;
}