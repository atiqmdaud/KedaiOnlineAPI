using FluentValidation;

namespace KedaiOnline.Application.KedaiOnline.Commands.UpdateKedai;

public class UpdateKedaiCommandValidator : AbstractValidator<UpdateKedaiCommand>
{
    public UpdateKedaiCommandValidator()
    {
        RuleFor(c => c.Nama)
            .Length(5, 100);
    }
}
