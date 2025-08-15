using FluentValidation;
using KedaiOnline.Application.KedaiOnline.Dtos;
using Microsoft.IdentityModel.Tokens;

namespace KedaiOnline.Application.KedaiOnline.Queries.GetAllKedaiOnline;

public class GetAllKedaiOnlineQueryValidator : AbstractValidator<GetAllKedaiOnlineQuery>
{
    private int[] allowPageSizes = [5,10,15,20];
    private string[] allowedSortByColumnNames = [nameof(KedaiDto.Nama),
    nameof(KedaiDto.Description),
    nameof(KedaiDto.Category)];

    public GetAllKedaiOnlineQueryValidator()
    {
        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Page number must be greater than or equal to 1.");

        RuleFor(r => r.PageSize)
            .Must(size => allowPageSizes.Contains(size))
            .WithMessage($"Page size must be one of the following values: {string.Join(", ", allowPageSizes)}.");

        RuleFor(r => r.SortBy)
         .Must(value => allowedSortByColumnNames.Contains(value))
         .When(q => q.SortBy != null)
         .WithMessage($"SortBy is optional, or must be in [{string.Join(", ", allowedSortByColumnNames)}].");
    }
}
