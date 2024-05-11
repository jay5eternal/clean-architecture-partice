using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Persistence.Entities.Shelfs;

/// <summary>
/// The size of the cabinet.
/// </summary>
public class Size
{
    /// <summary>
    /// The width of the cabinet.
    /// </summary>
    [BsonElement("width")]
    public int Width { get; set; }

    /// <summary>
    /// The depth of the cabinet.
    /// </summary>
    [BsonElement("depth")]
    public int Depth { get; set; }

    /// <summary>
    /// The overall height of the cabinet.
    /// </summary>
    [BsonElement("height")]
    public int Height { get; set; }
}