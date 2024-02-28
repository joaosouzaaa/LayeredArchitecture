using FlowerShop.ApplicationService.DataTransferObjects.Shop;
using FlowerShop.ApplicationService.Interfaces.Mappers;
using FlowerShop.Business.Settings.PaginationSettings;
using FlowerShop.Domain.Entites;

namespace FlowerShop.ApplicationService.Mappers;
public sealed class ShopMapper(IFlowerMapperFacade flowerMapperFacade) : IShopMapper
{
    public Shop SaveToDomain(ShopSave shopSave) =>
        new()
        {
            Name = shopSave.Name,
            Location = shopSave.Location,
            Email = shopSave.Email,
            Flowers = [],
            CreationDate = DateTime.Now
        };

    public void UpdateToDomain(ShopUpdate shopUpdate, Shop shop)
    {
        shop.Name = shopUpdate.Name;
        shop.Location = shopUpdate.Location;
        shop.Email = shopUpdate.Email;
    }

    public ShopResponse DomainToResponse(Shop shop) =>
        new()
        {
            Id = shop.Id,
            Name = shop.Name,
            Location = shop.Location,
            Email = shop.Email,
            CreationDate = shop.CreationDate,
            Flowers = flowerMapperFacade.DomainListToResponseList(shop.Flowers)
        };

    public PageList<ShopResponse> DomainPageListToResponsePageList(PageList<Shop> shopPageList) =>
        new()
        {
            CurrentPage = shopPageList.CurrentPage,
            PageSize = shopPageList.PageSize,
            Result = shopPageList.Result.Select(DomainToResponse).ToList(),
            TotalCount = shopPageList.TotalCount,
            TotalPages = shopPageList.TotalPages
        };
}
