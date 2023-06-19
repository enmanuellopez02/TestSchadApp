using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestSchadApp.Models;

namespace TestSchadApp.Configurations
{
	public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder
                .Property(b => b.CustomerName)
                .IsRequired()
                .HasMaxLength(70);

            builder
                .Property(b => b.Address)
                .IsRequired()
                .HasMaxLength(120);

            builder
                .Property(b => b.Status)
                .HasDefaultValue(true);

            builder
                .Property(b => b.CustomerTypeId)
                .HasDefaultValue(1);

            builder
                .HasOne(c => c.CustomerType)
                .WithMany(ct => ct.Customers)
                .HasForeignKey(c => c.CustomerTypeId);
        }
    }
}

