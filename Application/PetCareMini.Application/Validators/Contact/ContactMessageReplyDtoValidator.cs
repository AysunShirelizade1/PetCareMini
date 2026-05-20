using FluentValidation;
using PetCareMini.Application.DTOs.ContactMessage;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetCareMini.Application.Validators.Contact;

public class ContactMessageReplyDtoValidator : AbstractValidator<ContactMessageReplyDto>
{
    public ContactMessageReplyDtoValidator()
    {
        RuleFor(x => x.ReplyMessage)
            .NotEmpty().WithMessage("Cavab boş ola bilməz.")
            .MinimumLength(5).WithMessage("Cavab minimum 5 simvol olmalıdır.")
            .MaximumLength(2000).WithMessage("Cavab maksimum 2000 simvol ola bilər.");
    }
}