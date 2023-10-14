using InvoiceDiscount.Domain.Enums;

namespace InvoiceDiscount.Domain.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public decimal Amount { get; set; }
        public decimal FinalAmount { get; set; } 
        public ProductType ProductType { get; set; } // Enum: Groceries

    }
}
