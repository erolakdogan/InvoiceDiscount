using InvoiceDiscount.Application;
using InvoiceDiscount.Application.DTO;
using InvoiceDiscount.Application.Interfaces;
using InvoiceDiscount.Application.Strategies;
using InvoiceDiscount.Domain.Entities;
using InvoiceDiscount.Domain.Enums;
using Moq;

namespace InvoiceDiscount.Test
{
    public class DiscountCalculatorTests
    {
        private Mock<ICustomerDiscountStrategy> _mockStrategy;
        private IDiscountCalculator _calculator;

        public DiscountCalculatorTests()
        {
            _mockStrategy = new Mock<ICustomerDiscountStrategy>();
            var strategies = new List<ICustomerDiscountStrategy> { _mockStrategy.Object };
            _calculator = new DiscountCalculator(strategies);
        }

        //1.Madde If the user is an employee of the store, he gets a 30% discount
        [Fact]
        public void CalculateDiscount_ForEmployee_ShouldApply30PercentDiscountAndPer100Discount()
        {
            // Arrange
            var discountStrategiesMock = new Mock<IEnumerable<ICustomerDiscountStrategy>>();
            var employeeStrategyMock = new Mock<ICustomerDiscountStrategy>();

            var request = new DiscountRequestDto
            {
                Customer = new Customer
                {
                    CustomerType = CustomerType.Employee
                },
                Amount = 1000m,
                ProductType = ProductType.Electronics
            };

            employeeStrategyMock.Setup(e => e.IsMatch(It.IsAny<Customer>(), It.IsAny<ProductType>())).Returns(true);
            employeeStrategyMock.Setup(e => e.CalculateDiscountAmount(request.Amount)).Returns(request.Amount * 0.3m); // %30 indirim

            discountStrategiesMock.Setup(x => x.GetEnumerator()).Returns(new List<ICustomerDiscountStrategy> { employeeStrategyMock.Object }.GetEnumerator());

            var discountCalculator = new DiscountCalculator(discountStrategiesMock.Object);

            // Act
            var result = discountCalculator.CalculateDiscount(request);

            // Assert
            var expectedDiscount = (request.Amount * 0.3m) + 50m; // %30 indirim  + Her 100 tl üzerinde fatura için 5 tl
            var expectedFinalAmount = request.Amount - expectedDiscount;

            Assert.Equal(expectedDiscount, result.DiscountAmount);
            Assert.Equal(expectedFinalAmount, result.FinalAmount);
        }

        //2.Madde If the user is an affiliate of the store, he gets a 10% discount
        [Fact]
        public void CalculateDiscount_ForAffiliate_ShouldApply10PercentDiscount()
        {
            // Arrange
            var discountStrategiesMock = new Mock<IEnumerable<ICustomerDiscountStrategy>>();
            var affiliateStrategyMock = new Mock<ICustomerDiscountStrategy>();

            var request = new DiscountRequestDto
            {
                Customer = new Customer
                {
                    CustomerType = CustomerType.Affiliate
                },
                Amount = 1000m,
                ProductType = ProductType.Electronics
            };

            affiliateStrategyMock.Setup(a => a.IsMatch(It.IsAny<Customer>(), It.IsAny<ProductType>())).Returns(true);
            affiliateStrategyMock.Setup(a => a.CalculateDiscountAmount(request.Amount)).Returns(request.Amount * 0.1m); // %10 indirim

            discountStrategiesMock.Setup(x => x.GetEnumerator()).Returns(new List<ICustomerDiscountStrategy> { affiliateStrategyMock.Object }.GetEnumerator());

            var discountCalculator = new DiscountCalculator(discountStrategiesMock.Object);

            // Act
            var result = discountCalculator.CalculateDiscount(request);

            // Assert
            var expectedDiscount = (request.Amount * 0.1m) + 50m; // %10 indirim  + Her 100 tl üzerinde fatura için 5 tl
            var expectedFinalAmount = request.Amount - expectedDiscount;

            Assert.Equal(expectedDiscount, result.DiscountAmount);
            Assert.Equal(expectedFinalAmount, result.FinalAmount);
        }

        //3.Madde If the user has been a customer for over 2 years, he gets a 5% discount.
        [Fact]
        public void CalculateDiscount_ForOldCustomer_ShouldApply5PercentDiscountAndPer100Discount()
        {
            // Arrange
            var discountStrategiesMock = new Mock<IEnumerable<ICustomerDiscountStrategy>>();
            var oldCustomerStrategyMock = new Mock<ICustomerDiscountStrategy>();

            var request = new DiscountRequestDto
            {
                Customer = new Customer
                {
                    JoinDate = DateTime.Now.AddYears(-3) // 3 yıl önce üye olan müşteri
                },
                Amount = 1000m,
                ProductType = ProductType.Electronics
            };

            oldCustomerStrategyMock.Setup(o => o.IsMatch(It.IsAny<Customer>(), It.IsAny<ProductType>())).Returns(true);
            oldCustomerStrategyMock.Setup(o => o.CalculateDiscountAmount(request.Amount)).Returns(request.Amount * 0.05m); // %5 indirim

            discountStrategiesMock.Setup(x => x.GetEnumerator()).Returns(new List<ICustomerDiscountStrategy> { oldCustomerStrategyMock.Object }.GetEnumerator());

            var discountCalculator = new DiscountCalculator(discountStrategiesMock.Object);

            // Act
            var result = discountCalculator.CalculateDiscount(request);

            // Assert
            var expectedDiscount = (request.Amount * 0.05m) + 50m; // %5 indirim + Her 100 tl üzerinde fatura için 5 tl
            var expectedFinalAmount = request.Amount - expectedDiscount;

            Assert.Equal(expectedDiscount, result.DiscountAmount);
            Assert.Equal(expectedFinalAmount, result.FinalAmount);
        }

        //5. The percentage based discounts do not apply on groceries.
        [Fact]
        public void CalculateDiscount_ForGroceriesProductType_ShouldNotApplyPercentageDiscount()
        {
            // Arrange
            var discountStrategiesMock = new Mock<IEnumerable<ICustomerDiscountStrategy>>();
            var employeeStrategyMock = new Mock<ICustomerDiscountStrategy>();

            var request = new DiscountRequestDto
            {
                Customer = new Customer
                {
                    CustomerType = CustomerType.Employee
                },
                Amount = 1000m,
                ProductType = ProductType.Groceries
            };

            employeeStrategyMock.Setup(e => e.IsMatch(It.IsAny<Customer>(), It.IsAny<ProductType>())).Returns(false);
            discountStrategiesMock.Setup(x => x.GetEnumerator()).Returns(new List<ICustomerDiscountStrategy> { employeeStrategyMock.Object }.GetEnumerator());

            var discountCalculator = new DiscountCalculator(discountStrategiesMock.Object);

            // Act
            var result = discountCalculator.CalculateDiscount(request);

            // Assert
            var expectedDiscount = 50m;  // Her 100 tl üzerinde fatura için 5 tl 
            var expectedFinalAmount = request.Amount - expectedDiscount;

            Assert.Equal(expectedDiscount, result.DiscountAmount);
            Assert.Equal(expectedFinalAmount, result.FinalAmount);
        }

        //6. A user can get only one of the percentage based discounts on a bill.
        [Fact]
        public void CalculateDiscount_WhenCustomerMatchesMultipleStrategies_ShouldApplyOnlyOnePercentageDiscount()
        {
            var mockEmployeeStrategy = new Mock<ICustomerDiscountStrategy>();
            mockEmployeeStrategy.Setup(x => x.IsMatch(It.IsAny<Customer>(), It.IsAny<ProductType>())).Returns(true);
            mockEmployeeStrategy.Setup(x => x.CalculateDiscountAmount(It.IsAny<decimal>())).Returns(300m);

            var mockAffiliateStrategy = new Mock<ICustomerDiscountStrategy>();
            mockAffiliateStrategy.Setup(x => x.IsMatch(It.IsAny<Customer>(), It.IsAny<ProductType>())).Returns(true);
            mockAffiliateStrategy.Setup(x => x.CalculateDiscountAmount(It.IsAny<decimal>())).Returns(100m);

            var discountStrategies = new List<ICustomerDiscountStrategy> { mockEmployeeStrategy.Object, mockAffiliateStrategy.Object };

            var request = new DiscountRequestDto
            {
                Customer = new Customer
                {
                    CustomerType = CustomerType.Employee 
                },
                Amount = 1000m,
                ProductType = ProductType.Electronics
            };

            var discountCalculator = new DiscountCalculator(discountStrategies);

            var result = discountCalculator.CalculateDiscount(request);

            mockEmployeeStrategy.Verify(x => x.CalculateDiscountAmount(It.IsAny<decimal>()), Times.Once());
            mockAffiliateStrategy.Verify(x => x.CalculateDiscountAmount(It.IsAny<decimal>()), Times.Never());
        }
    }
}
