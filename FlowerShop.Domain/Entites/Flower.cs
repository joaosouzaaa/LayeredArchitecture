using FlowerShop.Domain.Enums;

namespace FlowerShop.Domain.Entites;
public sealed class Flower
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Color { get; set; }
    public required string Species { get; set; }
    public required EBloomingSeason BloomingSeason { get; set; }

    public List<Shop> Shops { get; set; }
}
