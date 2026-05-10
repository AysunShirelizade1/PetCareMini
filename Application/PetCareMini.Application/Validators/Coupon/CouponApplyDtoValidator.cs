using FluentValidation;
using PetCareMini.Application.DTOs.Coupon;

namespace PetCareMini.Application.Validators.Coupon;

public class CouponApplyDtoValidator : AbstractValidator<CouponApplyDto>
{
    public CouponApplyDtoValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Coupon code is required.")
            .MaximumLength(50).WithMessage("Coupon code must not exceed 50 characters.")
            .Matches("^[a-zA-Z0-9]+$").WithMessage("Coupon code can only contain letters and numbers.");
    }
}