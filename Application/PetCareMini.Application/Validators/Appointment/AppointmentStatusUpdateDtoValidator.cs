using FluentValidation;
using PetCareMini.Application.DTOs.Appointment;

namespace PetCareMini.Application.Validators.Appointment;

public class AppointmentStatusUpdateDtoValidator : AbstractValidator<AppointmentStatusUpdateDto>
{
    public AppointmentStatusUpdateDtoValidator()
    {
        RuleFor(x => x.Status)
            .InclusiveBetween(1, 4).WithMessage("Status must be between 1 and 4. (1=Pending, 2=Approved, 3=Completed, 4=Canceled)");
    }
}