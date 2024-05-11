namespace Domain.Entities.Shelfs;

/// <summary>
/// A list of rows contained within the cabinet.
/// </summary>
public class Row
{
    /// <summary>
    /// A unique identifier for the row within the cabinet.
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// A list of lanes contained within the row.
    /// </summary>
    public List<Lane> Lanes { get; set; }

    /// <summary>
    /// The Z-coordinate of the row within the cabinet.
    /// </summary>
    public int PositionZ { get; set; }

    /// <summary>
    /// The height of the row.
    /// </summary>
    public RowSize Size { get; set; }
}