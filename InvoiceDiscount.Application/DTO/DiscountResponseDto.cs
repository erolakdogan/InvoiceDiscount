
namespace InvoiceDiscount.Application.DTO
{
    public class DiscountResponseDto
    {
        public decimal TotalAmount { get; set; } // İndirimsiz toplam tutar
        public decimal DiscountAmount { get; set; } // Uygulanan indirim miktarı
        public decimal FinalAmount { get; set; } // İndirim sonrası ödenecek tutar
    }
}
