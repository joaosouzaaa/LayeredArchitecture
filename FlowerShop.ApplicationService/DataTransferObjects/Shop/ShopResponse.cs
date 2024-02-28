using FlowerShop.ApplicationService.DataTransferObjects.Flower;

namespace FlowerShop.ApplicationService.DataTransferObjects.Shop;
public sealed class ShopResponse
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Location { get; set; }
    public required string Email { get; set; }
    public required DateTime CreationDate { get; set; }

    public List<FlowerResponse> Flowers { get; set; }
}
