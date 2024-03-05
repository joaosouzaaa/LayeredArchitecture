using FlowerShop.ApplicationService.DataTransferObjects.Shop;
using FlowerShop.ApplicationService.Interfaces.Mappers;
using FlowerShop.ApplicationService.Interfaces.Services;
using FlowerShop.ApplicationService.Services.BaseServices;
using FlowerShop.Business.Extensions;
using FlowerShop.Business.Interfaces.Settings;
using FlowerShop.Business.Settings.PaginationSettings;
using FlowerShop.Domain.Entites;
using FlowerShop.Domain.Enums;
using FlowerShop.Infrastructure.Interfaces.Repositories;
using FluentValidation;

namespace FlowerShop.ApplicationService.Services;
public sealed class ShopService : BaseService<Shop>, IShopService
{
    private readonly IShopRepository _shopRepository;
    private readonly IShopMapper _shopMapper;
    private readonly IFlowerServiceFacade _flowerServiceFacade;

    public ShopService(IShopRepository shopRepository, IShopMapper shopMapper,
                       IFlowerServiceFacade flowerServiceFacade, INotificationHandler notificationHandler, 
                       IValidator<Shop> validator) 
                       : base(notificationHandler, validator)
    {
        _shopRepository = shopRepository;
        _shopMapper = shopMapper;
        _flowerServiceFacade = flowerServiceFacade;
    }

    public async Task<bool> AddAsync(ShopSave shopSave)
    {
        var shop = _shopMapper.SaveToDomain(shopSave);

        if(!await ValidateAsync(shop))
            return false;

        if(!await AddFlowersRelationshipAsync(shopSave.FlowerIds, shop.Flowers))
            return false;

        return await _shopRepository.AddAsync(shop);
    }

    public async Task<bool> UpdateAsync(ShopUpdate shopUpdate)
    {
        var shop = await _shopRepository.GetByIdAsync(shopUpdate.Id, false);

        if(shop is null)
        {
            _notificationHandler.AddNotification(nameof(EMessage.NotFound), EMessage.NotFound.Description().FormatTo("Shop"));

            return false;
        }

        _shopMapper.UpdateToDomain(shopUpdate, shop);

        if (!await ValidateAsync(shop))
            return false;

        shop.Flowers.Clear();
        if (!await AddFlowersRelationshipAsync(shopUpdate.FlowerIds, shop.Flowers))
            return false;

        return await _shopRepository.UpdateAsync(shop);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if(!await _shopRepository.ExistsAsync(id))
        {
            _notificationHandler.AddNotification(nameof(EMessage.NotFound), EMessage.NotFound.Description().FormatTo("Shop"));

            return false;
        }

        return await _shopRepository.DeleteAsync(id);
    }

    public async Task<ShopResponse?> GetByIdAsync(int id)
    {
        var shop = await _shopRepository.GetByIdAsync(id, true);

        if (shop is null)
            return null;

        return _shopMapper.DomainToResponse(shop);
    }

    public async Task<PageList<ShopResponse>> GetAllPaginatedAsync(PageParameters pageParameters)
    {
        var shopPageList = await _shopRepository.GetAllPaginatedAsync(pageParameters);

        return _shopMapper.DomainPageListToResponsePageList(shopPageList);
    }

    private async Task<bool> AddFlowersRelationshipAsync(List<int> flowerIdList, List<Flower> flowerList)
    {
        foreach (var flowerId in flowerIdList) 
        { 
            var flower = await _flowerServiceFacade.GetByIdReturnsDomainObjectAsync(flowerId);

            if(flower is null)
            {
                _notificationHandler.AddNotification(nameof(EMessage.NotFound), EMessage.NotFound.Description().FormatTo("Flower"));

                return false;
            }

            flowerList.Add(flower);
        }

        return true;
    }
}
