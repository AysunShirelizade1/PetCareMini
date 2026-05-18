using FluentValidation;
using PetCareMini.Application.DTOs.Review;

namespace PetCareMini.Application.Validators.Review;

public class ReviewCreateDtoValidator : AbstractValidator<ReviewCreateDto>
{
    public ReviewCreateDtoValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("A valid product must be selected.");

        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");

        RuleFor(x => x.Comment)
            .NotEmpty().WithMessage("Comment is required.")
            .MinimumLength(3).WithMessage("Comment must be at least 3 characters.")
            .MaximumLength(1000).WithMessage("Comment must not exceed 1000 characters.");
    }
}