using FluentValidation;
using KedaiOnline.Application.KedaiOnline.Dtos;

namespace KedaiOnline.Application.KedaiOnline.Commands.CreateKedai;

public class CreateKedaiCommandValidator : AbstractValidator<CreateKedaiCommand>
{
    private readonly List<string> validCategories = ["Makanan", "Runcit", "Minuman", "Nasi Kerabu"];
public CreateKedaiCommandValidator()
    {
        RuleFor(dto => dto.Nama)
            .Length(5, 100);

        RuleFor(dto => dto.Description)
            .NotEmpty()
            .WithMessage("Description is required.");

        RuleFor(dto => dto.Category)
            .NotEmpty()
            .WithMessage("Please choose a valid category.")
            //.Must(category => validCategories.Contains(category))
            .Must(validCategories.Contains)
            .WithMessage($"Category must be one of the following: {string.Join(", ", validCategories)}");

        //RuleFor(dto => dto.Category)
        //    .Custom((category, context) =>
        //    {
        //        if (string.IsNullOrEmpty(category) || !validCategories.Contains(category))
        //        {
        //            context.AddFailure("Category", $"Category must be one of the following: {string.Join(", ", validCategories)}");
        //        }
        //    });

        RuleFor(dto => dto.ContactEmail)
            .EmailAddress()
            .WithMessage("Invalid email format.");

        RuleFor(dto => dto.PostalCode)
            .Matches(@"^\d{2}-\d{3}$")
            .WithMessage("Please provide valid postal code (XX-XXX)");


    }
}
