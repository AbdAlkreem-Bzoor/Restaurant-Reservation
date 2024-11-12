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

            builder.HasData(SeedData.LoadReservations());
        }

        private static void ConfigureReservationDate(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(x => x.ReservationDate)
                   .HasColumnType("DATE")
                   .HasColumnName("Reservation Date")
                   .IsRequired();
        }

        private static void ConfigurePartySize(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(x => x.PartySize)
                   .HasColumnName("Party Size")
                   .IsRequired();
        }

        private static void ConfigureTableIdFK(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(x => x.TableId)
                   .IsRequired();

            builder.HasIndex(x => x.TableId).HasDatabaseName("IX_Reservation_TableId");
        }

        private static void ConfigureRestaurantIdFK(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(x => x.RestaurantId)
                   .IsRequired();

            builder.HasIndex(x => x.RestaurantId).HasDatabaseName("IX_Reservation_RestaurantId");
        }

        private static void ConfigureCustomerIdFK(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(x => x.CustomerId)
                .IsRequired();

            builder.HasIndex(x => x.CustomerId).HasDatabaseName("IX_Reservation_CustomerId");
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
