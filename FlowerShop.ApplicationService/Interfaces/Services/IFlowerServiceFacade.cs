using FlowerShop.Domain.Entites;

namespace FlowerShop.ApplicationService.Interfaces.Services;
public interface IFlowerServiceFacade
{
    Task<Flower?> GetByIdReturnsDomainObjectAsync(int id);
}
