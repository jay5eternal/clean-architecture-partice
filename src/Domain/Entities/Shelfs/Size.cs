﻿namespace Domain.Entities.Shelfs;

/// <summary>
/// The size of the cabinet.
/// </summary>
public class Size
{
    /// <summary>
    /// The width of the cabinet.
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// The depth of the cabinet.
    /// </summary>
    public int Depth { get; set; }

    /// <summary>
    /// The overall height of the cabinet.
    /// </summary>
    public int Height { get; set; }
}