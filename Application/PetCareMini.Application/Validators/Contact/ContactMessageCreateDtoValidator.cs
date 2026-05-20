using FluentValidation;
using PetCareMini.Application.DTOs.ContactMessage;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetCareMini.Application.Validators.Contact;

public class ContactMessageCreateDtoValidator : AbstractValidator<ContactMessageCreateDto>
{
    public ContactMessageCreateDtoValidator()
    {
        RuleFor(x => x.Subject)
            .NotEmpty().WithMessage("Mövzu boş ola bilməz.")
            .MaximumLength(200).WithMessage("Mövzu maksimum 200 simvol ola bilər.");

        RuleFor(x => x.Message)
            .NotEmpty().WithMessage("Mesaj boş ola bilməz.")
            .MinimumLength(10).WithMessage("Mesaj minimum 10 simvol olmalıdır.")
            .MaximumLength(2000).WithMessage("Mesaj maksimum 2000 simvol ola bilər.");
    }
}