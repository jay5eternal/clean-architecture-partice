using Contracts.Shelf;

namespace Application.Interfaces;

/// <summary>
/// interface of MoveSkuInShelfUseCase
/// </summary>
public interface IMoveSkuInShelfUseCase
{
    /// <summary>
    /// Execute
    /// </summary>
    /// <param name="request">MoveSkuInShel Request Entity</param>
    /// <returns>Task</returns>
    Task ExecuteAsync(MoveSkuInShelfRequest request);
}