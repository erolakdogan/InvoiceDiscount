using InvoiceDiscount.Application.Interfaces;
using InvoiceDiscount.Domain.Entities;
using InvoiceDiscount.Domain.Enums;


namespace InvoiceDiscount.Application.Strategies
{
    public class OldCustomerDiscountStrategy : ICustomerDiscountStrategy
    {
        public bool IsMatch(Customer customer, ProductType productType)
            => (DateTime.Now - customer.JoinDate).TotalDays > (2 * 365) && productType != ProductType.Groceries;

        public decimal CalculateDiscountAmount(decimal amount) => amount * 0.05m;  // 5% discount
    }
}
