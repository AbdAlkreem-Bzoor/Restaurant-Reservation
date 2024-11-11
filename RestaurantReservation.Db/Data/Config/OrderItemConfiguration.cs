using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Data.Config
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            ConfigurePK(builder);

            ConfigureOrderIdFK(builder);

            ConfigureItemIdFK(builder);

            ConfigureQuantity(builder);

            CostructRelationsBetweenEntities(builder);

            builder.ToTable("OrderItems");

            builder.HasData(LoadOrderItems());
        }
        private static OrderItem[] LoadOrderItems()
        {
            return [
                       new OrderItem { OrderItemId = 1, OrderId = 1, ItemId = 1, Quantity = 2 },
                       new OrderItem { OrderItemId = 2, OrderId = 1, ItemId = 2, Quantity = 1 },
                       new OrderItem { OrderItemId = 3, OrderId = 2, ItemId = 3, Quantity = 1 },
                       new OrderItem { OrderItemId = 4, OrderId = 2, ItemId = 4, Quantity = 1 },
                       new OrderItem { OrderItemId = 5, OrderId = 3, ItemId = 5, Quantity = 2 }
                   ];
        }
        private static void CostructRelationsBetweenEntities(EntityTypeBuilder<OrderItem> builder)
        {
            OrderOne_To_ManyOrderItem(builder);
            MenuItemOne_To_ManyOrderItem(builder);
        }

        private static void MenuItemOne_To_ManyOrderItem(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasOne(x => x.MenuItem)
                   .WithMany(x => x.OrderItems)
                   .HasForeignKey(x => x.ItemId)
                   .OnDelete(DeleteBehavior.Restrict);
        }

        private static void OrderOne_To_ManyOrderItem(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasOne(x => x.Order)
                   .WithMany(x => x.OrderItems)
                   .HasForeignKey(x => x.OrderId)
                   .OnDelete(DeleteBehavior.Restrict);
        }

        private static void ConfigureQuantity(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(x => x.Quantity);
        }

        private static void ConfigureItemIdFK(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(x => x.ItemId);
        }

        private static void ConfigureOrderIdFK(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(x => x.OrderId);
        }

        private static void ConfigurePK(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => x.OrderItemId);
        }
    }
}
