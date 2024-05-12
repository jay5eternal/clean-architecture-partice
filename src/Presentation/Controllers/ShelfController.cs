using Application.Interfaces;
using Contracts.Shelf;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

/// <summary>
/// ShelfController
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ShelfController : ControllerBase
{
    /// <summary>
    /// IGetShelfUseCase
    /// </summary>
    private readonly IGetShelfUseCase _getShelfUseCase;

    /// <summary>
    /// IAddShelfUseCase
    /// </summary>
    private readonly IAddSkuToShelfUseCase _addSkuToShelfUseCase;

    /// <summary>
    /// IMoveSkuInShelfUseCase
    /// </summary>
    private readonly IMoveSkuInShelfUseCase _moveSkuInShelfUseCase;

    /// <summary>
    /// ShelfController constructor
    /// </summary>
    /// <param name="getShelfUseCase">IGetShelfUseCase</param>
    /// <param name="addSkuToShelfUseCase">IAddShelfUseCase</param>
    /// <param name="moveSkuInShelfUseCase">IMoveSkuInShelfUseCase</param>
    public ShelfController(
        IGetShelfUseCase getShelfUseCase,
        IAddSkuToShelfUseCase addSkuToShelfUseCase,
        IMoveSkuInShelfUseCase moveSkuInShelfUseCase)
    {
        _getShelfUseCase = getShelfUseCase;
        _addSkuToShelfUseCase = addSkuToShelfUseCase;
        _moveSkuInShelfUseCase = moveSkuInShelfUseCase;
    }

    /// <summary>
    /// Get Current Shelf
    /// </summary>
    /// <returns>Shelf</returns>
    [HttpGet]
    public async Task<IActionResult> GetShelf()
    {
        try
        {
            var shelf = await _getShelfUseCase.ExecuteAsync();
            var response = new GetShelfResponse
            {
                Shelf = shelf
            };
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Add Sku To Shelf
    /// </summary>
    /// <param name="request">AddSkuToShelf Request Entity</param>
    /// <returns>ActionResult</returns>
    [HttpPost("sku-add")]
    public async Task<IActionResult> AddSku([FromBody] AddSkuToShelfRequest request)
    {
        try
        {
            await _addSkuToShelfUseCase.ExecuteAsync(request);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Move Sku In Shelf
    /// </summary>
    /// <param name="request">MoveSkuInShel Request Entity</param>
    /// <returns>ActionResult</returns>
    [HttpPost("sku-move")]
    public async Task<IActionResult> MoveSku([FromBody] MoveSkuInShelfRequest request)
    {
        try
        {
            await _moveSkuInShelfUseCase.ExecuteAsync(request);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}