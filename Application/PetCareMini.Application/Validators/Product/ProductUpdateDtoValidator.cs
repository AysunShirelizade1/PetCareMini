using FluentValidation;
using PetCareMini.Application.DTOs.Product;

namespace PetCareMini.Application.Validators.Product;

public class ProductUpdateDtoValidator : AbstractValidator<ProductUpdateDto>
{
    public ProductUpdateDtoValidator()
    {
        RuleFor(x => x.NameAz)
            .NotEmpty().WithMessage("Azerbaijani name is required.")
            .MaximumLength(200).WithMessage("Azerbaijani name must not exceed 200 characters.");

        RuleFor(x => x.NameEn)
            .NotEmpty().WithMessage("English name is required.")
            .MaximumLength(200).WithMessage("English name must not exceed 200 characters.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(x => x.StockQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Stock quantity cannot be negative.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("A valid category must be selected.");

        RuleFor(x => x.ImageUrl)
            .MaximumLength(500).WithMessage("Image URL must not exceed 500 characters.")
            .Must(url => url == null || url.StartsWith("http://") || url.StartsWith("https://"))
            .WithMessage("Image URL must be a valid URL starting with http:// or https://.")
            .When(x => x.ImageUrl != null);
    }
}