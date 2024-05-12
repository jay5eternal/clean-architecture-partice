namespace Contracts.Shelf;

/// <summary>
/// GetShelfResponse
/// </summary>
public class GetShelfResponse
{
    /// <summary>
    /// Represents a physical shelf in the store and contains multiple SKUs. Store has several Cabinets.
    /// </summary>
    public Domain.Entities.Shelfs.Shelf Shelf { get; set; }
}