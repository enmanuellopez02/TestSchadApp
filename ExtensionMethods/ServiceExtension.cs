using System;
using TestSchadApp.Interfaces;
using TestSchadApp.Persistances;
using TestSchadApp.Repositories;
using TestSchadApp.Services;

namespace TestSchadApp.ExtensionMethods
{
	public static class ServiceExtension
	{
        public static void AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerTypeService, CustomerTypeService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
        }

        public static void AddDbContext(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>();
        }
    }
}

