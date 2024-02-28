using FlowerShop.ApplicationService.DataTransferObjects.Shop;
using FlowerShop.ApplicationService.Interfaces.Mappers;
using FlowerShop.ApplicationService.Interfaces.Services;
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
public sealed class ShopServiceTests
{
    private readonly Mock<IShopRepository> _shopRepositoryMock;
    private readonly Mock<IShopMapper> _shopMapperMock;
    private readonly Mock<IFlowerServiceFacade> _flowerServiceFacadeMock;
    private readonly Mock<INotificationHandler> _notificationHandlerMock;
    private readonly Mock<IValidator<Shop>> _validatorMock;
    private readonly ShopService _shopService;

    public ShopServiceTests()
    {
        _shopRepositoryMock = new Mock<IShopRepository>();
        _shopMapperMock = new Mock<IShopMapper>();
        _flowerServiceFacadeMock = new Mock<IFlowerServiceFacade>();
        _notificationHandlerMock = new Mock<INotificationHandler>();
        _validatorMock = new Mock<IValidator<Shop>>();
        _shopService = new ShopService(_shopRepositoryMock.Object, _shopMapperMock.Object, _flowerServiceFacadeMock.Object,
            _notificationHandlerMock.Object, _validatorMock.Object);
    }

    [Fact]
    public async Task AddAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var flowerIdList = new List<int>()
        {
            1,
            2
        };
        var shopSave = ShopBuilder.NewObject().WithFlowerIdList(flowerIdList).SaveBuild();

        var shop = ShopBuilder.NewObject().DomainBuild();
        _shopMapperMock.Setup(s => s.SaveToDomain(It.IsAny<ShopSave>()))
            .Returns(shop);

        var validationResult = new ValidationResult();
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<Shop>(), It.IsAny<CancellationToken>()))
           .ReturnsAsync(validationResult);

        var flower = FlowerBuilder.NewObject().DomainBuild();
        _flowerServiceFacadeMock.SetupSequence(f => f.GetByIdReturnsDomainObjectAsync(It.IsAny<int>()))
            .ReturnsAsync(flower)
            .ReturnsAsync(flower);

        _shopRepositoryMock.Setup(s => s.AddAsync(It.IsAny<Shop>()))
            .ReturnsAsync(true);

        // A
        var addResult = await _shopService.AddAsync(shopSave);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _shopRepositoryMock.Verify(s => s.AddAsync(It.IsAny<Shop>()), Times.Once());

        Assert.True(addResult);
    }

    [Fact]
    public async Task AddAsync_InvalidEntity_ReturnsFalse()
    {
        // A
        var shopSave = ShopBuilder.NewObject().SaveBuild();

        var shop = ShopBuilder.NewObject().DomainBuild();
        _shopMapperMock.Setup(s => s.SaveToDomain(It.IsAny<ShopSave>()))
            .Returns(shop);

        var validationFailureList = new List<ValidationFailure>()
        {
            new("tes", "test"),
            new("tes", "test")
        };
        var validationResult = new ValidationResult()
        {
            Errors = validationFailureList
        };
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<Shop>(), It.IsAny<CancellationToken>()))
           .ReturnsAsync(validationResult);

        // A
        var addResult = await _shopService.AddAsync(shopSave);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(validationResult.Errors.Count));
        _flowerServiceFacadeMock.Verify(f => f.GetByIdReturnsDomainObjectAsync(It.IsAny<int>()), Times.Never());
        _shopRepositoryMock.Verify(s => s.AddAsync(It.IsAny<Shop>()), Times.Never());

        Assert.False(addResult);
    }

    [Fact]
    public async Task AddAsync_FlowerDoesNotExist_ReturnsTrue()
    {
        // A
        var flowerIdList = new List<int>()
        {
            1,
            2,
            5
        };
        var shopSave = ShopBuilder.NewObject().WithFlowerIdList(flowerIdList).SaveBuild();

        var shop = ShopBuilder.NewObject().DomainBuild();
        _shopMapperMock.Setup(s => s.SaveToDomain(It.IsAny<ShopSave>()))
            .Returns(shop);

        var validationResult = new ValidationResult();
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<Shop>(), It.IsAny<CancellationToken>()))
           .ReturnsAsync(validationResult);

        var flower = FlowerBuilder.NewObject().DomainBuild();
        _flowerServiceFacadeMock.SetupSequence(f => f.GetByIdReturnsDomainObjectAsync(It.IsAny<int>()))
            .ReturnsAsync(flower)
            .Returns(Task.FromResult<Flower?>(null));

        // A
        var addResult = await _shopService.AddAsync(shopSave);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        _flowerServiceFacadeMock.Verify(f => f.GetByIdReturnsDomainObjectAsync(It.IsAny<int>()), Times.Exactly(2));
        _shopRepositoryMock.Verify(s => s.AddAsync(It.IsAny<Shop>()), Times.Never());

        Assert.False(addResult);
    }

    [Fact]
    public async Task UpdateAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var flowerIdList = new List<int>()
        {
            1,
            1,
            2
        };
        var shopUpdate = ShopBuilder.NewObject().WithFlowerIdList(flowerIdList).UpdateBuild();

        var shop = ShopBuilder.NewObject().DomainBuild();
        _shopRepositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<int>(), false))
            .ReturnsAsync(shop);

        _shopMapperMock.Setup(s => s.UpdateToDomain(It.IsAny<ShopUpdate>(), It.IsAny<Shop>()));

        var validationResult = new ValidationResult();
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<Shop>(), It.IsAny<CancellationToken>()))
           .ReturnsAsync(validationResult);

        var flower = FlowerBuilder.NewObject().DomainBuild();
        _flowerServiceFacadeMock.SetupSequence(f => f.GetByIdReturnsDomainObjectAsync(It.IsAny<int>()))
            .ReturnsAsync(flower)
            .ReturnsAsync(flower)
            .ReturnsAsync(flower);

        _shopRepositoryMock.Setup(s => s.UpdateAsync(It.IsAny<Shop>()))
            .ReturnsAsync(true);

        // A
        var updateResult = await _shopService.UpdateAsync(shopUpdate);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _shopRepositoryMock.Verify(s => s.UpdateAsync(It.IsAny<Shop>()), Times.Once());

        Assert.True(updateResult);
    }

    [Fact]
    public async Task UpdateAsync_EntityDoesNotExist_ReturnsFalse()
    {
        // A
        var shopUpdate = ShopBuilder.NewObject().UpdateBuild();

        _shopRepositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<int>(), false))
            .Returns(Task.FromResult<Shop?>(null));

        // A
        var updateResult = await _shopService.UpdateAsync(shopUpdate);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        _shopMapperMock.Verify(s => s.UpdateToDomain(It.IsAny<ShopUpdate>(), It.IsAny<Shop>()), Times.Never());
        _validatorMock.Verify(v => v.ValidateAsync(It.IsAny<Shop>(), It.IsAny<CancellationToken>()), Times.Never());
        _flowerServiceFacadeMock.Verify(f => f.GetByIdReturnsDomainObjectAsync(It.IsAny<int>()), Times.Never());
        _shopRepositoryMock.Verify(s => s.UpdateAsync(It.IsAny<Shop>()), Times.Never());

        Assert.False(updateResult);
    }

    [Fact]
    public async Task UpdateAsync_InvalidEntity_ReturnsFalse()
    {
        // A
        var shopUpdate = ShopBuilder.NewObject().UpdateBuild();

        var shop = ShopBuilder.NewObject().DomainBuild();
        _shopRepositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<int>(), false))
            .ReturnsAsync(shop);

        _shopMapperMock.Setup(s => s.UpdateToDomain(It.IsAny<ShopUpdate>(), It.IsAny<Shop>()));

        var validationFailureList = new List<ValidationFailure>()
        {
            new("tes", "test"),
            new("tes", "test")
        };
        var validationResult = new ValidationResult()
        {
            Errors = validationFailureList
        };
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<Shop>(), It.IsAny<CancellationToken>()))
           .ReturnsAsync(validationResult);

        // A
        var updateResult = await _shopService.UpdateAsync(shopUpdate);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(validationResult.Errors.Count));
        _flowerServiceFacadeMock.Verify(f => f.GetByIdReturnsDomainObjectAsync(It.IsAny<int>()), Times.Never());
        _shopRepositoryMock.Verify(s => s.UpdateAsync(It.IsAny<Shop>()), Times.Never());

        Assert.False(updateResult);
    }

    [Fact]
    public async Task UpdateAsync_FlowerDoesNotExist_ReturnsFalse()
    {
        // A
        var flowerIdList = new List<int>()
        {
            1
        };
        var shopUpdate = ShopBuilder.NewObject().WithFlowerIdList(flowerIdList).UpdateBuild();

        var shop = ShopBuilder.NewObject().DomainBuild();
        _shopRepositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<int>(), false))
            .ReturnsAsync(shop);

        _shopMapperMock.Setup(s => s.UpdateToDomain(It.IsAny<ShopUpdate>(), It.IsAny<Shop>()));

        var validationResult = new ValidationResult();
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<Shop>(), It.IsAny<CancellationToken>()))
           .ReturnsAsync(validationResult);

        _flowerServiceFacadeMock.SetupSequence(f => f.GetByIdReturnsDomainObjectAsync(It.IsAny<int>()))
            .Returns(Task.FromResult<Flower?>(null));

        // A
        var updateResult = await _shopService.UpdateAsync(shopUpdate);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        _shopRepositoryMock.Verify(s => s.UpdateAsync(It.IsAny<Shop>()), Times.Never());

        Assert.False(updateResult);
    }

    [Fact]
    public async Task DeleteAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var id = 1;

        _shopRepositoryMock.Setup(s => s.ExistsAsync(It.IsAny<int>()))
            .ReturnsAsync(true);

        _shopRepositoryMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
            .ReturnsAsync(true);
        // A
        var deleteResult = await _shopService.DeleteAsync(id);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _shopRepositoryMock.Verify(s => s.DeleteAsync(It.IsAny<int>()), Times.Once());

        Assert.True(deleteResult);
    }

    [Fact]
    public async Task DeleteAsync_EntityDoesNotExist_ReturnsFalse()
    {
        // A
        var id = 1;

        _shopRepositoryMock.Setup(s => s.ExistsAsync(It.IsAny<int>()))
            .ReturnsAsync(false);

        // A
        var deleteResult = await _shopService.DeleteAsync(id);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        _shopRepositoryMock.Verify(s => s.DeleteAsync(It.IsAny<int>()), Times.Never());

        Assert.False(deleteResult);
    }

    [Fact]
    public async Task GetByIdAsync_SuccessfulScenario_ReturnsEntity()
    {
        // A
        var id = 12;

        var shop = ShopBuilder.NewObject().DomainBuild();
        _shopRepositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<int>(), true))
            .ReturnsAsync(shop);

        var shopResponse = ShopBuilder.NewObject().ResponseBuild();
        _shopMapperMock.Setup(s => s.DomainToResponse(It.IsAny<Shop>()))
            .Returns(shopResponse);

        // A
        var shopResponseResult = await _shopService.GetByIdAsync(id);

        // A
        _shopMapperMock.Verify(s => s.DomainToResponse(It.IsAny<Shop>()), Times.Once());

        Assert.NotNull(shopResponseResult);
    }

    [Fact]
    public async Task GetByIdAsync_EntityDoesNotExist_ReturnsNull()
    {
        // A
        var id = 12;

        _shopRepositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<int>(), true))
            .Returns(Task.FromResult<Shop?>(null));

        // A
        var shopResponseResult = await _shopService.GetByIdAsync(id);

        // A
        _shopMapperMock.Verify(s => s.DomainToResponse(It.IsAny<Shop>()), Times.Never());

        Assert.Null(shopResponseResult);
    }

    [Fact]
    public async Task GetAllPaginatedAsync_SuccessfulScenario_ReturnsEntityPageList()
    {
        // A
        var pageParameters = new PageParameters()
        {
            PageNumber = 1,
            PageSize = 9
        };

        var shopList = new List<Shop>()
        {
            ShopBuilder.NewObject().DomainBuild(),
            ShopBuilder.NewObject().DomainBuild(),
            ShopBuilder.NewObject().DomainBuild()
        };
        var shopPageList = new PageList<Shop>()
        {
            CurrentPage = 1,
            PageSize = 3,
            Result = shopList,
            TotalCount = 1,
            TotalPages = 8
        };
        _shopRepositoryMock.Setup(s => s.GetAllPaginatedAsync(It.IsAny<PageParameters>()))
            .ReturnsAsync(shopPageList);

        var shopResponseList = new List<ShopResponse>()
        {
              ShopBuilder.NewObject().ResponseBuild(),
              ShopBuilder.NewObject().ResponseBuild(),
              ShopBuilder.NewObject().ResponseBuild(),
              ShopBuilder.NewObject().ResponseBuild()
        };
        var shopResponsePageList = new PageList<ShopResponse>()
        {
            CurrentPage = 1,
            PageSize = 9,
            Result = shopResponseList,
            TotalCount = 8,
            TotalPages = 3
        };
        _shopMapperMock.Setup(s => s.DomainPageListToResponsePageList(It.IsAny<PageList<Shop>>()))
            .Returns(shopResponsePageList);

        // A
        var shopResponsePageListResult = await _shopService.GetAllPaginatedAsync(pageParameters);

        // A
        Assert.Equal(shopResponsePageListResult.Result.Count, shopResponsePageList.Result.Count);
    }
}
