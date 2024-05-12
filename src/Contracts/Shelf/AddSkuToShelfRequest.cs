namespace Contracts.Shelf;

/// <summary>
/// AddSkuToShelf Request Entity
/// </summary>
public class AddSkuToShelfRequest
{
    /// <summary>
    /// A unique identifier for the Cabinet.
    /// </summary>
    public int CabinetNumber { get; set; }

    /// <summary>
    /// A unique identifier for the Row.
    /// </summary>
    public int RowNumber { get; set; }

    /// <summary>
    /// A unique identifier for the lane.
    /// </summary>
    public int? LaneNumber { get; set; }

    /// <summary>
    /// Represents the product which will be placed in the the lane.
    /// </summary>
    public string JanCode { get; set; }

    /// <summary>
    /// The number of drinks that can be placed in the lane.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// The X-coordinate of the lane within the row.
    /// </summary>
    public int PositionX { get; set; }

    /// <summary>
    /// IsLaneExist
    /// </summary>
    public bool IsLaneExist { get; set; }
}