using FlowerShop.ApplicationService.DataTransferObjects.Flower;
using FlowerShop.Domain.Entites;

namespace FlowerShop.ApplicationService.Interfaces.Mappers;
public interface IFlowerMapperFacade
{
    List<FlowerResponse> DomainListToResponseList(List<Flower> flowerList);
}
