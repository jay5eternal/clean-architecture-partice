using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Persistence.Entities.Shelfs;

/// <summary>
/// A list of lanes contained within the row.
/// </summary>
public class Lane
{
    /// <summary>
    /// A unique identifier for the lane within the row.
    /// </summary>
    [BsonElement("number")]
    public int Number { get; set; }

    /// <summary>
    /// Represents the product which will be placed in the the lane.
    /// </summary>
    [BsonElement("janCode")]
    public string JanCode { get; set; }

    /// <summary>
    /// The number of drinks that can be placed in the lane.
    /// </summary>
    [BsonElement("quantity")]
    public int Quantity { get; set; }

    /// <summary>
    /// The X-coordinate of the lane within the row.
    /// </summary>
    [BsonElement("positionX")]
    public int PositionX { get; set; }
}