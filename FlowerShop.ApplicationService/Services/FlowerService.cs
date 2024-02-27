using FlowerShop.ApplicationService.DataTransferObjects.Flower;
using FlowerShop.ApplicationService.Interfaces.Mappers;
using FlowerShop.ApplicationService.Interfaces.Services;
using FlowerShop.ApplicationService.Services.BaseServices;
using FlowerShop.Business.Extensions;
using FlowerShop.Business.Interfaces.Repositories;
using FlowerShop.Business.Interfaces.Settings;
using FlowerShop.Business.Settings.PaginationSettings;
using FlowerShop.Domain.Entites;
using FlowerShop.Domain.Enums;
using FluentValidation;

namespace FlowerShop.ApplicationService.Services;
public sealed class FlowerService : BaseService<Flower>, IFlowerService
{
    private readonly IFlowerRepository _flowerRepository;
    private readonly IFlowerMapper _flowerMapper;

    public FlowerService(IFlowerRepository flowerRepository, IFlowerMapper flowerMapper,
                         INotificationHandler notificationHandler, IValidator<Flower> validator)
                         : base(notificationHandler, validator)
    {
        _flowerRepository = flowerRepository;
        _flowerMapper = flowerMapper;
    }

    public async Task<bool> AddAsync(FlowerSave flowerSave)
    {
        var flower = _flowerMapper.SaveToDomain(flowerSave);

        if (!await ValidateAsync(flower))
            return false;

        return await _flowerRepository.AddAsync(flower);
    }

    public async Task<bool> UpdateAsync(FlowerUpdate flowerUpdate)
    {
        var flower = await _flowerRepository.GetByIdAsync(flowerUpdate.Id, false);

        if (flower is null)
        {
            _notificationHandler.AddNotification(nameof(EMessage.NotFound), EMessage.NotFound.Description().FormatTo("Flower"));

            return false;
        }

        _flowerMapper.UpdateToDomain(flowerUpdate, flower);

        if (!await ValidateAsync(flower))
            return false;

        return await _flowerRepository.UpdateAsync(flower);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (!await _flowerRepository.ExistsAsync(id))
        {
            _notificationHandler.AddNotification(nameof(EMessage.NotFound), EMessage.NotFound.Description().FormatTo("Flower"));

            return false;
        }

        return await _flowerRepository.DeleteAsync(id);
    }

    public async Task<FlowerResponse?> GetByIdAsync(int id)
    {
        var flower = await _flowerRepository.GetByIdAsync(id, true);

        if (flower is null)
            return null;

        return _flowerMapper.DomainToResponse(flower);
    }

    public async Task<PageList<FlowerResponse>> GetAllPaginatedAsync(PageParameters pageParameters)
    {
        var flowerPageList = await _flowerRepository.GetAllPaginatedAsync(pageParameters);

        return _flowerMapper.DomainPageListToResponsePageList(flowerPageList);
    }
}
