using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RestaurantReservation.Db.Entities;
using System;

namespace RestaurantReservation.Db.Data
{
    public class RestaurantReservationDbContext : DbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ReservationStatus> ReservationsStatus { get; set; }
        public DbSet<ReservationCustomerRestaurantDetail> ReservationsCustomerRestaurantDetails { get; set; }
        public DbSet<EmployeeRestaurantDetail> EmployeeRestaurantDetails { get; set; }
        public DbSet<CustomerWithPartySizeAbove> CustomersWithPartySizeAbove { get; set; }
        public RestaurantReservationDbContext() { }
        public RestaurantReservationDbContext(DbContextOptions<RestaurantReservationDbContext> options) : base(options) { }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);

        //    var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        //    var conn = config.GetSection("constr").Value;

        //    optionsBuilder.UseSqlServer(conn);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RestaurantReservationDbContext).Assembly);
        }

        [DbFunction("fn_CalculateTotalRevenueByRestaurant", Schema = "dbo")]
        public static decimal CalculateTotalRevenueByRestaurant(int restaurantId)
        {
            throw new NotImplementedException();
        }
    }
}
