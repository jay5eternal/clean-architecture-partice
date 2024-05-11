using Application.Interfaces;
using Domain.Entities.Skus;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository of Sku
/// </summary>
public class SkuRepository : ISkuRepository
{
    /// <summary>
    /// IMongoCollection of Sku
    /// </summary>
    private readonly IMongoCollection<Infrastructure.Persistence.Entities.Skus.Sku> _skus;

    /// <summary>
    /// MongoDBContext
    /// </summary>
    private readonly MongoDBContext _mongoDbContext;

    /// <summary>
    /// SkuRepository constructor
    /// </summary>
    public SkuRepository(MongoDBContext mongoDbContext)
    {
        _mongoDbContext = mongoDbContext;
        _skus = _mongoDbContext.Skus;
    }

    /// <summary>
    /// Add Many skus
    /// </summary>
    /// <param name="skus">Sku List</param>
    public async Task AddManyAsync(List<Sku> skus)
    {
        var skusToAdd = DomainSkuMappingToInfra(skus);
        var session = await _mongoDbContext.GetSessionAsync();

        await _skus.InsertManyAsync(session, skusToAdd);
    }

    /// <summary>
    /// Get Document Count
    /// </summary>
    /// <returns>Document Count</returns>
    public async Task<int> GetCountAsync()
    {
        long count = await _skus.CountDocumentsAsync(Builders<Infrastructure.Persistence.Entities.Skus.Sku>.Filter.Empty);
        return (int)count; 
    }

    /// <summary>
    /// Map Domain skus Class To Infrastructure class
    /// </summary>
    /// <param name="skus">Domain skus</param>
    /// <returns>Infrastructure skus</returns>
    private List<Infrastructure.Persistence.Entities.Skus.Sku> DomainSkuMappingToInfra(List<Sku> skus)
    {
        var infraSkus = new List<Infrastructure.Persistence.Entities.Skus.Sku>();
        foreach (var domainSku in skus)
        {
            var infraSku = new Infrastructure.Persistence.Entities.Skus.Sku
            {
                JanCode = domainSku.JanCode,
                Name = domainSku.Name,
                X = domainSku.X,
                Y = domainSku.Y,
                Z = domainSku.Z,
                ImageURL = domainSku.ImageURL,
                Size = domainSku.Size,
                TimeStamp = domainSku.TimeStamp,
                Shape = domainSku.Shape
            };
            infraSkus.Add(infraSku);
        }
        return infraSkus;
    }
}