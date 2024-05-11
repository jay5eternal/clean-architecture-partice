namespace Domain.Entities.Shelfs;

/// <summary>
/// Represents a physical shelf in the store and contains multiple SKUs. Store has several Cabinets.
/// </summary>
public class Shelf
{
    /// <summary>
    /// store has several Cabinets
    /// </summary>
    public List<Cabinet> Cabinets { get; set; }
}