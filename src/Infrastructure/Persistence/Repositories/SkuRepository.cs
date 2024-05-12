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
    /// Get Sku Document
    /// </summary>
    /// <param name="janCode">A unique identifier for the SKU</param>
    /// <returns>Sku Document</returns>
    public async Task<Sku> GetAsync(string janCode)
    {
        var janCodeSkuFilter = Builders<Infrastructure.Persistence.Entities.Skus.Sku>.Filter.Eq(x => x.JanCode, janCode);
        var skuCursor = await _skus.FindAsync(janCodeSkuFilter);
        var infraShelf = await skuCursor.FirstOrDefaultAsync();

        if (infraShelf is null)
        {
            return null;
        }

        var domainShelf = InfraSkuMappingToDomain(new List<Infrastructure.Persistence.Entities.Skus.Sku>
            { infraShelf }).FirstOrDefault();
        
        return domainShelf;
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

    /// <summary>
    /// Map Infrastructure skus Class To Domain class
    /// </summary>
    /// <param name="infraSkus">Infrastructure skus</param>
    /// <returns>Domain skus</returns>
    private List<Sku> InfraSkuMappingToDomain(List<Infrastructure.Persistence.Entities.Skus.Sku> infraSkus)
    {
        if (infraSkus is null || infraSkus.Any() == false)
        {
            return null;
        }

        var domainSkus = new List<Sku>();
        foreach (var infraSku in infraSkus)
        {
            if (infraSku is null)
            {
                continue;
            }

            var domainSku = new Sku
            {
                JanCode = infraSku.JanCode,
                Name = infraSku.Name,
                X = infraSku.X,
                Y = infraSku.Y,
                Z = infraSku.Z,
                ImageURL = infraSku.ImageURL,
                Size = infraSku.Size,
                TimeStamp = infraSku.TimeStamp,
                Shape = infraSku.Shape
            };
            domainSkus.Add(domainSku);
        }

        if (domainSkus.Any() == false)
        {
            return null;
        }

        return domainSkus;
    }
}