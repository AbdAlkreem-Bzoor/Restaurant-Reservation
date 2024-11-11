using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Data.Config
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            ConfigurePK(builder);

            ConfigureName(builder);

            ConfigureRestaurantIdFK(builder);

            ConfigurePosition(builder);

            CostructRelationsBetweenEntities(builder);

            builder.ToTable("Employees");

            builder.HasData(SeedData.LoadEmployees());
        }

        private static void CostructRelationsBetweenEntities(EntityTypeBuilder<Employee> builder)
        {
            RestaurantOne_To_ManyEmployee(builder);
        }

        private static void RestaurantOne_To_ManyEmployee(EntityTypeBuilder<Employee> builder)
        {
            builder.HasOne(x => x.Restaurant)
                   .WithMany(x => x.Employees)
                   .HasForeignKey(x => x.RestaurantId)
                   .OnDelete(DeleteBehavior.Restrict);
        }

        private static void ConfigurePosition(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(x => x.Position)
                   .HasMaxLength(100);
        }

        private static void ConfigureRestaurantIdFK(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(x => x.RestaurantId);
        }

        private static void ConfigureName(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(x => x.FirstName)
                   .HasColumnName("First Name")
                   .HasMaxLength(45);

            builder.Property(x => x.LastName)
                   .HasColumnName("Last Name")
                   .HasMaxLength(45);
        }

        private static void ConfigurePK(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(x => x.EmployeeId);
        }
    }
}
