using FlowerShop.ApplicationService.DataTransferObjects.Flower;
using FlowerShop.ApplicationService.Interfaces.Mappers;
using FlowerShop.ApplicationService.Services;
using FlowerShop.Business.Interfaces.Repositories;
using FlowerShop.Business.Interfaces.Settings;
using FlowerShop.Business.Settings.PaginationSettings;
using FlowerShop.Domain.Entites;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using UnitTests.TestBuilders;

namespace UnitTests.ServicesTests;
public sealed class FlowerServiceTests
{
    private readonly Mock<IFlowerRepository> _flowerRepositoryMock;
    private readonly Mock<IFlowerMapper> _flowerMapperMock;
    private readonly Mock<INotificationHandler> _notificationHandlerMock;
    private readonly Mock<IValidator<Flower>> _validatorMock;
    private readonly FlowerService _flowerService;

    public FlowerServiceTests()
    {
        _flowerRepositoryMock = new Mock<IFlowerRepository>();
        _flowerMapperMock = new Mock<IFlowerMapper>();
        _notificationHandlerMock = new Mock<INotificationHandler>();
        _validatorMock = new Mock<IValidator<Flower>>();
        _flowerService = new FlowerService(_flowerRepositoryMock.Object, _flowerMapperMock.Object, _notificationHandlerMock.Object,
            _validatorMock.Object);
    }

    [Fact]
    public async Task AddAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var flowerSave = FlowerBuilder.NewObject().SaveBuild();

        var flower = FlowerBuilder.NewObject().DomainBuild();
        _flowerMapperMock.Setup(f => f.SaveToDomain(It.IsAny<FlowerSave>()))
            .Returns(flower);

        var validationResult = new ValidationResult();
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<Flower>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _flowerRepositoryMock.Setup(f => f.AddAsync(It.IsAny<Flower>()))
            .ReturnsAsync(true);

        // A
        var addResult = await _flowerService.AddAsync(flowerSave);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _flowerRepositoryMock.Verify(f => f.AddAsync(It.IsAny<Flower>()), Times.Once());

        Assert.True(addResult);
    }

    [Fact]
    public async Task AddAsync_InvalidEntity_ReturnsFalse()
    {
        // A
        var flowerSave = FlowerBuilder.NewObject().SaveBuild();

        var flower = FlowerBuilder.NewObject().DomainBuild();
        _flowerMapperMock.Setup(f => f.SaveToDomain(It.IsAny<FlowerSave>()))
            .Returns(flower);

        var validationFailureList = new List<ValidationFailure>()
        {
            new("tes", "test"),
            new("tes", "test")
        };
        var validationResult = new ValidationResult()
        {
            Errors = validationFailureList
        };
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<Flower>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        // A
        var addResult = await _flowerService.AddAsync(flowerSave);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(validationResult.Errors.Count));
        _flowerRepositoryMock.Verify(f => f.AddAsync(It.IsAny<Flower>()), Times.Never());

        Assert.False(addResult);
    }

    [Fact]
    public async Task UpdateAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var flowerUpdate = FlowerBuilder.NewObject().UpdateBuild();

        var flower = FlowerBuilder.NewObject().DomainBuild();
        _flowerRepositoryMock.Setup(f => f.GetByIdAsync(It.IsAny<int>(), false))
            .ReturnsAsync(flower);

        _flowerMapperMock.Setup(f => f.UpdateToDomain(It.IsAny<FlowerUpdate>(), It.IsAny<Flower>()));

        var validationResult = new ValidationResult();
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<Flower>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _flowerRepositoryMock.Setup(f => f.UpdateAsync(It.IsAny<Flower>()))
            .ReturnsAsync(true);

        // A
        var updateResult = await _flowerService.UpdateAsync(flowerUpdate);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _flowerRepositoryMock.Verify(f => f.UpdateAsync(It.IsAny<Flower>()), Times.Once());

        Assert.True(updateResult);
    }

    [Fact]
    public async Task UpdateAsync_EntityDoesNotExist_ReturnsFalse()
    {
        // A
        var flowerUpdate = FlowerBuilder.NewObject().UpdateBuild();

        _flowerRepositoryMock.Setup(f => f.GetByIdAsync(It.IsAny<int>(), false))
            .Returns(Task.FromResult<Flower?>(null));

        // A
        var updateResult = await _flowerService.UpdateAsync(flowerUpdate);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        _flowerMapperMock.Verify(f => f.UpdateToDomain(It.IsAny<FlowerUpdate>(), It.IsAny<Flower>()), Times.Never());
        _validatorMock.Verify(v => v.ValidateAsync(It.IsAny<Flower>(), It.IsAny<CancellationToken>()), Times.Never());
        _flowerRepositoryMock.Verify(f => f.UpdateAsync(It.IsAny<Flower>()), Times.Never());

        Assert.False(updateResult);
    }

    [Fact]
    public async Task UpdateAsync_InvalidEntity_ReturnsFalse()
    {
        // A
        var flowerUpdate = FlowerBuilder.NewObject().UpdateBuild();

        var flower = FlowerBuilder.NewObject().DomainBuild();
        _flowerRepositoryMock.Setup(f => f.GetByIdAsync(It.IsAny<int>(), false))
            .ReturnsAsync(flower);

        _flowerMapperMock.Setup(f => f.UpdateToDomain(It.IsAny<FlowerUpdate>(), It.IsAny<Flower>()));

        var validationFailureList = new List<ValidationFailure>()
        {
            new("tes", "test"),
            new("tes", "test")
        };
        var validationResult = new ValidationResult()
        {
            Errors = validationFailureList
        };
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<Flower>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        // A
        var updateResult = await _flowerService.UpdateAsync(flowerUpdate);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(validationResult.Errors.Count));
        _flowerRepositoryMock.Verify(f => f.UpdateAsync(It.IsAny<Flower>()), Times.Never());

        Assert.False(updateResult);
    }

    [Fact]
    public async Task DeleteAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var id = 1;

        _flowerRepositoryMock.Setup(f => f.ExistsAsync(It.IsAny<int>()))
            .ReturnsAsync(true);

        _flowerRepositoryMock.Setup(f => f.DeleteAsync(It.IsAny<int>()))
            .ReturnsAsync(true);
        // A
        var deleteResult = await _flowerService.DeleteAsync(id);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _flowerRepositoryMock.Verify(f => f.DeleteAsync(It.IsAny<int>()), Times.Once());

        Assert.True(deleteResult);
    }

    [Fact]
    public async Task DeleteAsync_EntityDoesNotExist_ReturnsFalse()
    {
        // A
        var id = 1;

        _flowerRepositoryMock.Setup(f => f.ExistsAsync(It.IsAny<int>()))
            .ReturnsAsync(false);

        // A
        var deleteResult = await _flowerService.DeleteAsync(id);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        _flowerRepositoryMock.Verify(f => f.DeleteAsync(It.IsAny<int>()), Times.Never());

        Assert.False(deleteResult);
    }

    [Fact]
    public async Task GetByIdAsync_SuccessfulScenario_ReturnsEntity()
    {
        // A
        var id = 1;

        var flower = FlowerBuilder.NewObject().DomainBuild();
        _flowerRepositoryMock.Setup(f => f.GetByIdAsync(It.IsAny<int>(), true))
            .ReturnsAsync(flower);

        var flowerResponse = FlowerBuilder.NewObject().ResponseBuild();
        _flowerMapperMock.Setup(f => f.DomainToResponse(It.IsAny<Flower>()))
            .Returns(flowerResponse);

        // A
        var flowerResponseResult = await _flowerService.GetByIdAsync(id);

        // A
        _flowerMapperMock.Verify(f => f.DomainToResponse(It.IsAny<Flower>()), Times.Once());

        Assert.NotNull(flowerResponseResult);
    }

    [Fact]
    public async Task GetByIdAsync_EntityDoesNotExist_ReturnsNull()
    {
        // A
        var id = 1;

        _flowerRepositoryMock.Setup(f => f.GetByIdAsync(It.IsAny<int>(), true))
            .Returns(Task.FromResult<Flower?>(null));

        // A
        var flowerResponseResult = await _flowerService.GetByIdAsync(id);

        // A
        _flowerMapperMock.Verify(f => f.DomainToResponse(It.IsAny<Flower>()), Times.Never());

        Assert.Null(flowerResponseResult);
    }

    [Fact]
    public async Task GetAllPaginatedAsync_SuccessfulScenario_ReturnsEntityPageList()
    {
        // A
        var pageParameters = new PageParameters()
        {
            PageNumber = 1,
            PageSize = 1
        };

        var flowerList = new List<Flower>()
        {
            FlowerBuilder.NewObject().DomainBuild(),
            FlowerBuilder.NewObject().DomainBuild(),
            FlowerBuilder.NewObject().DomainBuild()
        };
        var flowerPageList = new PageList<Flower>()
        {
            CurrentPage = 1,
            PageSize = 1,
            Result = flowerList,
            TotalCount = 1,
            TotalPages = 8
        };
        _flowerRepositoryMock.Setup(f => f.GetAllPaginatedAsync(It.IsAny<PageParameters>()))
            .ReturnsAsync(flowerPageList);

        var flowerResponseList = new List<FlowerResponse>()
        {
            FlowerBuilder.NewObject().ResponseBuild()
        };
        var flowerResponsePageList = new PageList<FlowerResponse>()
        {
            CurrentPage = 1,
            PageSize = 9,
            Result = flowerResponseList,
            TotalCount = 9,
            TotalPages = 7
        };
        _flowerMapperMock.Setup(f => f.DomainPageListToResponsePageList(It.IsAny<PageList<Flower>>()))
            .Returns(flowerResponsePageList);

        // A
        var flowerResponsePageListResult = await _flowerService.GetAllPaginatedAsync(pageParameters);

        // A
        Assert.Equal(flowerResponsePageListResult.Result.Count, flowerResponsePageList.Result.Count);
    }
}
