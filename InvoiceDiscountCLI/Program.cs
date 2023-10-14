using InvoiceDiscount.Application.DTO;
using InvoiceDiscount.Domain.Entities;
using InvoiceDiscount.Domain.Enums;
using Newtonsoft.Json;
using System.Text;

namespace InvoiceDiscount.CLI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Fatura İndirim Hesaplama Uygulaması V1");

            bool continueCalculating = true;

            while (continueCalculating)
            {
                var customerTypeInput = GetInput("Müşteri tipi (Regular, Employee, Affiliate):");
                var productTypeInput = GetInput("Ürün tipi (Groceries, Electronics, Clothing):");
                var amountInput = GetInput("Tutar:");

                if (Enum.TryParse(customerTypeInput, true, out CustomerType customerType) &&
                    Enum.TryParse(productTypeInput, true, out ProductType productType) &&
                    decimal.TryParse(amountInput, out decimal amount))
                {
                    var discountRequest = new DiscountRequestDto
                    {
                        Customer = new Customer { CustomerType = customerType },
                        ProductType = productType,
                        Amount = amount
                    };

                    var response = await CalculateDiscount(discountRequest);

                    Console.WriteLine($"Toplam Tutar: {response.TotalAmount}");
                    Console.WriteLine($"Uygulanan İndirim: {response.DiscountAmount}");
                    Console.WriteLine($"Son Tutar: {response.FinalAmount}");
                }
                else
                {
                    Console.WriteLine("Yanlış giriş yaptınız. Lütfen tekrar deneyin.");
                }

                Console.WriteLine("Başka bir sorgulama yapmak istiyor musunuz? (E/H)");
                var userInput = Console.ReadLine().ToLower();
                continueCalculating = userInput == "e";
            }
        }

        private static string GetInput(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }

        private static async Task<DiscountResponseDto> CalculateDiscount(DiscountRequestDto request)
        {
            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                var httpResponse = await httpClient.PostAsync("https://localhost:44300/api/Discount/calculate", content);

                httpResponse.EnsureSuccessStatusCode();

                var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<DiscountResponseDto>(jsonResponse);
            }
        }
    }
}
