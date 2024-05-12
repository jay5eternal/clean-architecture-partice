using Domain.Entities.Skus;

namespace Application.Interfaces;

/// <summary>
/// interface of SkuRepository
/// </summary>
public interface ISkuRepository
{
    /// <summary>
    /// Add Many skus
    /// </summary>
    /// <param name="skus">Sku List</param>
    Task AddManyAsync(List<Sku> skus);

    /// <summary>
    /// Get Document Count
    /// </summary>
    /// <returns>Document Count</returns>
    Task<int> GetCountAsync();

    /// <summary>
    /// Get Sku Document
    /// </summary>
    /// <param name="janCode">A unique identifier for the SKU</param>
    /// <returns>Sku Document</returns>
    Task<Sku> GetAsync(string janCode);
}