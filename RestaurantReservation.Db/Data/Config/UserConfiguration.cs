using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Data.Config
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            ConfigurePK(builder);

            ConfigureName(builder);

            ConfigureEmail(builder);

            ConfigurePassword(builder);

            ConfigureRole(builder);

            builder.ToTable("Users");

            builder.HasData(SeedData.LoadUsers());
        }
        private static void ConfigureRole(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Role)
                   .HasConversion(
                                  x => x.ToString(),
                                  x =>
                                      (UserRole)
                                      Enum.Parse
                                      (
                                       typeof(UserRole), x
                                      )
                                 )
                   .HasColumnName("Role");
        }
        private static void ConfigurePassword(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Password)
                   .HasColumnType("VARCHAR")
                   .HasColumnName("Password")
                   .HasMaxLength(500);
        }

        private static void ConfigureEmail(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Email)
                   .HasColumnType("VARCHAR")
                   .HasMaxLength(320)
                   .IsRequired();

            builder.HasIndex(x => x.Email)
                   .IsUnique();
        }

        private static void ConfigureName(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.UserName)
                   .HasColumnName("User Name")
                   .HasMaxLength(45)
                   .IsRequired();

            builder.HasIndex(x => x.UserName)
                   .IsUnique();
        }

        private static void ConfigurePK(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
