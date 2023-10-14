
using InvoiceDiscount.Domain.Entities;
using InvoiceDiscount.Domain.Enums;

namespace InvoiceDiscount.Application.DTO
{
    public class DiscountRequestDto
    {
        public Customer Customer { get; set; }
        public decimal Amount { get; set; }
        public ProductType ProductType { get; set; }
    }
}
