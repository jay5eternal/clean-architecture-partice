using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Persistence.Entities.Shelfs;

/// <summary>
/// A list of rows contained within the cabinet.
/// </summary>
public class Row
{
    /// <summary>
    /// A unique identifier for the row within the cabinet.
    /// </summary>
    [BsonElement("number")]
    public int Number { get; set; }

    /// <summary>
    /// A list of lanes contained within the row.
    /// </summary>
    [BsonElement("lanes")]
    public List<Lane> Lanes { get; set; }

    /// <summary>
    /// The Z-coordinate of the row within the cabinet.
    /// </summary>
    [BsonElement("positionZ")]
    public int PositionZ { get; set; }

    /// <summary>
    /// The height of the row.
    /// </summary>
    [BsonElement("size")]
    public RowSize Size { get; set; }
}