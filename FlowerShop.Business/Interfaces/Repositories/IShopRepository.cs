using FlowerShop.Business.Settings.PaginationSettings;
using FlowerShop.Domain.Entites;

namespace FlowerShop.Business.Interfaces.Repositories;
public interface IShopRepository
{
    Task<bool> AddAsync(Shop shop);
    Task<bool> UpdateAsync(Shop shop);
    Task<bool> ExistsAsync(int id);
    Task<bool> DeleteAsync(int id);
    Task<Shop?> GetByIdAsync(int id);
    Task<PageList<Shop>> GetAllPaginatedAsync(PageParameters pageParameters);
}
