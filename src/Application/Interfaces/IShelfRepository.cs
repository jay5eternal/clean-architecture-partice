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
}