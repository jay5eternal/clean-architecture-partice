namespace Domain.Entities.Skus;

/// <summary>
/// Represents an individual item that is placed on the shelf
/// </summary>
public class Sku
{
    /// <summary>
    /// A unique identifier for the SKU, equivalent to a barcode.
    /// </summary>
    public string JanCode { get; set; }

    /// <summary>
    /// The name of the SKU.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The width of the product.
    /// </summary>
    public decimal X { get; set; }

    /// <summary>
    /// The depth of the product.
    /// </summary>
    public decimal Y { get; set; }
    
    /// <summary>
    /// The height of the product.
    /// </summary>
    public decimal Z { get; set; }

    /// <summary>
    /// A URL pointing to an image of the SKU.
    /// </summary>
    public string ImageURL { get; set; }
    
    /// <summary>
    /// The volume or size of the drink (e.g., 355ml, 500ml).
    /// </summary>
    public int Size { get; set; }

    /// <summary>
    /// Time stamp when registered.
    /// </summary>
    public long TimeStamp { get; set; }

    /// <summary>
    /// The shape of the product (e.g., Can, Bottle, Box).
    /// </summary>
    public string Shape { get; set; }
}