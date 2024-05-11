using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Persistence.Entities.Skus;

/// <summary>
/// Represents an individual item that is placed on the shelf
/// </summary>
public class Sku
{
    /// <summary>
    /// MongoDb ObjectId
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    /// <summary>
    /// A unique identifier for the SKU, equivalent to a barcode.
    /// </summary>
    [BsonElement("janCode")]
    public string JanCode { get; set; }

    /// <summary>
    /// The name of the SKU.
    /// </summary>
    [BsonElement("name")]
    public string Name { get; set; }

    /// <summary>
    /// The width of the product.
    /// </summary>
    [BsonElement("x")]
    public decimal X { get; set; }

    /// <summary>
    /// The depth of the product.
    /// </summary>
    [BsonElement("y")]
    public decimal Y { get; set; }
    
    /// <summary>
    /// The height of the product.
    /// </summary>
    [BsonElement("z")]
    public decimal Z { get; set; }

    /// <summary>
    /// A URL pointing to an image of the SKU.
    /// </summary>
    [BsonElement("imageURL")]
    public string ImageURL { get; set; }
    
    /// <summary>
    /// The volume or size of the drink (e.g., 355ml, 500ml).
    /// </summary>
    [BsonElement("size")]
    public int Size { get; set; }

    /// <summary>
    /// Time stamp when registered.
    /// </summary>
    [BsonElement("timeStamp")]
    public long TimeStamp { get; set; }

    /// <summary>
    /// The shape of the product (e.g., Can, Bottle, Box).
    /// </summary>
    [BsonElement("shape")]
    public string Shape { get; set; }
}