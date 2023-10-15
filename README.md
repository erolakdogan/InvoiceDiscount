# InvoiceDiscount

InvoiceDiscount, .NET Core 7 ile geliştirilen, faturada müşteri indirimlerini customer olarak hesaplamak için oluşturulan bir projedir. Bu projede SOLID prensiplerine dikkat edilmiştir. Ayrıca FluentValidation kütüphanesi kullanılmış ve Moq ile birlikte birim testleri gerçekleştirilmiştir.Projede herhangi bir database kullanılmamıştır.Ancak istenildiğinde genişletilebilir.Ayrıca proje isteklerinde talep edilen indirim türleride çeşitlendirilmesi ve implement edilmesi basit bir şekilde yapılabilir.

## Mimari
Projede kullanılan mimari CLEAN Architecture uygulanıp. Müşteri tipine göre indirim hesaplama stratejileri Strateji Tasarım Desenine (Strategy Design Pattern) ile implemente edilmiştir.

## Projede Bulunan Ana Bileşenler:
InvoiceDiscount.API (Rest Servis Katmanı)
InvoiceDiscount.Application:(Interface- DTO - Strategies bulunduğu katman)
InvoiceDiscount.Domain(Entites Katmanı)
InvoiceDiscount.Test(Unıt Testlerimiz)
InvoiceDiscount.CLI: Kullanıcı girişi ve indirim hesaplamaları için konsol uygulaması bu bölümde bulunmaktadır. Kullanıcı girişi yapıldıktan sonra, indirim hesaplamaları için API ile iletişime geçilir.

### Gereksinimler:
- .NET Core 7
- Bir IDE (Visual Studio, Rider veya Visual Studio Code tavsiye edilir)

## Kurulum:

1. **Repo'yu Klonlama**:
   - Projeyi yerel makinanıza klonlayın:
     ```bash
     git clone https://github.com/erolakdogan/InvoiceDiscount.git
     ```

2. **Bağımlılıkları Yükleme**:
   - Proje dizinine gidin ve bağımlılıkları yükleyin:
     ```bash
     cd InvoiceDiscount
     dotnet restore
     ```

4. **API'yi Çalıştırma**:
   - API projenizin bulunduğu dizine gidin:
     ```bash
     cd InvoiceDiscount.API
     dotnet run
     ```

5. **CLI Uygulamasını Çalıştırma**:
   - CLI projenizin bulunduğu dizine gidin:
     ```bash
     cd InvoiceDiscount.CLI
     dotnet run
     ```

6. **Testlerin Çalıştırılması**:
   - Test projenizin bulunduğu dizinde, testleri çalıştırmak için:
     ```bash
     cd InvoiceDiscount.Test
     dotnet test
     ```
     
## UML Diyagram
![UML Diyagram](https://github.com/erolakdogan/InvoiceDiscount/blob/master/InvoiceDiscountDiagram.png)

[![.NET Build and Test](https://github.com/erolakdogan/InvoiceDiscount/workflows/.NET/badge.svg)](https://github.com/erolakdogan/InvoiceDiscount/actions?query=workflow%3A.NET)


