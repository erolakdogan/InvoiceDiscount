using FluentValidation;
using FluentValidation.AspNetCore;
using InvoiceDiscount.API.Validators;
using InvoiceDiscount.Application;
using InvoiceDiscount.Application.Interfaces;
using InvoiceDiscount.Application.Strategies;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers()
                        .AddFluentValidation(fv =>
                         fv.RegisterValidatorsFromAssemblyContaining<DiscountRequestValidator>());

        builder.Services.AddScoped<IDiscountCalculator, DiscountCalculator>();
        #region StrategyDI
        builder.Services.AddScoped<ICustomerDiscountStrategy, AffiliateDiscountStrategy>();
        builder.Services.AddScoped<ICustomerDiscountStrategy, EmployeeDiscountStrategy>();
        builder.Services.AddScoped<ICustomerDiscountStrategy, OldCustomerDiscountStrategy>();
        #endregion



        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.MapControllers();
        app.Run();
    }
}

