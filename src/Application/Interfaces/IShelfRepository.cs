using Contracts.Shelf;
using Domain.Entities.Shelfs;

namespace Application.Interfaces;

/// <summary>
/// interface of ShelfRepository
/// </summary>
public interface IShelfRepository
{
    /// <summary>
    /// Add Shelf
    /// </summary>
    /// <param name="shelf">Shelf</param>
    Task AddAsync(Shelf shelf);

    /// <summary>
    /// Get Document Count
    /// </summary>
    /// <returns>Document Count</returns>
    Task<int> GetCountAsync();
    
    /// <summary>
    /// Get Shelf Document 
    /// </summary>
    /// <returns>Shelf Document</returns>
    Task<Shelf> GetAsync();

    /// <summary>
    /// Get Row Document List
    /// </summary>
    /// <param name="cabinetNumber">A unique identifier for the Cabinet.</param>
    /// <param name="rowNumber">A unique identifier for the Row.</param>
    /// <returns>Row Document List</returns>
    Task<List<Row>> GetRowListAsync(int cabinetNumber, int? rowNumber);

    /// <summary>
    /// AddSkuToShelf 
    /// </summary>
    /// <param name="request">AddSkuToShelf Request Entity</param>
    Task AddSkuAsync(AddSkuToShelfRequest request);
}