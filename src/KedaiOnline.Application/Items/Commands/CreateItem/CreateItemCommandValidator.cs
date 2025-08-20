using FluentValidation;

namespace KedaiOnline.Application.Items.Commands.CreateItem;

public class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
{
    public CreateItemCommandValidator()
    {
         RuleFor(item=>item.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price must be greater than zero.");
    }
}
