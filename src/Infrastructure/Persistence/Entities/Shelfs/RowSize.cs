using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Persistence.Entities.Shelfs;

/// <summary>
/// The size of the row.
/// </summary>
public class RowSize
{
    /// <summary>
    /// The height of the row.
    /// </summary>
    [BsonElement("height")]
    public int Height { get; set; }
}