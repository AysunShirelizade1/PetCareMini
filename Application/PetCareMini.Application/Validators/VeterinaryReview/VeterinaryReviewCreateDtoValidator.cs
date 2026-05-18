using FluentValidation;
using PetCareMini.Application.DTOs.VeterinaryReview;


namespace PetCareMini.Application.Validators.VeterinaryReview;

public class VeterinaryReviewCreateDtoValidator
    : AbstractValidator<VeterinaryReviewCreateDto>
{
    public VeterinaryReviewCreateDtoValidator()
    {
        RuleFor(x => x.VeterinarianId).GreaterThan(0);
        RuleFor(x => x.Rating).InclusiveBetween(1, 5);
        RuleFor(x => x.Comment)
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(500);
    }
}