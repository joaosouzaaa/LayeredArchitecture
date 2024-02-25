using FlowerShop.Domain.Entites;
using FlowerShop.Domain.Enums;

namespace UnitTests.TestBuilders;
public sealed class FlowerBuilder
{
    private readonly int _id = 123;
    private string _name = "test";
    private string _color = "test";
    private string _species = "flower";
    private readonly EBloomingSeason _bloomingSeason = EBloomingSeason.Winter;
    private readonly int _shopId = 123;

    public static FlowerBuilder NewObject() =>
        new();

    public Flower DomainBuild() =>
        new()
        {
            Id = _id,
            Name = _name,
            Color = _color,
            Species = _species,
            BloomingSeason = _bloomingSeason,
            ShopId = _shopId
        };

    public FlowerBuilder WithName(string name)
    {
        _name = name;

        return this;
    }

    public FlowerBuilder WithColor(string color)
    {
        _color = color;

        return this;
    }

    public FlowerBuilder WithSpecies(string species)
    {
        _species = species;

        return this;
    }
}
