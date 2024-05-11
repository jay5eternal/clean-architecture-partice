using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Persistence.Entities.Shelfs;

/// <summary>
/// The origin position of the cabinet based on the robot's coordinate system.
/// </summary>
public class Position
{
    /// <summary>
    /// The X-coordinate of the cabinet.
    /// </summary>
    [BsonElement("x")]
    public int X { get; set; }

    /// <summary>
    /// The Y-coordinate of the cabinet.
    /// </summary>
    [BsonElement("y")]
    public int Y { get; set; }

    /// <summary>
    /// The Z-coordinate of the cabinet.
    /// </summary>
    [BsonElement("z")]
    public int Z { get; set; }
}