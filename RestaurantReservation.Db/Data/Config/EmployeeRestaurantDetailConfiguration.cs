using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Data.Config
{
    public class EmployeeRestaurantDetailConfiguration
        : IEntityTypeConfiguration<EmployeeRestaurantDetail>
    {
        public void Configure(EntityTypeBuilder<EmployeeRestaurantDetail> builder)
        {
            builder.HasNoKey();
            builder.Property(x => x.RestaurantName).HasColumnName("Restaurant Name");
            builder.Property(x => x.RestaurantAddress).HasColumnName("Address");
            builder.Property(x => x.RestaurantPhoneNumber).HasColumnName("Phone Number");

            builder.ToView("EmployeeRestaurantDetails");

        }
    }
}
