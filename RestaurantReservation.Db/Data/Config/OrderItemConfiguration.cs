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
