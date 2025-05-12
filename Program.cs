using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RickAndMorty.Core.Interfaces;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<IRickAndMortyService, RickAndMortyService>(client =>
{
  client.BaseAddress = new Uri("https://rickandmortyapi.com/api/");
});

Env.Load();

var mongoConnectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING") ?? "";
var mongoDatabaseName = Environment.GetEnvironmentVariable("MONGO_DATABASE_NAME") ?? "";
var mongoCollectionName = Environment.GetEnvironmentVariable("MONGO_COLLECTION_NAME") ?? "";

if (!string.IsNullOrEmpty(mongoConnectionString) &&
    !string.IsNullOrEmpty(mongoDatabaseName) &&
    !string.IsNullOrEmpty(mongoCollectionName))
{
  builder.Services.Configure<MongoDbSettings>(options =>
  {
    options.ConnectionString = mongoConnectionString;
    options.DatabaseName = mongoDatabaseName;
    options.CollectionName = mongoCollectionName;
  });

  // Inject the MongoDB client
  builder.Services.AddSingleton<IMongoClient>(sp =>
  {
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
  });

  builder.Services.AddScoped<MongoDbRepository<FavoriteCharacter>>();
}
else
{
  builder.Services.AddScoped(typeof(MongoDbRepository<>), typeof(NullMongoDbRepository<>));
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
