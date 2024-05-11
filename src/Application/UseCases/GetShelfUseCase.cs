using Application.Interfaces;
using Domain.Entities.Shelfs;

namespace Application.UseCases;

/// <summary>
/// GetShelfUseCase
/// </summary>
public class GetShelfUseCase : IGetShelfUseCase
{
    /// <summary>
    /// IShelfRepository
    /// </summary>
    private readonly IShelfRepository _shelfRepository;

    /// <summary>
    /// GetShelfUseCase constructor
    /// </summary>
    /// <param name="shelfRepository">IShelfRepository</param>
    public GetShelfUseCase(
        IShelfRepository shelfRepository)
    {
        _shelfRepository = shelfRepository;
    }
    
    /// <summary>
    /// Execute
    /// </summary>
    /// <returns>Shelf</returns>
    public async Task<Shelf> ExecuteAsync()
    {
        var shelf = await _shelfRepository.GetAsync();
        return shelf;
    }
}