﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Data.Config
{
    public class TableConfiguration : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            ConfigurePK(builder);

            ConfigureRestaurantIdFK(builder);

            ConfigureCapacity(builder);

            CostructRelationsBetweenEntities(builder);

            builder.ToTable("Tables");


            builder.HasData(LoadTables());
        }
        private static Table[] LoadTables()
        {
            return [
                       new Table { TableId = 1, RestaurantId = 1, Capacity = 4, Reservations = new List<Reservation>() },
                       new Table { TableId = 2, RestaurantId = 1, Capacity = 2, Reservations = new List<Reservation>() },
                       new Table { TableId = 3, RestaurantId = 2, Capacity = 4, Reservations = new List<Reservation>() },
                       new Table { TableId = 4, RestaurantId = 2, Capacity = 6, Reservations = new List<Reservation>() },
                       new Table { TableId = 5, RestaurantId = 3, Capacity = 8, Reservations = new List<Reservation>() }
                   ];
        }
        private static void CostructRelationsBetweenEntities(EntityTypeBuilder<Table> builder)
        {
            RestaurantOne_To_ManyTable(builder);
        }

        private static void RestaurantOne_To_ManyTable(EntityTypeBuilder<Table> builder)
        {
            builder.HasOne(x => x.Restaurant)
                   .WithMany(x => x.Tables)
                   .HasForeignKey(x => x.RestaurantId)
                   .OnDelete(DeleteBehavior.Restrict);
        }

        private static void ConfigureCapacity(EntityTypeBuilder<Table> builder)
        {
            builder.Property(x => x.Capacity);
        }

        private static void ConfigureRestaurantIdFK(EntityTypeBuilder<Table> builder)
        {
            builder.Property(x => x.RestaurantId);
        }

        private static void ConfigurePK(EntityTypeBuilder<Table> builder)
        {
            builder.HasKey(x => x.TableId);
        }
    }
}