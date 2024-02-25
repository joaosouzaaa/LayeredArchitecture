﻿using FlowerShop.Business.Interfaces.Repositories;
using FlowerShop.Domain.Entites;
using FlowerShop.Infrastructure.DatabaseContexts;
using FlowerShop.Infrastructure.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;
using FlowerShop.Business.Settings.PaginationSettings;

namespace FlowerShop.Infrastructure.Repositories;
public sealed class ShopRepository : BaseRepository<Shop>, IShopRepository
{
    public ShopRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> AddAsync(Shop shop)
    {
        await DbContextSet.AddAsync(shop);

        return await SaveChangesAsync();
    }

    public Task<bool> UpdateAsync(Shop shop)
    {
        _dbContext.Entry(shop).State = EntityState.Modified;

        return SaveChangesAsync();
    }

    public Task<bool> ExistsAsync(int id) =>
        DbContextSet.AsNoTracking()
                    .AnyAsync(s => s.Id == id);

    public async Task<bool> DeleteAsync(int id)
    {
        var shop = await DbContextSet.FirstOrDefaultAsync(s => s.Id == id);

        DbContextSet.Remove(shop!);

        return await SaveChangesAsync();
    }

    public Task<Shop?> GetByIdAsync(int id) =>
        DbContextSet.Include(s => s.Flowers)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.Id == id);

    public Task<PageList<Shop>> GetAllPaginatedAsync(PageParameters pageParameters) =>
        DbContextSet.Include(s => s.Flowers)
                    .PaginateAsync(pageParameters);
}
