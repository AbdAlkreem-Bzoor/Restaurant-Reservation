using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            ConfigurePK(builder);

            ConfigureEmployeeIdFK(builder);

            ConfigureReservationIdFK(builder);

            ConfigureOrderDate(builder);

            ConfigureTotalAmount(builder);


            CostructRelationsBetweenEntities(builder);

            builder.ToTable("Orders");
        }
        
        private static void CostructRelationsBetweenEntities(EntityTypeBuilder<Order> builder)
        {
            ReservationOne_To_ManyOrder(builder);

            EmployeeOne_To_ManyOrder(builder);
        }

        private static void EmployeeOne_To_ManyOrder(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(x => x.Employee)
                   .WithMany(x => x.Orders)
                   .HasForeignKey(x => x.EmployeeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }

        private static void ReservationOne_To_ManyOrder(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(x => x.Reservation)
                   .WithMany(x => x.Orders)
                   .HasForeignKey(x => x.ReservationId)
                   .OnDelete(DeleteBehavior.Restrict);
        }

        private static void ConfigureTotalAmount(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.TotalAmount)
                   .HasColumnName("Total Amount")
                   .HasColumnType("DECIMAL(15, 2)");
        }

        private static void ConfigureOrderDate(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.OrderDate)
                   .HasColumnType("DATE")
                   .HasColumnName("Order Date");
        }

        private static void ConfigureReservationIdFK(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.ReservationId);
        }

        private static void ConfigureEmployeeIdFK(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.EmployeeId);
        }

        private static void ConfigurePK(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.OrderId);
        }
    }
}
