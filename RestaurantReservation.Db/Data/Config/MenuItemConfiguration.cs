using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Data.Config
{
    public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            ConfigurePK(builder);

            ConfigureName(builder);

            ConfigureDescription(builder);

            ConfigurePrice(builder);

            ConfigureRestaurantIdFK(builder);

            CostructRelationsBetweenEntities(builder);

            builder.ToTable("MenuItems");

            builder.HasData(LoadMenuItems());
        }
        private static MenuItem[] LoadMenuItems()
        {
            return [
                       new MenuItem { ItemId = 1, RestaurantId = 1, Name = "Cheeseburger", Description = "Beef patty with cheese", Price = 12.99m, OrderItems = new List<OrderItem>() },
                       new MenuItem { ItemId = 2, RestaurantId = 1, Name = "Caesar Salad", Description = "Fresh lettuce with Caesar dressing", Price = 8.99m, OrderItems = new List<OrderItem>() },
                       new MenuItem { ItemId = 3, RestaurantId = 2, Name = "Spaghetti Carbonara", Description = "Spaghetti with creamy carbonara sauce", Price = 14.99m, OrderItems = new List<OrderItem>() },
                       new MenuItem { ItemId = 4, RestaurantId = 2, Name = "Margherita Pizza", Description = "Tomato, mozzarella, and basil pizza", Price = 11.99m, OrderItems = new List<OrderItem>() },
                       new MenuItem { ItemId = 5, RestaurantId = 3, Name = "Salmon Sashimi", Description = "Fresh slices of salmon", Price = 18.99m, OrderItems = new List<OrderItem>() }
                   ];
        }
        private static void CostructRelationsBetweenEntities(EntityTypeBuilder<MenuItem> builder)
        {
            RestaurantOne_To_ManyMenuItem(builder);
        }

        private static void RestaurantOne_To_ManyMenuItem(EntityTypeBuilder<MenuItem> builder)
        {
            builder.HasOne(x => x.Restaurant)
                   .WithMany(x => x.MenuItems)
                   .HasForeignKey(x => x.RestaurantId)
                   .OnDelete(DeleteBehavior.Restrict);
        }

        private static void ConfigureRestaurantIdFK(EntityTypeBuilder<MenuItem> builder)
        {
            builder.Property(x => x.RestaurantId);
        }

        private static void ConfigurePrice(EntityTypeBuilder<MenuItem> builder)
        {
            builder.Property(x => x.Price)
                   .HasPrecision(15, 2);
        }

        private static void ConfigureDescription(EntityTypeBuilder<MenuItem> builder)
        {
            builder.Property(x => x.Description)
                   .HasMaxLength(500);
        }

        private static void ConfigureName(EntityTypeBuilder<MenuItem> builder)
        {
            builder.Property(x => x.Name)
                   .HasMaxLength(100);
        }

        private static void ConfigurePK(EntityTypeBuilder<MenuItem> builder)
        {
            builder.HasKey(x => x.ItemId);
        }
    }
}
