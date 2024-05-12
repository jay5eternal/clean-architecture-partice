namespace Contracts.Shelf;

/// <summary>
/// MoveSkuInShel Request Entity
/// </summary>
public class MoveSkuInShelfRequest
{
    /// <summary>
    /// A unique identifier for the SKU
    /// </summary>
    public string JanCode { get; set; }
    
    /// <summary>
    /// SkuOriginPosition Request Entity
    /// </summary>
    public MoveSkuOriginPositionRequest OriginPosition { get; set; }

    /// <summary>
    /// MoveSkuNewPosition Request Entity
    /// </summary>
    public MoveSkuNewPositionRequest NewPosition { get; set; }
}