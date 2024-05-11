namespace Domain.Entities.Shelfs;

/// <summary>
/// The origin position of the cabinet based on the robot's coordinate system.
/// </summary>
public class Position
{
    /// <summary>
    /// The X-coordinate of the cabinet.
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// The Y-coordinate of the cabinet.
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// The Z-coordinate of the cabinet.
    /// </summary>
    public int Z { get; set; }
}