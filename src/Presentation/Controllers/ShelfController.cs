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
    /// ShelfController constructor
    /// </summary>
    /// <param name="getShelfUseCase">IGetShelfUseCase</param>
    public ShelfController(IGetShelfUseCase getShelfUseCase)
    {
        _getShelfUseCase = getShelfUseCase;
    }

    /// <summary>
    /// Get Current Shelf
    /// </summary>
    /// <returns>Shelf</returns>
    [HttpGet]
    public async Task<IActionResult> GetShelf()
    {
        var shelf = await _getShelfUseCase.ExecuteAsync();
        var response = new GetShelfResponse
        {
            Shelf = shelf
        };
        return Ok(response);
    }
}