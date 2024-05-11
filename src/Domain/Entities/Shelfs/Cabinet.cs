namespace Domain.Entities.Shelfs;

/// <summary>
/// store has several Cabinets
/// </summary>
public class Cabinet
{
    /// <summary>
    /// A unique identifier for the cabinet within the store.
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// A list of rows contained within the cabinet.
    /// </summary>
    public List<Row> Rows { get; set; }

    /// <summary>
    /// The origin position of the cabinet based on the robot's coordinate system.
    /// </summary>
    public Position Position { get; set; }

    /// <summary>
    /// The size of the cabinet.
    /// </summary>
    public Size Size { get; set; }
}