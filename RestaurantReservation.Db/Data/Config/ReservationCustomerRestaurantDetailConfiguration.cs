using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Data.Config
{
    public class ReservationCustomerRestaurantDetailConfiguration
        : IEntityTypeConfiguration<ReservationCustomerRestaurantDetail>
    {
        public void Configure(EntityTypeBuilder<ReservationCustomerRestaurantDetail> builder)
        {
            builder.HasNoKey();
            builder.Property(x => x.RestaurantName).HasColumnName("Restaurant Name");
            builder.Property(x => x.RestaurantAddress).HasColumnName("Address");
            builder.Property(x => x.CustomerPhoneNumber).HasColumnName("Customer Phone Number");
            builder.Property(x => x.PartySize).HasColumnName("Party Size");
            builder.Property(x => x.CustomerPhoneNumber).HasColumnName("Phone Number");
            builder.Property(x => x.ReservationDate).HasColumnName("Reservation Date");
            builder.Property(x => x.PartySize).HasColumnName("Party Size");
            builder.Property(x => x.RestaurantPhoneNumber).HasColumnName("Restaurant Phone Number");

            builder.ToView("ReservationsCustomerRestaurantDetails");

        }
    }
}
