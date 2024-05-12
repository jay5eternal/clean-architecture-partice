namespace Contracts.Shelf;

/// <summary>
/// RemoveSku Request Entity
/// </summary>
public class RemoveSkuRequest
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
    public int LaneNumber { get; set; }

    /// <summary>
    /// A unique identifier for the SKU.
    /// </summary>
    public string JanCode { get; set; }
}