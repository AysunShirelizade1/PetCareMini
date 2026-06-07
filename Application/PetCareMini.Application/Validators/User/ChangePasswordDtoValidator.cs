using FluentValidation;
using PetCareMini.Application.DTOs.User;

namespace PetCareMini.Application.Validators.User;

public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
{
    public ChangePasswordDtoValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty().WithMessage("Cari şifrə boş ola bilməz.");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("Yeni şifrə boş ola bilməz.")
            .MinimumLength(6).WithMessage("Yeni şifrə minimum 6 simvol olmalıdır.")
            .MaximumLength(50).WithMessage("Yeni şifrə 50 simvoldan çox ola bilməz.");
    }
}