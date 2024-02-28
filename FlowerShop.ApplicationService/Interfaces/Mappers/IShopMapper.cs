using FlowerShop.ApplicationService.DataTransferObjects.Shop;
using FlowerShop.Business.Settings.PaginationSettings;
using FlowerShop.Domain.Entites;

namespace FlowerShop.ApplicationService.Interfaces.Mappers;
public interface IShopMapper
{
    Shop SaveToDomain(ShopSave shopSave);
    void UpdateToDomain(ShopUpdate shopUpdate, Shop shop);
    ShopResponse DomainToResponse(Shop shop);
    PageList<ShopResponse> DomainPageListToResponsePageList(PageList<Shop> shopPageList);
}
