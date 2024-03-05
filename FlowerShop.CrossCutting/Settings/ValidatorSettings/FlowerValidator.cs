using FlowerShop.CrossCutting.Extensions;
using FlowerShop.Domain.Entites;
using FlowerShop.Domain.Enums;
using FluentValidation;

namespace FlowerShop.CrossCutting.Settings.ValidatorSettings;
public sealed class FlowerValidator : AbstractValidator<Flower>
{
    public FlowerValidator()
    {
        RuleFor(f => f.Name).NotEmpty().Length(3, 100)
            .WithMessage(EMessage.InvalidLength.Description().FormatTo("Name", "3 to 100"));

        RuleFor(f => f.Color).NotEmpty().Length(3, 100)
            .WithMessage(EMessage.InvalidLength.Description().FormatTo("Color", "3 to 100"));
        
        RuleFor(f => f.Species).NotEmpty().Length(3, 100)
            .WithMessage(EMessage.InvalidLength.Description().FormatTo("Species", "3 to 100"));
    }
}
