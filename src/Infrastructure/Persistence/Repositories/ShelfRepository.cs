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
    /// Get Shelf Document 
    /// </summary>
    /// <returns>Shelf Document</returns>
    public async Task<Shelf> GetAsync()
    {
        var shelfCursor = await _shelves.FindAsync(Builders<Infrastructure.Persistence.Entities.Shelfs.Shelf>.Filter.Empty);
        var infraShelf = await shelfCursor.FirstAsync();
        var domainShelf = InfrastructureShelfMappingToDomain(infraShelf);
        return domainShelf;
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
    
    /// <summary>
    /// Map infrastructure Shelf Class To Domain class
    /// </summary>
    /// <param name="infraShelf">infrastructure Shelf</param>
    /// <returns>Domain Shelf</returns>
    private Shelf InfrastructureShelfMappingToDomain(Infrastructure.Persistence.Entities.Shelfs.Shelf infraShelf)
    {
        if (infraShelf is null)
        {
            return null;
        }
        
        var domainShelf = new Shelf
        {
            Cabinets = new List<Cabinet>()
        };

        foreach (var infraCabinet in infraShelf.Cabinets)
        {
            var domainCabinet = new Cabinet
            {
                Number = infraCabinet.Number,
                Position = new Position
                {
                    X = infraCabinet.Position.X,
                    Y = infraCabinet.Position.Y,
                    Z = infraCabinet.Position.Z
                },
                Size = new Size
                {
                    Width = infraCabinet.Size.Width,
                    Depth = infraCabinet.Size.Depth,
                    Height = infraCabinet.Size.Height
                },
                Rows = new List<Row>()
            };

            foreach (var infraRow in infraCabinet.Rows)
            {
                var domainRow = new Row
                {
                    Number = infraRow.Number,
                    PositionZ = infraRow.PositionZ,
                    Size = new RowSize
                    {
                        Height = infraRow.Size.Height
                    },
                    Lanes = new List<Lane>()
                };

                foreach (var infraLane in infraRow.Lanes)
                {
                    var domainLane = new Lane
                    {
                        Number = infraLane.Number,
                        JanCode = infraLane.JanCode,
                        Quantity = infraLane.Quantity,
                        PositionX = infraLane.PositionX
                    };
                    domainRow.Lanes.Add(domainLane);
                }

                domainCabinet.Rows.Add(domainRow);
            }

            domainShelf.Cabinets.Add(domainCabinet);
        }

        return domainShelf;
    }
}