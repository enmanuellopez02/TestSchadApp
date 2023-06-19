using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestSchadApp.Models;

namespace TestSchadApp.Configurations
{
	public class InvoiceDetailEntityTypeConfiguration : IEntityTypeConfiguration<InvoiceDetail>
    {
        public void Configure(EntityTypeBuilder<InvoiceDetail> builder)
        {
            builder
                .Property(b => b.Qty)
                .IsRequired()
                .HasDefaultValue(0);

            builder
                .Property(b => b.Price)
                .IsRequired();

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
                .HasOne(idt => idt.Invoice)
                .WithMany(i => i.InvoiceDetails)
                .HasForeignKey(idt => idt.InvoiceId);
        }
    }
}

