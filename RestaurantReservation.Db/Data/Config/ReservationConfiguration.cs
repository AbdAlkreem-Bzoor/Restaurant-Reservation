using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Data.Config
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            ConfigurePK(builder);

            ConfigureCustomerIdFK(builder);

            ConfigureRestaurantIdFK(builder);

            ConfigureTableIdFK(builder);

            ConfigurePartySize(builder);

            ConfigureReservationDate(builder);

            CostructRelationsBetweenEntities(builder);

            builder.ToTable("Reservations");

            builder.HasData(LoadReservations());
        }
        private static Reservation[] LoadReservations()
        {
            return [
                       new Reservation { ReservationId = 1, CustomerId = 1, RestaurantId = 1, TableId = 1, ReservationDate = new DateOnly(2024, 11, 8), PartySize = 4, Orders = new List<Order>() },
                       new Reservation { ReservationId = 2, CustomerId = 2, RestaurantId = 1, TableId = 2, ReservationDate = new DateOnly(2024, 11, 9), PartySize = 2, Orders = new List<Order>() },
                       new Reservation { ReservationId = 3, CustomerId = 3, RestaurantId = 2, TableId = 3, ReservationDate = new DateOnly(2024, 11, 10), PartySize = 3, Orders = new List<Order>() },
                       new Reservation { ReservationId = 4, CustomerId = 4, RestaurantId = 2, TableId = 4, ReservationDate = new DateOnly(2024, 11, 11), PartySize = 5, Orders = new List<Order>() },
                       new Reservation { ReservationId = 5, CustomerId = 5, RestaurantId = 3, TableId = 5, ReservationDate = new DateOnly(2024, 11, 12), PartySize = 6, Orders = new List<Order>() }
                   ];
        }
        private static void ConfigureReservationDate(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(x => x.ReservationDate)
                   .HasColumnType("DATE")
                   .HasColumnName("Reservation Date");
        }

        private static void ConfigurePartySize(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(x => x.PartySize)
                   .HasColumnName("Party Size");
        }

        private static void ConfigureTableIdFK(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(x => x.TableId);
        }

        private static void ConfigureRestaurantIdFK(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(x => x.RestaurantId);
        }

        private static void ConfigureCustomerIdFK(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(x => x.CustomerId);
        }

        private static void ConfigurePK(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(x => x.ReservationId);
        }

        private static void CostructRelationsBetweenEntities(EntityTypeBuilder<Reservation> builder)
        {
            CustomerOne_To_ManyReservation(builder);

            TableOne_To_ManyReservation(builder);

            RestaurantOne_To_ManyReservation(builder);
        }

        private static void RestaurantOne_To_ManyReservation(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasOne(x => x.Restaurant)
                   .WithMany(x => x.Reservations)
                   .HasForeignKey(x => x.RestaurantId)
                   .OnDelete(DeleteBehavior.Restrict);
        }

        private static void TableOne_To_ManyReservation(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasOne(x => x.Table)
                   .WithMany(x => x.Reservations)
                   .HasForeignKey(x => x.TableId)
                   .OnDelete(DeleteBehavior.Restrict);
        }

        private static void CustomerOne_To_ManyReservation(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasOne(x => x.Customer)
                   .WithMany(x => x.Reservations)
                   .HasForeignKey(x => x.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
