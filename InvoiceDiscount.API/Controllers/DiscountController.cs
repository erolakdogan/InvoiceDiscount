using InvoiceDiscount.Application.DTO;
using InvoiceDiscount.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceDiscount.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountCalculator _discountCalculator;

        public DiscountController(IDiscountCalculator discountCalculator)
        {
            _discountCalculator = discountCalculator;
        }

        [HttpPost("calculate")]
        public async Task<IActionResult> CalculateDiscount([FromBody] DiscountRequestDto request)
        {
            var discount = await Task.Run(() => _discountCalculator.CalculateDiscount(request));
            return Ok(discount);
        }
    }
}
