namespace FlowerShop.Domain.Entites;
public sealed class FlowerShop
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Location { get; set; }
    public required string Email { get; set; }
    public required DateTime CreationDate { get; set; }

    public List<Flower> Flowers { get; set; }
}
