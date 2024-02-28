using FlowerShop.ApplicationService.DataTransferObjects.Shop;
using FlowerShop.Business.Settings.PaginationSettings;

namespace FlowerShop.ApplicationService.Interfaces.Services;
public interface IShopService
{
    Task<bool> AddAsync(ShopSave shopSave);
    Task<bool> UpdateAsync(ShopUpdate shopUpdate);
    Task<bool> DeleteAsync(int id);
    Task<ShopResponse?> GetByIdAsync(int id);
    Task<PageList<ShopResponse>> GetAllPaginatedAsync(PageParameters pageParameters);
}
