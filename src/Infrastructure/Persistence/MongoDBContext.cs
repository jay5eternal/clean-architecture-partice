using Infrastructure.Persistence.Entities.Shelfs;
using Infrastructure.Persistence.Entities.Skus;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infrastructure.Persistence;

public class MongoDBContext
{
    /// <summary>
    /// IMongoDatabase
    /// </summary>
    private readonly IMongoDatabase _database;

    /// <summary>
    /// IMongoClient
    /// </summary>
    private readonly IMongoClient _client;

    /// <summary>
    /// MongoDBContext constructor
    /// </summary>
    /// <param name="configuration">IConfiguration</param>
    public MongoDBContext(IConfiguration configuration)
    {
        _client = new MongoClient(configuration["MongoDBSettings:ConnectionString"]);
        _database = _client.GetDatabase(configuration["MongoDBSettings:DatabaseName"]);
    }

    /// <summary>
    /// Shelves Collections
    /// </summary>
    public IMongoCollection<Shelf> Shelves => _database.GetCollection<Shelf>("Shelves");

    /// <summary>
    /// Sku Collections
    /// </summary>
    public IMongoCollection<Sku> Skus => _database.GetCollection<Sku>("Skus");

    /// <summary>
    /// Get Session
    /// </summary>
    /// <param name="options">options</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>client session</returns>
    public async Task<IClientSessionHandle> GetSessionAsync(
        ClientSessionOptions options = null,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _client.StartSessionAsync(options, cancellationToken);
    }

}