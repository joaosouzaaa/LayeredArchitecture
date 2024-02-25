using FlowerShop.Business.Settings.ValidatorSettings;
using UnitTests.TestBuilders;

namespace UnitTests.ValidatorTests;
public sealed class ShopValidatorTests
{
    private readonly ShopValidator _shopValidator;

    public ShopValidatorTests()
    {
        _shopValidator = new ShopValidator();
    }

    [Fact]
    public async Task ValidateAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var shopToValidate = ShopBuilder.NewObject().DomainBuild();

        // A
        var validationResult = await _shopValidator.ValidateAsync(shopToValidate);

        // A
        Assert.True(validationResult.IsValid);
    }

    [Theory]
    [MemberData(nameof(InvalidNameParameters))]
    public async Task ValidateAsync_InvalidName_ReturnsFalse(string name)
    {
        // A
        var shopWithInvalidName = ShopBuilder.NewObject().WithName(name).DomainBuild();

        // A
        var validationResult = await _shopValidator.ValidateAsync(shopWithInvalidName);

        // A
        Assert.False(validationResult.IsValid);
    }

    public static IEnumerable<object[]> InvalidNameParameters()
    {
        yield return new object[]
        {
            ""
        };

        yield return new object[]
        {
            "a"
        };

        yield return new object[]
        {
            new string('a', 103)
        };
    }

    [Theory]
    [MemberData(nameof(InvalidLocationParameters))]
    public async Task ValidateAsync_InvalidLocation_ReturnsFalse(string location)
    {
        // A
        var shopWithInvalidLocation = ShopBuilder.NewObject().WithLocation(location).DomainBuild();

        // A
        var validationResult = await _shopValidator.ValidateAsync(shopWithInvalidLocation);

        // A
        Assert.False(validationResult.IsValid);
    }

    public static IEnumerable<object[]> InvalidLocationParameters()
    {
        yield return new object[]
        {
            ""
        };

        yield return new object[]
        {
            "a"
        };

        yield return new object[]
        {
            new string('a', 203)
        };
    }

    [Theory]
    [MemberData(nameof(InvalidEmailParameters))]
    public async Task ValidateAsync_InvalidEmail_ReturnsFalse(string email)
    {
        // A
        var shopWithInvalidEmail = ShopBuilder.NewObject().WithEmail(email).DomainBuild();

        // A
        var validationResult = await _shopValidator.ValidateAsync(shopWithInvalidEmail);

        // A
        Assert.False(validationResult.IsValid);
    }

    public static IEnumerable<object[]> InvalidEmailParameters()
    {
        yield return new object[]
        {
            ""
        };

        yield return new object[]
        {
            "a"
        };

        yield return new object[]
        {
            new string('a', 153)
        };

        yield return new object[]
        {
            "invalid"
        };

        yield return new object[]
        {
            "invalid.com"
        };

        yield return new object[]
        {
            "test@"
        };

        yield return new object[]
        {
            $"test@{new string('a', 160)}.com"
        };
    }
}
