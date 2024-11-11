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

            builder.HasData(LoadRestaurants());
        }
        private static Restaurant[] LoadRestaurants()
        {
            return [
                       new Restaurant { RestaurantId = 1, Name = "Burger Haven", Address = "123 Burger St.", PhoneNumber = "555-1111", Tables = new List<Table>(), Reservations = new List<Reservation>(), Employees = new List<Employee>(), MenuItems = new List<MenuItem>(), OpeningHours = new TimeSpan(8, 0, 0) },
                       new Restaurant { RestaurantId = 2, Name = "Pasta Palace", Address = "456 Pasta Rd.", PhoneNumber = "555-2222", Tables = new List<Table>(), Reservations = new List<Reservation>(), Employees = new List<Employee>(), MenuItems = new List<MenuItem>(), OpeningHours = new TimeSpan(8, 0, 0) },
                       new Restaurant { RestaurantId = 3, Name = "Seafood Bistro", Address = "789 Seafood Blvd.", PhoneNumber = "555-3333", Tables = new List<Table>(), Reservations = new List<Reservation>(), Employees = new List<Employee>(), MenuItems = new List<MenuItem>(), OpeningHours = new TimeSpan(8, 0, 0) },
                       new Restaurant { RestaurantId = 4, Name = "Steakhouse Grill", Address = "321 Steak Dr.", PhoneNumber = "555-4444", Tables = new List<Table>(), Reservations = new List<Reservation>(), Employees = new List<Employee>(), MenuItems = new List<MenuItem>(), OpeningHours = new TimeSpan(8, 0, 0) },
                       new Restaurant { RestaurantId = 5, Name = "Vegan Delight", Address = "654 Green Ln.", PhoneNumber = "555-5555", Tables = new List<Table>(), Reservations = new List<Reservation>(), Employees = new List<Employee>(), MenuItems = new List<MenuItem>(), OpeningHours = new TimeSpan(8, 0, 0) }
                   ];
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
                   .HasMaxLength(100);
        }

        private static void ConfigurePK(EntityTypeBuilder<Restaurant> builder)
        {
            builder.HasKey(x => x.RestaurantId);
        }
    }
}
