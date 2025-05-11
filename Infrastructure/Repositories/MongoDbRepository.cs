using Microsoft.Extensions.Options;
using MongoDB.Driver;

public class MongoDbRepository<T> where T : class
{
  private readonly IMongoCollection<T> _collection;

  public MongoDbRepository(IOptions<MongoDbSettings> settings, IMongoClient mongoClient)
  {
    var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
    _collection = database.GetCollection<T>(settings.Value.CollectionName);
  }

  public async Task InsertAsync(T entity) => await _collection.InsertOneAsync(entity);
  public async Task<List<T>> GetAllAsync() => await _collection.Find(_ => true).ToListAsync();
  public async Task<bool> DeleteOneAsync(T entity)
  {
    ArgumentNullException.ThrowIfNull(entity);

    var idValue = GetIdPropertyValue(entity);

    var filter = Builders<T>.Filter.Eq("Id", idValue);

    try
    {
      var result = await _collection.DeleteOneAsync(filter);
      return result.DeletedCount > 0;
    }
    catch (MongoException ex)
    {
      throw new Exception("Error to deleting entity from MongoDB", ex);
    }
  }

  public async Task<T?> GetByIdAsync(T entity)
  {
    ArgumentNullException.ThrowIfNull(entity);

    var idValue = GetIdPropertyValue(entity);

    var filter = Builders<T>.Filter.Eq("Name", idValue);

    var result = await _collection.Find(filter).FirstOrDefaultAsync();

    return result;
  }


  private static object? GetIdPropertyValue(T entity)
  {
    var idProperty = typeof(T).GetProperty("Id") ??
                     throw new InvalidOperationException("The entity must have an Id property");

    return idProperty.GetValue(entity);
  }

}