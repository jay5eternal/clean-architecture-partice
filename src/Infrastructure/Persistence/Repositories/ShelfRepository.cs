using Application.Interfaces;
using Contracts.Shelf;
using Domain.Entities.Shelfs;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

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
    /// <remarks>Standalone servers do not support transactions.</remarks>
    public async Task AddAsync(Shelf shelf)
    {
        var shelfToAdd = DomainShelfMappingToInfra(shelf);
        var session = await _mongoDbContext.GetSessionAsync();
        //session.StartTransaction();
        await _shelves.InsertOneAsync(session, shelfToAdd);
        //await session.CommitTransactionAsync();
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
    /// Get Row Document List
    /// </summary>
    /// <param name="cabinetNumber">A unique identifier for the Cabinet.</param>
    /// <param name="rowNumber">A unique identifier for the Row.</param>
    /// <returns>Row Document List</returns>
    public async Task<List<Row>> GetRowListAsync(int cabinetNumber, int? rowNumber)
    {
        var shelfQuery = _shelves.AsQueryable();
        var shelf = await shelfQuery.FirstOrDefaultAsync();

        if (shelf is null)
        {
            return null;
        }

        var infraRows = new List<Entities.Shelfs.Row>();
        if (rowNumber.HasValue)
        {
            infraRows = shelf.Cabinets
                .Where(x => x.Number == cabinetNumber)
                .SelectMany(x => x.Rows)
                .Where(x => x.Number == rowNumber.Value)
                .ToList();
        }
        else
        {
            infraRows = shelf.Cabinets
                .Where(x => x.Number == cabinetNumber)
                .SelectMany(x => x.Rows)
                .ToList();
        }

        var domainLane = InfrastructureRowsMappingToDomain(infraRows);

        return domainLane;
    }

    /// <summary>
    /// AddSkuToShelf 
    /// </summary>
    /// <param name="request">AddSkuToShelf Request Entity</param>
    /// <param name="session">MongoDB session</param>
    /// <remarks>Standalone servers do not support transactions.</remarks>
    public async Task AddSkuAsync(AddSkuToShelfRequest request, IClientSessionHandle session = null)
    {
        if (session is null)
        {
            session = await _mongoDbContext.GetSessionAsync();
        }

        var shelfQuery = _shelves.AsQueryable(session);
        var infraShelf = shelfQuery.FirstOrDefault();
        var row = infraShelf.Cabinets
            .Where(c => c.Number == request.CabinetNumber)
            .SelectMany(c => c.Rows)
            .First(r => r.Number == request.RowNumber);
        if (request.IsLaneExist)
        {
            var lane = row.Lanes.Single(x => x.Number == request.LaneNumber);
            lane.Quantity = lane.Quantity + request.Quantity;
            lane.PositionX = lane.PositionX + request.PositionX;
        }
        else
        {
            var maxLaneNumber = row.Lanes.Max(x => x.Number);
            row.Lanes.Add(new Entities.Shelfs.Lane
            {
                Number = maxLaneNumber + 1,
                JanCode = request.JanCode,
                Quantity = request.Quantity,
                PositionX = request.PositionX
            });
        }

        var updateFilter =
            Builders<Infrastructure.Persistence.Entities.Shelfs.Shelf>.Filter.Eq(x => x.Id, infraShelf.Id);

        var updateDef =
            Builders<Infrastructure.Persistence.Entities.Shelfs.Shelf>.Update.Set(x => x.Cabinets, infraShelf.Cabinets);
        await _shelves.UpdateOneAsync(session, updateFilter, updateDef);
    }

    /// <summary>
    /// RemoveSku 
    /// </summary>
    /// <param name="request">RemoveSku Request Entity</param>
    /// <param name="session">MongoDB session</param>
    /// <remarks>Standalone servers do not support transactions.</remarks>
    public async Task RemoveSkuAsync(RemoveSkuRequest request, IClientSessionHandle session = null)
    {
        if (session is null)
        {
            session = await _mongoDbContext.GetSessionAsync();
        }

        var shelfQuery = _shelves.AsQueryable(session);
        var infraShelf = shelfQuery.FirstOrDefault();
        var row = infraShelf.Cabinets
            .Where(c => c.Number == request.CabinetNumber)
            .SelectMany(c => c.Rows)
            .First(r => r.Number == request.RowNumber);

        row.Lanes = row.Lanes.Where(l => l.Number != request.LaneNumber && l.JanCode != request.JanCode).ToList();

        var updateFilter =
            Builders<Infrastructure.Persistence.Entities.Shelfs.Shelf>.Filter.Eq(x => x.Id, infraShelf.Id);

        var updateDef =
            Builders<Infrastructure.Persistence.Entities.Shelfs.Shelf>.Update.Set(x => x.Cabinets, infraShelf.Cabinets);
        await _shelves.UpdateOneAsync(session, updateFilter, updateDef);
    }

    /// <summary>
    /// MoveSkuInShelf 
    /// </summary>
    /// <param name="request">MoveSkuInShel Request Entity</param>
    /// <remarks>Standalone servers do not support transactions.</remarks>
    public async Task MoveSkuInShelfAsync(MoveSkuInShelfRequest request)
    {
        var session = await _mongoDbContext.GetSessionAsync();
        //// Remove Origin Position
        var originPosition = request.OriginPosition;
        var removeRequest = new RemoveSkuRequest
        {
            CabinetNumber = originPosition.CabinetNumber,
            RowNumber = originPosition.RowNumber,
            LaneNumber = originPosition.LaneNumber,
            JanCode = request.JanCode
        };
        await RemoveSkuAsync(removeRequest, session);
        //// Add New Position
        var newPosition = request.NewPosition;
        var addRequest = new AddSkuToShelfRequest
        {
            CabinetNumber = newPosition.CabinetNumber,
            RowNumber = newPosition.RowNumber,
            LaneNumber = newPosition.LaneNumber,
            JanCode = request.JanCode,
            Quantity = newPosition.Quantity,
            PositionX = newPosition.PositionX,
            IsLaneExist = newPosition.IsLaneExist
        };
        await AddSkuAsync(addRequest, session);
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

    /// <summary>
    /// Map infrastructure Lanes Class To Domain class
    /// </summary>
    /// <param name="infraLanes">infrastructure Lanes</param>
    /// <returns>Domain Lane</returns>
    private List<Lane> InfrastructureLanesMappingToDomain(List<Infrastructure.Persistence.Entities.Shelfs.Lane> infraLanes)
    {
        if (infraLanes is null || infraLanes.Any() == false)
        {
            return null;
        }

        var domainLanes = new List<Lane>();
        foreach (var infraLane in infraLanes)
        {
            if (infraLane is null)
            {
                continue;
            }

            var domainLane = new Lane
            {
                Number = infraLane.Number,
                JanCode = infraLane.JanCode,
                Quantity = infraLane.Quantity,
                PositionX = infraLane.PositionX
            };
            domainLanes.Add(domainLane);
        }

        if (domainLanes.Any() == false)
        {
            return null;
        }

        return domainLanes;
    }

    /// <summary>
    /// Map infrastructure Rows Class To Domain class
    /// </summary>
    /// <param name="infraRows">infrastructure Rows</param>
    /// <returns>Domain Rows</returns>
    private List<Row> InfrastructureRowsMappingToDomain(List<Infrastructure.Persistence.Entities.Shelfs.Row> infraRows)
    {
        if (infraRows is null || infraRows.Any() == false)
        {
            return null;
        }

        var domainRows = new List<Row>();

        foreach (var infraRow in infraRows)
        {
            if (infraRow is null)
            {
                continue;
            }

            var domainRow = new Row
            {
                Number = infraRow.Number,
                Lanes = InfrastructureLanesMappingToDomain(infraRow.Lanes),
                PositionZ = infraRow.PositionZ,
                Size = new RowSize
                {
                    Height = infraRow.Size.Height
                }
            };

            domainRows.Add(domainRow);
        }

        if (domainRows.Any() == false)
        {
            return null;
        }

        return domainRows;
    }
}