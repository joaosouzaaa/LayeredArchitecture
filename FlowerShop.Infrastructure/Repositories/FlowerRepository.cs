using FlowerShop.Domain.Entites;
using FlowerShop.Infrastructure.DatabaseContexts;
using FlowerShop.Infrastructure.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;
using FlowerShop.Business.Settings.PaginationSettings;
using FlowerShop.Infrastructure.Interfaces.Repositories;

namespace FlowerShop.Infrastructure.Repositories;
public sealed class FlowerRepository : BaseRepository<Flower>, IFlowerRepository
{
    public FlowerRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> AddAsync(Flower flower)
    {
        await DbContextSet.AddAsync(flower);

        return await SaveChangesAsync();
    }

    public Task<bool> UpdateAsync(Flower flower)
    {
        _dbContext.Entry(flower).State = EntityState.Modified;

        return SaveChangesAsync();
    }

    public Task<bool> ExistsAsync(int id) =>
        DbContextSet.AsNoTracking()
                    .AnyAsync(f => f.Id == id);

    public async Task<bool> DeleteAsync(int id)
    {
        var flower = await DbContextSet.FirstOrDefaultAsync(f => f.Id == id);

        DbContextSet.Remove(flower!);

        return await SaveChangesAsync();
    }

    public Task<Flower?> GetByIdAsync(int id, bool asNoTracking)
    {
        var query = (IQueryable<Flower>)DbContextSet;

        if (asNoTracking)
            query = DbContextSet.AsNoTracking();

        return query.FirstOrDefaultAsync(f => f.Id == id);
    }

    public Task<PageList<Flower>> GetAllPaginatedAsync(PageParameters pageParameters) =>
        DbContextSet.PaginateAsync(pageParameters);
}
