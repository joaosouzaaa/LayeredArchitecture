using FlowerShop.CrossCutting.Extensions;
using FlowerShop.Domain.Entites;
using FlowerShop.Domain.Enums;
using FluentValidation;

namespace FlowerShop.CrossCutting.Settings.ValidatorSettings;
public sealed class ShopValidator : AbstractValidator<Shop>
{
    public ShopValidator()
    {
        RuleFor(s => s.Name).NotEmpty().Length(3, 100)
            .WithMessage(EMessage.InvalidLength.Description().FormatTo("Name", "3 to 100"));

        RuleFor(s => s.Location).NotEmpty().Length(3, 200)
            .WithMessage(EMessage.InvalidLength.Description().FormatTo("Location", "3 to 200"));

        RuleFor(s => s.Email).EmailAddress().Length(3, 150)
            .WithMessage(EMessage.InvalidFormat.Description().FormatTo("Email"));
    }
}
