using InvoiceDiscount.Application.DTO;
using InvoiceDiscount.Application.Interfaces;


namespace InvoiceDiscount.Application
{
    public class DiscountCalculator : IDiscountCalculator
    {
        private readonly IEnumerable<ICustomerDiscountStrategy> _discountStrategies;
        public DiscountCalculator(IEnumerable<ICustomerDiscountStrategy> discountStrategies)
        {
            _discountStrategies = discountStrategies ?? throw new ArgumentNullException(nameof(discountStrategies));
        }

        public DiscountResponseDto CalculateDiscount(DiscountRequestDto request)
        {
            var applicableStrategy = _discountStrategies.FirstOrDefault(s => s.IsMatch(request.Customer, request.ProductType));
            var percentageDiscountAmount = applicableStrategy?.CalculateDiscountAmount(request.Amount) ?? 0m;
            var perHundredDiscountAmount = CalculatePerHundredDiscount(request.Amount);

            var totalDiscount = percentageDiscountAmount + perHundredDiscountAmount;
            var finalAmount = request.Amount - totalDiscount;

            return new DiscountResponseDto
            {
                TotalAmount = request.Amount,
                DiscountAmount = totalDiscount,
                FinalAmount = finalAmount
            };
        }

        private decimal CalculatePerHundredDiscount(decimal amount)
        {
            int discountMultiplier = (int)(amount / 100);
            return discountMultiplier * 5;
        }
    }
}
