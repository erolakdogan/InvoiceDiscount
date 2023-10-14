using InvoiceDiscount.Application.DTO;
using InvoiceDiscount.Domain.Entities;


namespace InvoiceDiscount.Application.Interfaces
{
    public interface IDiscountCalculator
    {
        DiscountResponseDto CalculateDiscount(DiscountRequestDto request);
    }
}
