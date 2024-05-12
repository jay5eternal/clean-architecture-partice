﻿namespace Contracts.Shelf;

/// <summary>
/// MoveSkuOriginPosition Request Entity
/// </summary>
public class MoveSkuOriginPositionRequest
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
    /// The number of drinks that can be placed in the lane.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// The X-coordinate of the lane within the row.
    /// </summary>
    public int PositionX { get; set; }
}