using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestSchadApp.Models;

namespace TestSchadApp.Configurations
{
	public class CustomerTypeEntityTypeConfiguration : IEntityTypeConfiguration<CustomerType>
    {
        public void Configure(EntityTypeBuilder<CustomerType> builder)
        {
            builder
                .Property(b => b.Description)
                .IsRequired()
                .HasMaxLength(70);
        }
    }
}

