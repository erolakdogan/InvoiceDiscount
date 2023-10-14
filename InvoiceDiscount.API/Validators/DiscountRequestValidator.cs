using FluentValidation;
using InvoiceDiscount.Application.DTO;

namespace InvoiceDiscount.API.Validators
{
    public class DiscountRequestValidator : AbstractValidator<DiscountRequestDto>
    {
        public DiscountRequestValidator()
        {
            RuleFor(x => x.Customer).NotEmpty().WithMessage("Müşteri boş olamaz");
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Fatura miktarına  0 Tl'den büyük giriş yapılmalıdır.");
            RuleFor(x => x.ProductType).NotNull().WithMessage("Ürün tipi boş olamaz");
        }
    }
}
