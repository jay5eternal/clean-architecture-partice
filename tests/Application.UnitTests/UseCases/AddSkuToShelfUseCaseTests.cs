using Application.Interfaces;
using Application.UseCases;
using Contracts.Shelf;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Application.UnitTests.UseCases;

public class AddSkuToShelfUseCaseTests
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
    /// AddSkuToShelfUseCase
    /// </summary>
    private readonly AddSkuToShelfUseCase _addSkuToShelfUseCase;

    /// <summary>
    /// AddSkuToShelfUseCaseTests constructor
    /// </summary>
    public AddSkuToShelfUseCaseTests()
    {
        _shelfRepository = Substitute.For<IShelfRepository>();
        _skuRepository = Substitute.For<ISkuRepository>();
        _addSkuToShelfUseCase = new AddSkuToShelfUseCase(_shelfRepository, _skuRepository);
    }

    [Fact]
    public async Task AddSkuToShelf_JanCode_Not_Exist_Should_Throw_Exception()
    {
        // Arrange
        var request = new AddSkuToShelfRequest
        {
            CabinetNumber = 4,
            RowNumber = 3,
            LaneNumber = 2,
            JanCode = "1234",
            Quantity = 1,
            PositionX = 10,
            IsLaneExist = true
        };
        _skuRepository.GetAsync(Arg.Any<string>()).ReturnsNull();

        //// Act & Assert
        var exception = await Assert.ThrowsAsync<ApplicationException>(() => _addSkuToShelfUseCase.ExecuteAsync(request));
        
        Assert.Equal("Sku is not exist", exception.Message);
        Assert.Equal(typeof(ApplicationException), exception.GetType());
    }
}