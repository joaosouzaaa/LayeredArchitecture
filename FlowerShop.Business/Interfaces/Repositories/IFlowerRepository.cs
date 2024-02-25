using FlowerShop.Business.Settings.PaginationSettings;
using FlowerShop.Domain.Entites;

namespace FlowerShop.Business.Interfaces.Repositories;
public interface IFlowerRepository
{
    Task<bool> AddAsync(Flower flower);
    Task<bool> UpdateAsync(Flower flower);
    Task<bool> ExistsAsync(int id);
    Task<bool> DeleteAsync(int id);
    Task<Flower?> GetByIdAsync(int id);
    Task<PageList<Flower>> GetAllPaginatedAsync(PageParameters pageParameters);
}
