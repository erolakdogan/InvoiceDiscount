using InvoiceDiscount.Application.Interfaces;
using InvoiceDiscount.Domain.Entities;
using InvoiceDiscount.Domain.Enums;


namespace InvoiceDiscount.Application.Strategies
{
    public class AffiliateDiscountStrategy : ICustomerDiscountStrategy
    {
        public bool IsMatch(Customer customer, ProductType productType)
            => customer.CustomerType == CustomerType.Affiliate && productType != ProductType.Groceries;

        public decimal CalculateDiscountAmount(decimal amount) => amount * 0.1m;  // 10% discount
    }
}
