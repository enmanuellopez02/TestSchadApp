using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestSchadApp.Models;

namespace TestSchadApp.Configurations
{
	public class InvoiceEntityTypeConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder
                .Property(b => b.TotalItbis)
                .IsRequired()
                .HasColumnType("money")
                .HasDefaultValue(0);

            builder
                .Property(b => b.SubTotal)
                .IsRequired()
                .HasColumnType("money");

            builder
                .Property(b => b.Total)
                .IsRequired()
                .HasColumnType("money");

            builder
                .HasOne(i => i.Customer)
                .WithMany(c => c.Invoices)
                .HasForeignKey(idt => idt.CustomerId);
        }   
    }
}

