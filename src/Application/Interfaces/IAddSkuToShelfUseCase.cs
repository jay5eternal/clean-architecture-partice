using Contracts.Shelf;

namespace Application.Interfaces;

/// <summary>
/// interface of IAddShelfUseCase
/// </summary>
public interface IAddSkuToShelfUseCase
{
    /// <summary>
    /// Execute
    /// </summary>
    /// <param name="request">AddSkuToShelf Request Entity</param>
    /// <returns>Task</returns>
    Task ExecuteAsync(AddSkuToShelfRequest request);
}