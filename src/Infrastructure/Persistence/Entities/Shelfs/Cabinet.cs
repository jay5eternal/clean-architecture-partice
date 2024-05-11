using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Persistence.Entities.Shelfs;

/// <summary>
/// store has several Cabinets
/// </summary>
public class Cabinet
{
    /// <summary>
    /// A unique identifier for the cabinet within the store.
    /// </summary>
    [BsonElement("number")]
    public int Number { get; set; }

    /// <summary>
    /// A list of rows contained within the cabinet.
    /// </summary>
    [BsonElement("rows")]
    public List<Row> Rows { get; set; }

    /// <summary>
    /// The origin position of the cabinet based on the robot's coordinate system.
    /// </summary>
    [BsonElement("position")]
    public Position Position { get; set; }

    /// <summary>
    /// The size of the cabinet.
    /// </summary>
    [BsonElement("size")]
    public Size Size { get; set; }
}