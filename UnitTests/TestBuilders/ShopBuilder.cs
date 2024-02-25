using FlowerShop.Domain.Entites;

namespace UnitTests.TestBuilders;
public sealed class ShopBuilder
{
    private readonly int _id = 10;
    private string _name = "test";
    private string _location = "location";
    private string _email = "test@email.com";
    private readonly DateTime _creationDate = DateTime.Now;
    private List<Flower> _flowerList = [];

    public static ShopBuilder NewObject() =>
        new();

    public Shop DomainBuild() =>
        new()
        {
            Id = _id,
            Name = _name,
            Location = _location,
            Email = _email,
            CreationDate = _creationDate,
            Flowers = _flowerList
        };

    public ShopBuilder WithName(string name)
    {
        _name = name;

        return this;
    }

    public ShopBuilder WithLocation(string location)
    {
        _location = location;

        return this;
    }

    public ShopBuilder WithEmail(string email)
    {
        _email = email;

        return this;
    }
}
