using FlowerShop.Business.Settings.ValidatorSettings;
using UnitTests.TestBuilders;

namespace UnitTests.ValidatorTests;
public sealed class FlowerValidatorTests
{
    private readonly FlowerValidator _flowerValidator;

    public FlowerValidatorTests()
    {
        _flowerValidator = new FlowerValidator();
    }

    [Fact]
    public async Task ValidateAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var flowerToValidate = FlowerBuilder.NewObject().DomainBuild();

        // A
        var validationResult = await _flowerValidator.ValidateAsync(flowerToValidate);

        // A
        Assert.True(validationResult.IsValid);
    }

    [Theory]
    [MemberData(nameof(InvalidStringWithMaxLength100Parameters))]
    public async Task ValidateAsync_InvalidName_ReturnsFalse(string name)
    {
        // A
        var flowerWithInvalidName = FlowerBuilder.NewObject().WithName(name).DomainBuild();

        // A
        var validationResult = await _flowerValidator.ValidateAsync(flowerWithInvalidName);

        // A
        Assert.False(validationResult.IsValid);
    }

    [Theory]
    [MemberData(nameof(InvalidStringWithMaxLength100Parameters))]
    public async Task ValidateAsync_InvalidColor_ReturnsFalse(string color)
    {
        // A
        var flowerWithInvalidColor = FlowerBuilder.NewObject().WithColor(color).DomainBuild();

        // A
        var validationResult = await _flowerValidator.ValidateAsync(flowerWithInvalidColor);

        // A
        Assert.False(validationResult.IsValid);
    }

    [Theory]
    [MemberData(nameof(InvalidStringWithMaxLength100Parameters))]
    public async Task ValidateAsync_InvalidSpecies_ReturnsFalse(string species)
    {
        // A
        var flowerWithInvalidSpecies = FlowerBuilder.NewObject().WithSpecies(species).DomainBuild();

        // A
        var validationResult = await _flowerValidator.ValidateAsync(flowerWithInvalidSpecies);

        // A
        Assert.False(validationResult.IsValid);
    }

    public static IEnumerable<object[]> InvalidStringWithMaxLength100Parameters()
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
}
