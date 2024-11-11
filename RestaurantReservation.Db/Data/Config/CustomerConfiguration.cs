using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Data.Config
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            ConfigurePK(builder);

            ConfigureName(builder);

            ConfigureEmail(builder);

            ConfigurePhoneNumber(builder);

            builder.ToTable("Customers");
        }

        private static void ConfigurePhoneNumber(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(x => x.PhoneNumber)
                   .HasColumnType("VARCHAR")
                   .HasColumnName("Phone Number")
                   .HasMaxLength(15);
        }

        private static void ConfigureEmail(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(x => x.Email)
                   .HasColumnType("VARCHAR")
                   .HasMaxLength(320);
        }

        private static void ConfigureName(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(x => x.FirstName)
                   .HasColumnName("First Name")
                   .HasMaxLength(45);

            builder.Property(x => x.LastName)
                   .HasColumnName("Last Name")
                   .HasMaxLength(45);
        }

        private static void ConfigurePK(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.CustomerId);
        }
    }
}
