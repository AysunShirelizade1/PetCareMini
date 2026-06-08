using FluentValidation;
using PetCareMini.Application.DTOs.User;

namespace PetCareMini.Application.Validators.User;

public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
{
    public UserUpdateDtoValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Ad boş ola bilməz.")
            .MaximumLength(100).WithMessage("Ad 100 simvoldan çox ola bilməz.");

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+?[0-9]{7,15}$").WithMessage("Telefon nömrəsi düzgün deyil.")
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));
    }
}