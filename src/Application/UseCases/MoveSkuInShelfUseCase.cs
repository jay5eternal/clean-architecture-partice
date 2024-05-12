using Application.Interfaces;
using Contracts.Shelf;

namespace Application.UseCases;

/// <summary>
/// MoveSkuInShelfUseCase
/// </summary>
public class MoveSkuInShelfUseCase : IMoveSkuInShelfUseCase
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
    /// IAddShelfUseCase
    /// </summary>
    private readonly IAddSkuToShelfUseCase _addSkuToShelfUseCase;

    /// <summary>
    /// MoveSkuInShelfUseCase constructor
    /// </summary>
    /// <param name="shelfRepository">IShelfRepository</param>
    /// <param name="skuRepository">ISkuRepository</param>
    /// <param name="addSkuToShelfUseCase">IAddSkuToShelfUseCase</param>
    public MoveSkuInShelfUseCase(
        IShelfRepository shelfRepository,
        ISkuRepository skuRepository,
        IAddSkuToShelfUseCase addSkuToShelfUseCase)
    {
        _shelfRepository = shelfRepository;
        _skuRepository = skuRepository;
        _addSkuToShelfUseCase = addSkuToShelfUseCase;
    }
    
    /// <summary>
    /// Execute
    /// </summary>
    /// <param name="request">MoveSkuInShel Request Entity</param>
    /// <returns>Task</returns>
    public async Task ExecuteAsync(MoveSkuInShelfRequest request)
    {
        //// Validate
        await DoValidateAsync(request);
        await _shelfRepository.MoveSkuInShelfAsync(request);
    }

    /// <summary>
    /// DoValidate
    /// </summary>
    /// <param name="request">MoveSkuInShel Request Entity</param>
    private async Task DoValidateAsync(MoveSkuInShelfRequest request)
    {
        var sku = await this._skuRepository.GetAsync(request.JanCode);
        if (sku is null)
        {
            throw new ApplicationException("Sku is not exist");
        }

        //// Check Origin
        var originPosition = request.OriginPosition;
        var originRows = await _shelfRepository.GetRowListAsync(originPosition.CabinetNumber, originPosition.RowNumber);
        if (originRows is null || originRows.Any() == false)
        {
            throw new ApplicationException("Origin Row is not exist");
        }

        var originLane = originRows.SelectMany(x => x.Lanes)
            .SingleOrDefault(l => l.Number == originPosition.LaneNumber && l.JanCode == request.JanCode);
        
        if (originLane is null)
        {
            throw new ApplicationException("Origin Lane is not exist");
        }

        if (originLane.PositionX != originPosition.PositionX)
        {
            throw new ApplicationException("Origin PositionX is invalid");
        }
        
        if (originLane.Quantity != originPosition.Quantity)
        {
            throw new ApplicationException("Origin Quantity is invalid");
        }

        //// Check New
        var newPosition = request.NewPosition;
        var addRequest = new AddSkuToShelfRequest
        {
            CabinetNumber = newPosition.CabinetNumber,
            RowNumber = newPosition.RowNumber,
            LaneNumber = newPosition.LaneNumber,
            JanCode = request.JanCode,
            Quantity = newPosition.Quantity,
            PositionX = newPosition.PositionX,
            IsLaneExist = newPosition.IsLaneExist
        };
        await _addSkuToShelfUseCase.DoValidateBeforeAddAsync(addRequest);
    }
}