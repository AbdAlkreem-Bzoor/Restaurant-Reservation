﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RestaurantReservation.Db.Data;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    [DbContext(typeof(RestaurantReservationDbContext))]
    [Migration("20241111151214_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RestaurantReservation.Db.Entities.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<string>("Email")
                        .HasMaxLength(320)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("FirstName")
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)")
                        .HasColumnName("First Name");

                    b.Property<string>("LastName")
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)")
                        .HasColumnName("Last Name");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(15)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Phone Number");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers", (string)null);

                    b.HasData(
                        new
                        {
                            CustomerId = 1,
                            Email = "john.doe@example.com",
                            FirstName = "John",
                            LastName = "Doe",
                            PhoneNumber = "555-0101"
                        },
                        new
                        {
                            CustomerId = 2,
                            Email = "jane.smith@example.com",
                            FirstName = "Jane",
                            LastName = "Smith",
                            PhoneNumber = "555-0102"
                        },
                        new
                        {
                            CustomerId = 3,
                            Email = "alice.johnson@example.com",
                            FirstName = "Alice",
                            LastName = "Johnson",
                            PhoneNumber = "555-0103"
                        },
                        new
                        {
                            CustomerId = 4,
                            Email = "bob.brown@example.com",
                            FirstName = "Bob",
                            LastName = "Brown",
                            PhoneNumber = "555-0104"
                        },
                        new
                        {
                            CustomerId = 5,
                            Email = "charlie.davis@example.com",
                            FirstName = "Charlie",
                            LastName = "Davis",
                            PhoneNumber = "555-0105"
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Entities.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<string>("FirstName")
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)")
                        .HasColumnName("First Name");

                    b.Property<string>("LastName")
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)")
                        .HasColumnName("Last Name");

                    b.Property<string>("Position")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("int");

                    b.HasKey("EmployeeId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Employees", (string)null);

                    b.HasData(
                        new
                        {
                            EmployeeId = 1,
                            FirstName = "Sophia",
                            LastName = "Lee",
                            Position = "Manager",
                            RestaurantId = 1
                        },
                        new
                        {
                            EmployeeId = 2,
                            FirstName = "James",
                            LastName = "King",
                            Position = "Waiter",
                            RestaurantId = 1
                        },
                        new
                        {
                            EmployeeId = 3,
                            FirstName = "Oliver",
                            LastName = "Scott",
                            Position = "Cook",
                            RestaurantId = 2
                        },
                        new
                        {
                            EmployeeId = 4,
                            FirstName = "Liam",
                            LastName = "Evans",
                            Position = "Waiter",
                            RestaurantId = 2
                        },
                        new
                        {
                            EmployeeId = 5,
                            FirstName = "Mia",
                            LastName = "Taylor",
                            Position = "Waitress",
                            RestaurantId = 3
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Entities.MenuItem", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemId"));

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal?>("Price")
                        .HasPrecision(15, 2)
                        .HasColumnType("decimal(15,2)");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("int");

                    b.HasKey("ItemId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("MenuItems", (string)null);

                    b.HasData(
                        new
                        {
                            ItemId = 1,
                            Description = "Beef patty with cheese",
                            Name = "Cheeseburger",
                            Price = 12.99m,
                            RestaurantId = 1
                        },
                        new
                        {
                            ItemId = 2,
                            Description = "Fresh lettuce with Caesar dressing",
                            Name = "Caesar Salad",
                            Price = 8.99m,
                            RestaurantId = 1
                        },
                        new
                        {
                            ItemId = 3,
                            Description = "Spaghetti with creamy carbonara sauce",
                            Name = "Spaghetti Carbonara",
                            Price = 14.99m,
                            RestaurantId = 2
                        },
                        new
                        {
                            ItemId = 4,
                            Description = "Tomato, mozzarella, and basil pizza",
                            Name = "Margherita Pizza",
                            Price = 11.99m,
                            RestaurantId = 2
                        },
                        new
                        {
                            ItemId = 5,
                            Description = "Fresh slices of salmon",
                            Name = "Salmon Sashimi",
                            Price = 18.99m,
                            RestaurantId = 3
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Entities.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateOnly>("OrderDate")
                        .HasColumnType("DATE")
                        .HasColumnName("Order Date");

                    b.Property<int>("ReservationId")
                        .HasColumnType("int");

                    b.Property<decimal?>("TotalAmount")
                        .HasColumnType("DECIMAL(15, 2)")
                        .HasColumnName("Total Amount");

                    b.HasKey("OrderId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ReservationId");

                    b.ToTable("Orders", (string)null);

                    b.HasData(
                        new
                        {
                            OrderId = 1,
                            EmployeeId = 1,
                            OrderDate = new DateOnly(2024, 11, 7),
                            ReservationId = 1,
                            TotalAmount = 20m
                        },
                        new
                        {
                            OrderId = 2,
                            EmployeeId = 2,
                            OrderDate = new DateOnly(2024, 11, 8),
                            ReservationId = 2,
                            TotalAmount = 25m
                        },
                        new
                        {
                            OrderId = 3,
                            EmployeeId = 3,
                            OrderDate = new DateOnly(2024, 11, 9),
                            ReservationId = 3,
                            TotalAmount = 18m
                        },
                        new
                        {
                            OrderId = 4,
                            EmployeeId = 4,
                            OrderDate = new DateOnly(2024, 11, 10),
                            ReservationId = 4,
                            TotalAmount = 30m
                        },
                        new
                        {
                            OrderId = 5,
                            EmployeeId = 5,
                            OrderDate = new DateOnly(2024, 11, 11),
                            ReservationId = 5,
                            TotalAmount = 22m
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Entities.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderItemId"));

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderItemId");

                    b.HasIndex("ItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems", (string)null);

                    b.HasData(
                        new
                        {
                            OrderItemId = 1,
                            ItemId = 1,
                            OrderId = 1,
                            Quantity = 2
                        },
                        new
                        {
                            OrderItemId = 2,
                            ItemId = 2,
                            OrderId = 1,
                            Quantity = 1
                        },
                        new
                        {
                            OrderItemId = 3,
                            ItemId = 3,
                            OrderId = 2,
                            Quantity = 1
                        },
                        new
                        {
                            OrderItemId = 4,
                            ItemId = 4,
                            OrderId = 2,
                            Quantity = 1
                        },
                        new
                        {
                            OrderItemId = 5,
                            ItemId = 5,
                            OrderId = 3,
                            Quantity = 2
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Entities.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservationId"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<byte>("PartySize")
                        .HasColumnType("tinyint")
                        .HasColumnName("Party Size");

                    b.Property<DateOnly>("ReservationDate")
                        .HasColumnType("DATE")
                        .HasColumnName("Reservation Date");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("int");

                    b.Property<int>("TableId")
                        .HasColumnType("int");

                    b.HasKey("ReservationId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("TableId");

                    b.ToTable("Reservations", (string)null);

                    b.HasData(
                        new
                        {
                            ReservationId = 1,
                            CustomerId = 1,
                            PartySize = (byte)4,
                            ReservationDate = new DateOnly(2024, 11, 8),
                            RestaurantId = 1,
                            TableId = 1
                        },
                        new
                        {
                            ReservationId = 2,
                            CustomerId = 2,
                            PartySize = (byte)2,
                            ReservationDate = new DateOnly(2024, 11, 9),
                            RestaurantId = 1,
                            TableId = 2
                        },
                        new
                        {
                            ReservationId = 3,
                            CustomerId = 3,
                            PartySize = (byte)3,
                            ReservationDate = new DateOnly(2024, 11, 10),
                            RestaurantId = 2,
                            TableId = 3
                        },
                        new
                        {
                            ReservationId = 4,
                            CustomerId = 4,
                            PartySize = (byte)5,
                            ReservationDate = new DateOnly(2024, 11, 11),
                            RestaurantId = 2,
                            TableId = 4
                        },
                        new
                        {
                            ReservationId = 5,
                            CustomerId = 5,
                            PartySize = (byte)6,
                            ReservationDate = new DateOnly(2024, 11, 12),
                            RestaurantId = 3,
                            TableId = 5
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Entities.Restaurant", b =>
                {
                    b.Property<int>("RestaurantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RestaurantId"));

                    b.Property<string>("Address")
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<TimeSpan?>("OpeningHours")
                        .HasColumnType("TIME")
                        .HasColumnName("Opening Hours");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("Phone Number");

                    b.HasKey("RestaurantId");

                    b.ToTable("Restaurants", (string)null);

                    b.HasData(
                        new
                        {
                            RestaurantId = 1,
                            Address = "123 Burger St.",
                            Name = "Burger Haven",
                            OpeningHours = new TimeSpan(0, 8, 0, 0, 0),
                            PhoneNumber = "555-1111"
                        },
                        new
                        {
                            RestaurantId = 2,
                            Address = "456 Pasta Rd.",
                            Name = "Pasta Palace",
                            OpeningHours = new TimeSpan(0, 8, 0, 0, 0),
                            PhoneNumber = "555-2222"
                        },
                        new
                        {
                            RestaurantId = 3,
                            Address = "789 Seafood Blvd.",
                            Name = "Seafood Bistro",
                            OpeningHours = new TimeSpan(0, 8, 0, 0, 0),
                            PhoneNumber = "555-3333"
                        },
                        new
                        {
                            RestaurantId = 4,
                            Address = "321 Steak Dr.",
                            Name = "Steakhouse Grill",
                            OpeningHours = new TimeSpan(0, 8, 0, 0, 0),
                            PhoneNumber = "555-4444"
                        },
                        new
                        {
                            RestaurantId = 5,
                            Address = "654 Green Ln.",
                            Name = "Vegan Delight",
                            OpeningHours = new TimeSpan(0, 8, 0, 0, 0),
                            PhoneNumber = "555-5555"
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Entities.Table", b =>
                {
                    b.Property<int>("TableId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TableId"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("int");

                    b.HasKey("TableId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Tables", (string)null);

                    b.HasData(
                        new
                        {
                            TableId = 1,
                            Capacity = 4,
                            RestaurantId = 1
                        },
                        new
                        {
                            TableId = 2,
                            Capacity = 2,
                            RestaurantId = 1
                        },
                        new
                        {
                            TableId = 3,
                            Capacity = 4,
                            RestaurantId = 2
                        },
                        new
                        {
                            TableId = 4,
                            Capacity = 6,
                            RestaurantId = 2
                        },
                        new
                        {
                            TableId = 5,
                            Capacity = 8,
                            RestaurantId = 3
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Entities.Employee", b =>
                {
                    b.HasOne("RestaurantReservation.Db.Entities.Restaurant", "Restaurant")
                        .WithMany("Employees")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Entities.MenuItem", b =>
                {
                    b.HasOne("RestaurantReservation.Db.Entities.Restaurant", "Restaurant")
                        .WithMany("MenuItems")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Entities.Order", b =>
                {
                    b.HasOne("RestaurantReservation.Db.Entities.Employee", "Employee")
                        .WithMany("Orders")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RestaurantReservation.Db.Entities.Reservation", "Reservation")
                        .WithMany("Orders")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Entities.OrderItem", b =>
                {
                    b.HasOne("RestaurantReservation.Db.Entities.MenuItem", "MenuItem")
                        .WithMany("OrderItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RestaurantReservation.Db.Entities.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("MenuItem");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Entities.Reservation", b =>
                {
                    b.HasOne("RestaurantReservation.Db.Entities.Customer", "Customer")
                        .WithMany("Reservations")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RestaurantReservation.Db.Entities.Restaurant", "Restaurant")
                        .WithMany("Reservations")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RestaurantReservation.Db.Entities.Table", "Table")
                        .WithMany("Reservations")
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Restaurant");

                    b.Navigation("Table");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Entities.Table", b =>
                {
                    b.HasOne("RestaurantReservation.Db.Entities.Restaurant", "Restaurant")
                        .WithMany("Tables")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Entities.Customer", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Entities.Employee", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Entities.MenuItem", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Entities.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Entities.Reservation", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Entities.Restaurant", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("MenuItems");

                    b.Navigation("Reservations");

                    b.Navigation("Tables");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Entities.Table", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}