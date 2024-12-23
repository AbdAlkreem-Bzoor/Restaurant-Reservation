﻿using Microsoft.EntityFrameworkCore;
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

            builder.HasData(SeedData.LoadMenuItems());
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
            builder.Property(x => x.RestaurantId)
                   .IsRequired();

            builder.HasIndex("RestaurantId").HasDatabaseName("IX_MenuItem_RestaurantId");
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
                   .HasMaxLength(100)
                   .IsRequired();
        }

        private static void ConfigurePK(EntityTypeBuilder<MenuItem> builder)
        {
            builder.HasKey(x => x.ItemId);
        }
    }
}
