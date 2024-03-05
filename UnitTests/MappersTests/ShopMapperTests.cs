using FlowerShop.ApplicationService.DataTransferObjects.Flower;
using FlowerShop.ApplicationService.Interfaces.Mappers;
using FlowerShop.ApplicationService.Mappers;
using FlowerShop.CrossCutting.Settings.PaginationSettings;
using FlowerShop.Domain.Entites;
using Moq;
using UnitTests.TestBuilders;

namespace UnitTests.MappersTests;
public sealed class ShopMapperTests
{
    private readonly Mock<IFlowerMapperFacade> _flowerMapperFacadeMock;
    private readonly ShopMapper _shopMapper;

    public ShopMapperTests()
    {
        _flowerMapperFacadeMock = new Mock<IFlowerMapperFacade>();
        _shopMapper = new ShopMapper(_flowerMapperFacadeMock.Object);
    }

    [Fact]
    public void SaveToDomain_SuccessfulScenario_ReturnsDomainObject()
    {
        // A
        var shopSave = ShopBuilder.NewObject().SaveBuild();

        // A
        var shopResult = _shopMapper.SaveToDomain(shopSave);

        // A
        Assert.Equal(shopResult.Name, shopSave.Name);
        Assert.Equal(shopResult.Location, shopSave.Location);
        Assert.Equal(shopResult.Email, shopSave.Email);
        Assert.Empty(shopResult.Flowers);
    }

    [Fact]
    public void UpdateToDomain_SuccessfulScenario_ReturnsDomainObject()
    {
        // A
        var shopUpdate = ShopBuilder.NewObject().UpdateBuild();
        var shopResult = ShopBuilder.NewObject().DomainBuild();

        // A
        _shopMapper.UpdateToDomain(shopUpdate, shopResult);

        // A
        Assert.Equal(shopResult.Name, shopUpdate.Name);
        Assert.Equal(shopResult.Location, shopUpdate.Location);
        Assert.Equal(shopResult.Email, shopUpdate.Email);
    }

    [Fact]
    public void DomainToResponse_SuccesfulScenario_ReturnsResponseObject()
    {
        // A
        var shop = ShopBuilder.NewObject().DomainBuild();

        var flowerResponseList = new List<FlowerResponse>()
        {
            FlowerBuilder.NewObject().ResponseBuild()
        };
        _flowerMapperFacadeMock.Setup(f => f.DomainListToResponseList(It.IsAny<List<Flower>>()))
            .Returns(flowerResponseList);

        // A
        var shopResponseResult = _shopMapper.DomainToResponse(shop);

        // A
        Assert.Equal(shopResponseResult.Id, shop.Id);
        Assert.Equal(shopResponseResult.Name, shop.Name);
        Assert.Equal(shopResponseResult.Location, shop.Location);
        Assert.Equal(shopResponseResult.Email, shop.Email);
        Assert.Equal(shopResponseResult.CreationDate, shop.CreationDate);
        Assert.Equal(shopResponseResult.Flowers.Count, flowerResponseList.Count);
    }

    [Fact]
    public void DomainPageListToResponsePageList_SuccesfulScenario_ReturnsResponseObjectPageList()
    {
        // A
        var shopList = new List<Shop>()
        {
            ShopBuilder.NewObject().DomainBuild(),
            ShopBuilder.NewObject().DomainBuild(),
            ShopBuilder.NewObject().DomainBuild()
        };
        var shopPageList = new PageList<Shop>()
        {
            CurrentPage = 1,
            PageSize = 9,
            Result = shopList,
            TotalCount = 8,
            TotalPages = 1
        };

        var flowerResponseList = new List<FlowerResponse>()
        {
            FlowerBuilder.NewObject().ResponseBuild()
        };
        _flowerMapperFacadeMock.SetupSequence(f => f.DomainListToResponseList(It.IsAny<List<Flower>>()))
            .Returns(flowerResponseList)
            .Returns(flowerResponseList)
            .Returns(flowerResponseList);

        // A
        var shopResponsePageListResult = _shopMapper.DomainPageListToResponsePageList(shopPageList);

        // A
        Assert.Equal(shopResponsePageListResult.CurrentPage, shopPageList.CurrentPage);
        Assert.Equal(shopResponsePageListResult.PageSize, shopPageList.PageSize);
        Assert.Equal(shopResponsePageListResult.Result.Count, shopPageList.Result.Count);
        Assert.Equal(shopResponsePageListResult.TotalCount, shopPageList.TotalCount);
        Assert.Equal(shopResponsePageListResult.TotalPages, shopPageList.TotalPages);
    }
}
