using FlowerShop.ApplicationService.DataTransferObjects.Flower;
using FlowerShop.Business.Settings.PaginationSettings;
using FlowerShop.Domain.Entites;

namespace FlowerShop.ApplicationService.Interfaces.Mappers;
public interface IFlowerMapper
{
    Flower SaveToDomain(FlowerSave flowerSave);
    void UpdateToDomain(FlowerUpdate flowerUpdate, Flower flower);
    FlowerResponse DomainToResponse(Flower flower);
    PageList<FlowerResponse> DomainPageListToResponsePageList(PageList<Flower> flowerPageList);
}
