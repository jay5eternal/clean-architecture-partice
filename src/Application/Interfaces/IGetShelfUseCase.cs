using Domain.Entities.Shelfs;

namespace Application.Interfaces;

/// <summary>
/// interface of IGetShelfUseCase
/// </summary>
public interface IGetShelfUseCase
{
    /// <summary>
    /// Execute
    /// </summary>
    /// <returns>Shelf</returns>
    Task<Shelf> ExecuteAsync();
}