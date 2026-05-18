using FluentValidation;
using PetCareMini.Application.DTOs.Pet;

namespace PetCareMini.Application.Validators.Pet;

public class PetCreateDtoValidator : AbstractValidator<PetCreateDto>
{
    public PetCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Pet name is required.")
            .MaximumLength(100).WithMessage("Pet name must not exceed 100 characters.");

        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Pet type is required.")
            .MaximumLength(50).WithMessage("Pet type must not exceed 50 characters.");

        RuleFor(x => x.Age)
            .GreaterThanOrEqualTo(0).WithMessage("Age cannot be negative.")
            .LessThanOrEqualTo(100).WithMessage("Age seems invalid.")
            .When(x => x.Age.HasValue);

        RuleFor(x => x.Weight)
            .GreaterThan(0).WithMessage("Weight must be greater than 0.")
            .LessThanOrEqualTo(500).WithMessage("Weight seems invalid.")
            .When(x => x.Weight.HasValue);

        RuleFor(x => x.Gender)
            .Must(g => g == null || g == "Male" || g == "Female")
            .WithMessage("Gender must be 'Male' or 'Female'.");

        RuleFor(x => x.Breed)
            .MaximumLength(100).WithMessage("Breed must not exceed 100 characters.")
            .When(x => x.Breed != null);

        RuleFor(x => x.Notes)
            .MaximumLength(500).WithMessage("Notes must not exceed 500 characters.")
            .When(x => x.Notes != null);
    }
}