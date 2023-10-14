using FluentValidation;
using InvoiceDiscount.Application.DTO;

namespace InvoiceDiscount.API.Validators
{
    public class DiscountRequestValidator : AbstractValidator<DiscountRequestDto>
    {
        public DiscountRequestValidator()
        {
            RuleFor(x => x.Customer).NotEmpty().WithMessage("Müşteri boş olamaz");
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Fatura miktarı 0'dan büyük olmalıdır");
            RuleFor(x => x.ProductType).NotNull().WithMessage("Ürün tipi boş olamaz");
        }
    }
}
