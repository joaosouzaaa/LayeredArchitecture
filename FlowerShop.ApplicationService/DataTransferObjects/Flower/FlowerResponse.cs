namespace FlowerShop.ApplicationService.DataTransferObjects.Flower;
public sealed class FlowerResponse
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Color { get; set; }
    public required string Species { get; set; }
    public required ushort BloomingSeason { get; set; }
}
