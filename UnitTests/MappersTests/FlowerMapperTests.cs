using FlowerShop.ApplicationService.Mappers;
using FlowerShop.CrossCutting.Settings.PaginationSettings;
using FlowerShop.Domain.Entites;
using FlowerShop.Domain.Enums;
using UnitTests.TestBuilders;

namespace UnitTests.MappersTests;
public sealed class FlowerMapperTests
{
    private readonly FlowerMapper _flowerMapper;

    public FlowerMapperTests()
    {
        _flowerMapper = new FlowerMapper();
    }

    [Fact]
    public void SaveToDomain_SuccessfulScenario_ReturnsDomainObject()
    {
        // A
        var flowerSave = FlowerBuilder.NewObject().SaveBuild();

        // A
        var flowerResult = _flowerMapper.SaveToDomain(flowerSave);

        // A
        Assert.Equal(flowerResult.Name, flowerSave.Name);
        Assert.Equal(flowerResult.Color, flowerSave.Color);
        Assert.Equal(flowerResult.Species, flowerSave.Species);
        Assert.Equal(flowerResult.BloomingSeason, (EBloomingSeason)flowerSave.BloomingSeason);
    }

    [Fact]
    public void UpdateToDomain_SuccessfulScenario_ReturnsDomainObject()
    {
        // A
        var flowerUpdate = FlowerBuilder.NewObject().UpdateBuild();
        var flowerResult = FlowerBuilder.NewObject().DomainBuild();

        // A
        _flowerMapper.UpdateToDomain(flowerUpdate, flowerResult);

        // A
        Assert.Equal(flowerResult.Name, flowerUpdate.Name);
        Assert.Equal(flowerResult.Color, flowerUpdate.Color);
        Assert.Equal(flowerResult.Species, flowerUpdate.Species);
        Assert.Equal(flowerResult.BloomingSeason, (EBloomingSeason)flowerUpdate.BloomingSeason);
    }

    [Fact]
    public void DomainToResponse_SuccessfulScenario_ReturnsResponseObject()
    {
        // A
        var flower = FlowerBuilder.NewObject().DomainBuild();

        // A
        var flowerResponseResult = _flowerMapper.DomainToResponse(flower);

        // A
        Assert.Equal(flowerResponseResult.Id, flower.Id);
        Assert.Equal(flowerResponseResult.Name, flower.Name);
        Assert.Equal(flowerResponseResult.Color, flower.Color);
        Assert.Equal(flowerResponseResult.Species, flower.Species);
        Assert.Equal(flowerResponseResult.BloomingSeason, (ushort)flower.BloomingSeason);
    }

    [Fact]
    public void DomainPageListToResponsePageList_SuccessfulScenario_ReturnsResponseObjectPageList()
    {
        // A
        var flowerList = new List<Flower>()
        {
            FlowerBuilder.NewObject().DomainBuild()
        };
        var flowerPageList = new PageList<Flower>()
        {
            CurrentPage = 123,
            PageSize = 9,
            Result = flowerList,
            TotalCount = 1,
            TotalPages = 9
        };

        // A
        var flowerResponsePageListResult = _flowerMapper.DomainPageListToResponsePageList(flowerPageList);

        // A
        Assert.Equal(flowerResponsePageListResult.CurrentPage, flowerPageList.CurrentPage);
        Assert.Equal(flowerResponsePageListResult.PageSize, flowerPageList.PageSize);
        Assert.Equal(flowerResponsePageListResult.Result.Count, flowerPageList.Result.Count);
        Assert.Equal(flowerResponsePageListResult.TotalCount, flowerPageList.TotalCount);
        Assert.Equal(flowerResponsePageListResult.TotalPages, flowerPageList.TotalPages);
    }

    [Fact]
    public void DomainListToResponseList_SuccessfulScenario_ReturnsResponseObjectList()
    {
        // A
        var flowerList = new List<Flower>()
        {
            FlowerBuilder.NewObject().DomainBuild(),
            FlowerBuilder.NewObject().DomainBuild(),
            FlowerBuilder.NewObject().DomainBuild()
        };

        // A
        var flowerResponseListResult = _flowerMapper.DomainListToResponseList(flowerList);

        // A
        Assert.Equal(flowerResponseListResult.Count, flowerList.Count);
    }
}
