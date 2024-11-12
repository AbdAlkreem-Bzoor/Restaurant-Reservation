using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Data.Config
{
    public class ReservationStatusConfiguration 
        : IEntityTypeConfiguration<ReservationStatus>
    {
        public void Configure(EntityTypeBuilder<ReservationStatus> builder)
        {
            ConfigurePK(builder);

            ConfigureStatusDate(builder);

            ConfigureReservationIdFK(builder);

            ConfigureStatus(builder);

            CostructRelationsBetweenEntities(builder);

            builder.ToTable("ReservationsStatus");

            builder.HasData(SeedData.LoadReservationsStatus());
        }

        private void CostructRelationsBetweenEntities(EntityTypeBuilder<ReservationStatus> builder)
        {
            ReservationOne_To_ManyReservationStatus(builder);
        }

        private void ReservationOne_To_ManyReservationStatus(EntityTypeBuilder<ReservationStatus> builder)
        {
            builder.HasOne(x => x.Reservation)
                   .WithMany(x => x.ReservationStatus)
                   .HasForeignKey(x => x.ReservationId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
        private void ConfigureStatus(EntityTypeBuilder<ReservationStatus> builder)
        {
            builder.Property(x => x.Status)
                   .HasConversion(
                                  x => x.ToString(),
                                  x =>
                                      (ReservationStatusType)
                                      Enum.Parse
                                      (
                                       typeof(ReservationStatusType), x
                                      )
                                 )
                   .HasColumnName("Status");
        }
        private void ConfigureStatusDate(EntityTypeBuilder<ReservationStatus> builder)
        {
            builder.Property(x => x.StatusDate)
                   .HasColumnName("Status Date")
                   .HasColumnType("DATE")
                   .IsRequired();
        }

        private void ConfigureReservationIdFK(EntityTypeBuilder<ReservationStatus> builder)
        {
            builder.Property(x => x.ReservationId)
                   .IsRequired();

            builder.HasIndex(x => x.ReservationId)
                   .HasDatabaseName("IX_ReservationsStatus_ReservationId");
        }

        private void ConfigurePK(EntityTypeBuilder<ReservationStatus> builder)
        {
            builder.HasKey(x => x.ReservationStatusId);
        }
    }
}
