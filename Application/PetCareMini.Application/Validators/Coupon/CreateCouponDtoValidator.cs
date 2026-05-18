using FluentValidation;
using PetCareMini.Application.DTOs.Coupon;

namespace PetCareMini.Application.Validators.Coupon;

public class CreateCouponDtoValidator : AbstractValidator<CreateCouponDto>
{
    public CreateCouponDtoValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Coupon code is required.")
            .MaximumLength(50).WithMessage("Coupon code must not exceed 50 characters.")
            .Matches("^[a-zA-Z0-9]+$").WithMessage("Coupon code can only contain letters and numbers.");

        RuleFor(x => x.DiscountPercent)
            .GreaterThan(0).WithMessage("Discount percent must be greater than 0.")
            .LessThanOrEqualTo(100).WithMessage("Discount percent cannot exceed 100.");

        RuleFor(x => x.ExpireDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("Expiry date must be in the future.");
    }
}