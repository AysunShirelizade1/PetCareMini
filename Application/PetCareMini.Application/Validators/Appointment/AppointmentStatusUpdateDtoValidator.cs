using FluentValidation;
using PetCareMini.Application.DTOs.Appointment;

namespace PetCareMini.Application.Validators.Appointment;

public class AppointmentStatusUpdateDtoValidator : AbstractValidator<AppointmentStatusUpdateDto>
{
    public AppointmentStatusUpdateDtoValidator()
    {
        RuleFor(x => x.Status)
            .InclusiveBetween(0, 3).WithMessage("Status must be between 0 and 3. (0=Pending, 1=Approved, 2=Completed, 3=Canceled)");
    }
}