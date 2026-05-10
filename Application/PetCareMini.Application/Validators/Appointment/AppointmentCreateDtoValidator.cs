using FluentValidation;
using PetCareMini.Application.DTOs.Appointment;

namespace PetCareMini.Application.Validators.Appointment;

public class AppointmentCreateDtoValidator : AbstractValidator<AppointmentCreateDto>
{
    public AppointmentCreateDtoValidator()
    {
        RuleFor(x => x.PetId)
            .GreaterThan(0).WithMessage("A valid pet must be selected.");

        RuleFor(x => x.VeterinarianId)
            .GreaterThan(0).WithMessage("A valid veterinarian must be selected.");

        RuleFor(x => x.ServiceId)
            .GreaterThan(0).WithMessage("A valid service must be selected.");

        RuleFor(x => x.AppointmentDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("Appointment date must be in the future.");

        RuleFor(x => x.Notes)
            .MaximumLength(500).WithMessage("Notes must not exceed 500 characters.")
            .When(x => x.Notes != null);
    }
}