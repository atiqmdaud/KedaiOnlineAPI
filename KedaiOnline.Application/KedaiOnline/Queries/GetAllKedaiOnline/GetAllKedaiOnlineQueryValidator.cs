using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace KedaiOnline.Application.KedaiOnline.Queries.GetAllKedaiOnline;

public class GetAllKedaiOnlineQueryValidator : AbstractValidator<GetAllKedaiOnlineQuery>
{
    private int[] allowPageSizes = [5,10,15,20];
    public GetAllKedaiOnlineQueryValidator()
    {
        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Page number must be greater than or equal to 1.");

        RuleFor(r => r.PageSize)
            .Must(size => allowPageSizes.Contains(size))
            .WithMessage($"Page size must be one of the following values: {string.Join(", ", allowPageSizes)}.");

    }
}
