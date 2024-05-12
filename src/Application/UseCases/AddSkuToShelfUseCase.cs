using Application.Interfaces;
using Contracts.Shelf;

namespace Application.UseCases;

/// <summary>
/// AddShelfUseCase
/// </summary>
public class AddSkuToShelfUseCase : IAddSkuToShelfUseCase
{
    /// <summary>
    /// IShelfRepository
    /// </summary>
    private readonly IShelfRepository _shelfRepository;
    
    /// <summary>
    /// ISkuRepository
    /// </summary>
    private readonly ISkuRepository _skuRepository;

    /// <summary>
    /// AddShelfUseCase constructor
    /// </summary>
    /// <param name="shelfRepository">IShelfRepository</param>
    /// <param name="skuRepository">ISkuRepository</param>
    public AddSkuToShelfUseCase(
        IShelfRepository shelfRepository,
        ISkuRepository skuRepository)
    {
        _shelfRepository = shelfRepository;
        _skuRepository = skuRepository;
    }
    
    /// <summary>
    /// Execute
    /// </summary>
    /// <param name="request">AddSkuToShelf Request Entity</param>
    /// <returns>Task</returns>
    public async Task ExecuteAsync(AddSkuToShelfRequest request)
    {
        //// Validate
        await DoValidateBeforeAddAsync(request);
        await _shelfRepository.AddSkuAsync(request);
    }

    /// <summary>
    /// DoValidate
    /// </summary>
    /// <param name="request">AddSkuToShelf Request Entity</param>
    public async Task DoValidateBeforeAddAsync(AddSkuToShelfRequest request)
    {
        var sku = await this._skuRepository.GetAsync(request.JanCode);
        if (sku is null)
        {
            throw new ApplicationException("Sku is not exist");
        }

        var rows = await _shelfRepository.GetRowListAsync(request.CabinetNumber, request.RowNumber);
        if (rows is null || rows.Any() == false)
        {
            throw new ApplicationException("Row is not exist");
        }

        if (request.IsLaneExist || request.LaneNumber.HasValue)
        {
            var lane = rows.SelectMany(x => x.Lanes)
                .SingleOrDefault(l => l.Number == request.LaneNumber && l.JanCode == request.JanCode);
            if (lane is null)
            {
                throw new ApplicationException("Lane is not exist");
            }
        }
        else
        {
            var hasLaneExistInDb = rows.SelectMany(x => x.Lanes)
                .Any(l => l.JanCode == request.JanCode);
            if (hasLaneExistInDb)
            {
                throw new ApplicationException("JanCode is exist In Lane"); 
            }
        }
    }
}