using FlowerShop.ApplicationService.DataTransferObjects.Flower;
using FlowerShop.Business.Settings.PaginationSettings;

namespace FlowerShop.ApplicationService.Interfaces.Services;
public interface IFlowerService
{
    Task<bool> AddAsync(FlowerSave flowerSave);
    Task<bool> UpdateAsync(FlowerUpdate flowerUpdate);
    Task<bool> DeleteAsync(int id);
    Task<FlowerResponse?> GetByIdAsync(int id);
    Task<PageList<FlowerResponse>> GetAllPaginatedAsync(PageParameters pageParameters);
}
