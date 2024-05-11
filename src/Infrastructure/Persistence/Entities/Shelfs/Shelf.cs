using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Persistence.Entities.Shelfs;

/// <summary>
/// Represents a physical shelf in the store and contains multiple SKUs. Store has several Cabinets.
/// </summary>
public class Shelf
{
    /// <summary>
    /// MongoDb ObjectId
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    /// <summary>
    /// store has several Cabinets
    /// </summary>
    [BsonElement("cabinets")]
    public List<Cabinet> Cabinets { get; set; }
}