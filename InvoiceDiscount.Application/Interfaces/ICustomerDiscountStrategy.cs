using InvoiceDiscount.Domain.Entities;
using InvoiceDiscount.Domain.Enums;

namespace InvoiceDiscount.Application.Interfaces
{
    public interface ICustomerDiscountStrategy
    {
        bool IsMatch(Customer customer, ProductType productType);
        decimal CalculateDiscountAmount(decimal amount);
    }
}
