using Application.Interfaces;
using Domain.Entities.Shelfs;
using System.Text.Json;
using Domain.Entities.Skus;
using MiniExcelLibs;

namespace Application.UseCases;

public class InitialDataUseCase : IInitialDataUseCase
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
    /// InitialDataUseCase constructor
    /// </summary>
    /// <param name="shelfRepository">IShelfRepository</param>
    /// <param name="skuRepository">ISkuRepository</param>
    public InitialDataUseCase(
        IShelfRepository shelfRepository,
        ISkuRepository skuRepository)
    {
        _shelfRepository = shelfRepository;
        _skuRepository = skuRepository;
    }

    /// <summary>
    /// Initialize from files
    /// </summary>
    public async Task InitialAsync()
    {
        //// Check Data
        var shelveCount = await _shelfRepository.GetCountAsync();
        var skuCount = await _skuRepository.GetCountAsync();
        if (shelveCount == 0)
        {
            //// Read shelf.json
            var filePath = Path.Combine("..", "Domain", "Files", "shelf.json");
            var jsonData = await File.ReadAllTextAsync(filePath);
            var shelve = JsonSerializer.Deserialize<Shelf>(jsonData);
            await _shelfRepository.AddAsync(shelve);
        }

        if (skuCount == 0)
        {
            //// Read sku.csv
            var skus = LoadSkusFromCsv();
            await _skuRepository.AddManyAsync(skus);
        }
    }

    /// <summary>
    /// load skus from csv
    /// </summary>
    /// <returns>Sku List</returns>
    private List<Sku> LoadSkusFromCsv()
    {
        string csvFilePath = Path.Combine("..", "Domain", "Files", "sku.csv");
        using var stream = File.OpenRead(csvFilePath);
        var skus = stream.Query<Sku>(null, ExcelType.CSV);
        return skus.ToList();
    }
}