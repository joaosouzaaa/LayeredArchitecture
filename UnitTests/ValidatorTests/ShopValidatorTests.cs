﻿using FlowerShop.CrossCutting.Settings.ValidatorSettings;
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

    public static TheoryData<string> InvalidNameParameters() =>
        new()
        {
            "",
            "a",
            new string('a', 103)
        };

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

    public static TheoryData<string> InvalidLocationParameters() =>
        new()
        {
            "",
            "a",
            new string('a', 203)
        };

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

    public static TheoryData<string> InvalidEmailParameters() =>
        new()
        {
            "",
            "a",
             new string('a', 153),
             "invalid",
             "invalid.com",
             "test@",
             $"test@{new string('a', 160)}.com"
        };
}
