using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

/// <summary>
/// for the data initialize
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class DataController : ControllerBase
{
    /// <summary>
    /// IInitialDataUseCase
    /// </summary>
    private readonly IInitialDataUseCase _initialDataUseCase;

    /// <summary>
    /// DataController constructor
    /// </summary>
    /// <param name="initialDataUseCase">IInitialDataUseCase</param>
    public DataController(
        IInitialDataUseCase initialDataUseCase)
    {
        _initialDataUseCase = initialDataUseCase;
    }

    /// <summary>
    /// load initial data from files
    /// </summary>
    /// <returns>ActionResult</returns>
    [HttpPost("load-initial-data")]
    public async Task<IActionResult> LoadInitialData()
    {
        await _initialDataUseCase.InitialAsync();
        return Ok("Data loaded successfully");
    }
}