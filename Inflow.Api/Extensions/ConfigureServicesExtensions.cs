using DiyorMarket.Infrastructure.Persistence.Repositories;
using DiyorMarket.Services;
using Inflow.Domain.Intefaces.Services;
using Inflow.Domain.Interfaces.Repositories;
using Inflow.Domain.Interfaces.Services;
using Inflow.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Inflow.Api.Extensions
{
    public static class ConfigureServicesExtensions
    {

        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICommonRepository, CommonRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<ISaleItemRepository, SaleItemRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<ISupplyRepository, SupplyRepository>();
            services.AddScoped<ISupplyItemRepository, SupplyItemRepository>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ISaleService, SaleService>();
            services.AddScoped<ISaleItemService, SaleItemService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<ISupplyService, SupplyService>();
            services.AddScoped<ISupplyItemService, SupplyItemService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddSingleton<IEmailSender, EmailSender>();

            services.AddControllers()
              .AddNewtonsoftJson(options =>
                  options.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore
               );

            return services;
        }

        public static IServiceCollection ConfigureLogger(this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File("logs/logs.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.File("logs/error_.txt", Serilog.Events.LogEventLevel.Error, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            return services;
        }

        public static IServiceCollection ConfigureDatabaseContext(this IServiceCollection services)
        {
            var builder = WebApplication.CreateBuilder();

            services.AddDbContext<InflowDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DiyorMarketConection")));

            return services;
        }
    }
}

