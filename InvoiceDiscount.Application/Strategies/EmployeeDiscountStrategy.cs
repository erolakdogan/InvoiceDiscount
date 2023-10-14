using InvoiceDiscount.Application.Interfaces;
using InvoiceDiscount.Domain.Entities;
using InvoiceDiscount.Domain.Enums;


namespace InvoiceDiscount.Application.Strategies
{
    public class EmployeeDiscountStrategy : ICustomerDiscountStrategy
    {
        public bool IsMatch(Customer customer, ProductType productType)
            => customer.CustomerType == CustomerType.Employee && productType != ProductType.Groceries;

        public decimal CalculateDiscountAmount(decimal amount) => amount * 0.3m;
    }
}
