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

            builder.HasData(LoadOrders());
        }
        private static Order[] LoadOrders()
        {
            return [
                       new Order { OrderId = 1, ReservationId = 1, EmployeeId = 1, OrderDate = new DateOnly(2024, 11, 7), TotalAmount = 20, OrderItems = new List<OrderItem>() },
                       new Order { OrderId = 2, ReservationId = 2, EmployeeId = 2, OrderDate = new DateOnly(2024, 11, 8), TotalAmount = 25, OrderItems = new List<OrderItem>() },
                       new Order { OrderId = 3, ReservationId = 3, EmployeeId = 3, OrderDate = new DateOnly(2024, 11, 9), TotalAmount = 18, OrderItems = new List<OrderItem>() },
                       new Order { OrderId = 4, ReservationId = 4, EmployeeId = 4, OrderDate = new DateOnly(2024, 11, 10), TotalAmount = 30, OrderItems = new List<OrderItem>() },
                       new Order { OrderId = 5, ReservationId = 5, EmployeeId = 5, OrderDate = new DateOnly(2024, 11, 11), TotalAmount = 22, OrderItems = new List<OrderItem>() }
                   ];
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
                   .HasColumnName("Total Amount");
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
