using Application.Interfaces;
using Domain.Entities.Shelfs;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository of Shelf
/// </summary>
public class ShelfRepository : IShelfRepository
{
    /// <summary>
    /// IMongoCollection of Shelf
    /// </summary>
    private readonly IMongoCollection<Infrastructure.Persistence.Entities.Shelfs.Shelf> _shelves;

    /// <summary>
    /// MongoDBContext
    /// </summary>
    private readonly MongoDBContext _mongoDbContext;

    /// <summary>
    /// ShelfRepository constructor
    /// </summary>
    /// <param name="mongoDbContext"></param>
    public ShelfRepository(MongoDBContext mongoDbContext)
    {
        _mongoDbContext = mongoDbContext;
        _shelves = _mongoDbContext.Shelves;
    }

    /// <summary>
    /// Add Shelf
    /// </summary>
    /// <param name="shelf">Shelf</param>
    public async Task AddAsync(Shelf shelf)
    {
        var shelfToAdd = DomainShelfMappingToInfra(shelf);
        var session = await _mongoDbContext.GetSessionAsync();
        
        await _shelves.InsertOneAsync(session, shelfToAdd);
    }

    /// <summary>
    /// Get Document Count
    /// </summary>
    /// <returns>Document Count</returns>
    public async Task<int> GetCountAsync()
    {
        long count = await _shelves.CountDocumentsAsync(Builders<Infrastructure.Persistence.Entities.Shelfs.Shelf>.Filter.Empty);
        return (int)count; 
    }

    /// <summary>
    /// Map Domain Shelf Class To Infrastructure class
    /// </summary>
    /// <param name="shelf">Domain Shelf</param>
    /// <returns>Infrastructure Shelf</returns>
    private Infrastructure.Persistence.Entities.Shelfs.Shelf DomainShelfMappingToInfra(Shelf shelf)
    {
        var shelfToMapping = new Infrastructure.Persistence.Entities.Shelfs.Shelf
        {
            Cabinets = new List<Infrastructure.Persistence.Entities.Shelfs.Cabinet>()
        };

        foreach (var domainCabinet in shelf.Cabinets)
        {
            var infraCabinet = new Infrastructure.Persistence.Entities.Shelfs.Cabinet
            {
                Number = domainCabinet.Number,
                Position = new Infrastructure.Persistence.Entities.Shelfs.Position
                {
                    X = domainCabinet.Position.X,
                    Y = domainCabinet.Position.Y,
                    Z = domainCabinet.Position.Z
                },
                Size = new Infrastructure.Persistence.Entities.Shelfs.Size
                {
                    Width = domainCabinet.Size.Width,
                    Depth = domainCabinet.Size.Depth,
                    Height = domainCabinet.Size.Height
                },
                Rows = new List<Infrastructure.Persistence.Entities.Shelfs.Row>()
            };

            foreach (var domainRow in domainCabinet.Rows)
            {
                var infraRow = new Infrastructure.Persistence.Entities.Shelfs.Row
                {
                    Number = domainRow.Number,
                    PositionZ = domainRow.PositionZ,
                    Size = new Infrastructure.Persistence.Entities.Shelfs.RowSize
                    {
                        Height = domainRow.Size.Height
                    },
                    Lanes = new List<Infrastructure.Persistence.Entities.Shelfs.Lane>()
                };

                foreach (var domainLane in domainRow.Lanes)
                {
                    var infraLane = new Infrastructure.Persistence.Entities.Shelfs.Lane
                    {
                        Number = domainLane.Number,
                        JanCode = domainLane.JanCode,
                        Quantity = domainLane.Quantity,
                        PositionX = domainLane.PositionX
                    };
                    infraRow.Lanes.Add(infraLane);
                }

                infraCabinet.Rows.Add(infraRow);
            }

            shelfToMapping.Cabinets.Add(infraCabinet);
        }

        return shelfToMapping;
    }
}