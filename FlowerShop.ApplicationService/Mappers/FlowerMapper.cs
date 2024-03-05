using FlowerShop.ApplicationService.DataTransferObjects.Flower;
using FlowerShop.ApplicationService.Interfaces.Mappers;
using FlowerShop.CrossCutting.Settings.PaginationSettings;
using FlowerShop.Domain.Entites;
using FlowerShop.Domain.Enums;

namespace FlowerShop.ApplicationService.Mappers;
public sealed class FlowerMapper : IFlowerMapper, IFlowerMapperFacade
{
    public Flower SaveToDomain(FlowerSave flowerSave) =>
        new()
        {
            Name = flowerSave.Name,
            Color = flowerSave.Color,
            Species = flowerSave.Species,
            BloomingSeason = (EBloomingSeason)flowerSave.BloomingSeason
        };

    public void UpdateToDomain(FlowerUpdate flowerUpdate, Flower flower) 
    {
        flower.Name = flowerUpdate.Name;
        flower.Color = flowerUpdate.Color;
        flower.Species = flowerUpdate.Species;
        flower.BloomingSeason = (EBloomingSeason)flowerUpdate.BloomingSeason;
    }

    public FlowerResponse DomainToResponse(Flower flower) =>
        new()
        {
            Id = flower.Id,
            Name = flower.Name,
            Color = flower.Color,
            Species = flower.Species,
            BloomingSeason = (ushort)flower.BloomingSeason
        };

    public PageList<FlowerResponse> DomainPageListToResponsePageList(PageList<Flower> flowerPageList) =>
        new()
        {
            CurrentPage = flowerPageList.CurrentPage,
            PageSize = flowerPageList.PageSize,
            Result = DomainListToResponseList(flowerPageList.Result),
            TotalCount = flowerPageList.TotalCount,
            TotalPages = flowerPageList.TotalPages
        };

    public List<FlowerResponse> DomainListToResponseList(List<Flower> flowerList) =>
        flowerList.Select(DomainToResponse).ToList();
}
