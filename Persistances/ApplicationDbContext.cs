using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestSchadApp.Configurations;
using TestSchadApp.Models;

namespace TestSchadApp.Persistances
{
	public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<CustomerType> CustomerTypes => Set<CustomerType>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Invoice> Invoices => Set<Invoice>();
        public DbSet<InvoiceDetail> InvoiceDetails => Set<InvoiceDetail>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            new CustomerTypeEntityTypeConfiguration().Configure(builder.Entity<CustomerType>());
            new CustomerEntityTypeConfiguration().Configure(builder.Entity<Customer>());
            new InvoiceEntityTypeConfiguration().Configure(builder.Entity<Invoice>());
            new InvoiceDetailEntityTypeConfiguration().Configure(builder.Entity<InvoiceDetail>());
        }
    }
}