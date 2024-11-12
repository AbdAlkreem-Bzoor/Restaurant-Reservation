using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Data.Config
{
    public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            ConfigurePK(builder);

            ConfigureName(builder);

            ConfigureAddress(builder);

            ConfigureOpeningHours(builder);

            ConfigurePhoneNumber(builder);

            builder.ToTable("Restaurants");

            builder.HasData(SeedData.LoadRestaurants());
        }

        private static void ConfigurePhoneNumber(EntityTypeBuilder<Restaurant> builder)
        {
            builder.Property(x => x.PhoneNumber)
                   .HasMaxLength(15)
                   .HasColumnName("Phone Number");
        }

        private static void ConfigureOpeningHours(EntityTypeBuilder<Restaurant> builder)
        {
            builder.Property(x => x.OpeningHours)
                   .HasColumnType("TIME")
                   .HasColumnName("Opening Hours");
        }

        private static void ConfigureAddress(EntityTypeBuilder<Restaurant> builder)
        {
            builder.Property(x => x.Address)
                   .HasMaxLength(35);
        }

        private static void ConfigureName(EntityTypeBuilder<Restaurant> builder)
        {
            builder.Property(x => x.Name)
                   .HasMaxLength(100)
                   .IsRequired();
        }

        private static void ConfigurePK(EntityTypeBuilder<Restaurant> builder)
        {
            builder.HasKey(x => x.RestaurantId);
        }
    }
}
