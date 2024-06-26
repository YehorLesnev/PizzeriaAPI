﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pizzeria.Domain;

#nullable disable

namespace Pizzeria.Domain.Migrations
{
    [DbContext(typeof(PizzeriaDbContext))]
    [Migration("20240418160345_AddCascadeDeletingFK_shift_staff_shift")]
    partial class AddCascadeDeletingFK_shift_staff_shift
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Pizzeria.Domain.Models.Address", b =>
                {
                    b.Property<Guid>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("address_id")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("Address1")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar")
                        .HasColumnName("address1");

                    b.Property<string>("Address2")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar")
                        .HasColumnName("address2");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar")
                        .HasColumnName("city");

                    b.Property<string>("Zipcode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar")
                        .HasColumnName("zipcode");

                    b.HasKey("AddressId");

                    b.ToTable("address", (string)null);
                });

            modelBuilder.Entity("Pizzeria.Domain.Models.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasMaxLength(55)
                        .HasColumnType("varchar")
                        .HasColumnName("first_name")
                        .IsFixedLength();

                    b.Property<string>("LastName")
                        .HasMaxLength(55)
                        .HasColumnType("varchar")
                        .HasColumnName("last_name")
                        .IsFixedLength();

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(25)
                        .HasColumnType("varchar")
                        .HasColumnName("phone_number")
                        .IsFixedLength();

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Pizzeria.Domain.Models.Ingredient", b =>
                {
                    b.Property<Guid>("IngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ingredient_id")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("IngredientName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar")
                        .HasColumnName("ingredient_name");

                    b.Property<decimal>("IngredientPrice")
                        .HasColumnType("decimal(5, 2)")
                        .HasColumnName("ingredient_price");

                    b.Property<string>("IngredientWeightMeasure")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar")
                        .HasColumnName("ingredient_weight_measure");

                    b.Property<float>("QuantityInStock")
                        .HasColumnType("real")
                        .HasColumnName("quantity_in_stock");

                    b.HasKey("IngredientId");

                    b.ToTable("ingredients", (string)null);
                });

            modelBuilder.Entity("Pizzeria.Domain.Models.Item", b =>
                {
                    b.Property<Guid>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("item_id")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("image_path");

                    b.Property<string>("ItemCategory")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar")
                        .HasColumnName("item_category");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar")
                        .HasColumnName("item_name");

                    b.Property<decimal>("ItemPrice")
                        .HasColumnType("decimal(5, 2)")
                        .HasColumnName("item_price");

                    b.Property<string>("ItemSize")
                        .IsRequired()
                        .HasMaxLength(55)
                        .IsUnicode(false)
                        .HasColumnType("varchar")
                        .HasColumnName("item_size");

                    b.Property<Guid>("RecipeId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("recipe_id");

                    b.HasKey("ItemId");

                    b.HasIndex("RecipeId")
                        .IsUnique();

                    b.ToTable("items", (string)null);
                });

            modelBuilder.Entity("Pizzeria.Domain.Models.Order", b =>
                {
                    b.Property<Guid>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("order_id")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("customer_id");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime")
                        .HasColumnName("date");

                    b.Property<bool>("Delivery")
                        .HasColumnType("bit")
                        .HasColumnName("delivery");

                    b.Property<Guid?>("DeliveryAddressId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("delivery_address_id");

                    b.Property<Guid>("StaffId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("staff_id");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(5, 2)")
                        .HasColumnName("order_total");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("Date");

                    SqlServerIndexBuilderExtensions.IncludeProperties(b.HasIndex("Date"), new[] { "OrderId", "CustomerId", "StaffId" });

                    b.HasIndex("DeliveryAddressId");

                    b.HasIndex("StaffId");

                    SqlServerIndexBuilderExtensions.IncludeProperties(b.HasIndex("StaffId"), new[] { "OrderId", "Date", "CustomerId" });

                    b.ToTable("orders", (string)null);

                    b.HasAnnotation("SqlServer:UseSqlOutputClause", false);
                });

            modelBuilder.Entity("Pizzeria.Domain.Models.OrderItem", b =>
                {
                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("order_id");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("item_id");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.HasKey("OrderId", "ItemId");

                    b.HasIndex("ItemId");

                    SqlServerIndexBuilderExtensions.IncludeProperties(b.HasIndex("ItemId"), new[] { "OrderId", "Quantity" });

                    b.ToTable("order_items", (string)null);

                    b.HasAnnotation("SqlServer:UseSqlOutputClause", false);
                });

            modelBuilder.Entity("Pizzeria.Domain.Models.Recipe", b =>
                {
                    b.Property<Guid>("RecipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("recipe_id")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<TimeOnly>("CookingTime")
                        .HasColumnType("time")
                        .HasColumnName("cooking_time");

                    b.Property<string>("RecipeName")
                        .IsRequired()
                        .HasMaxLength(55)
                        .IsUnicode(false)
                        .HasColumnType("varchar")
                        .HasColumnName("recipe_name");

                    b.HasKey("RecipeId");

                    b.ToTable("recipes", (string)null);
                });

            modelBuilder.Entity("Pizzeria.Domain.Models.RecipeIngredient", b =>
                {
                    b.Property<Guid>("RecipeId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("recipe_id");

                    b.Property<Guid>("IngredientId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ingredient_id");

                    b.Property<float>("IngredientWeight")
                        .HasColumnType("real")
                        .HasColumnName("ingredient_weight");

                    b.HasKey("RecipeId", "IngredientId");

                    b.HasIndex("IngredientId");

                    b.ToTable("recipe_ingredients", (string)null);
                });

            modelBuilder.Entity("Pizzeria.Domain.Models.Shift", b =>
                {
                    b.Property<Guid>("ShiftId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("shift_id")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateOnly>("ShiftDate")
                        .HasColumnType("date")
                        .HasColumnName("shift_date");

                    b.Property<TimeOnly>("ShiftEndTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("time")
                        .HasDefaultValue(new TimeOnly(22, 0, 0))
                        .HasColumnName("shift_end_time");

                    b.Property<TimeOnly>("ShiftStartTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("time")
                        .HasDefaultValue(new TimeOnly(8, 0, 0))
                        .HasColumnName("shift_start_time");

                    b.HasKey("ShiftId");

                    b.ToTable("shifts", (string)null);
                });

            modelBuilder.Entity("Pizzeria.Domain.Models.ShiftStaff", b =>
                {
                    b.Property<Guid>("ShiftId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("shift_id");

                    b.Property<Guid>("StaffId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("staff_id");

                    b.HasKey("ShiftId", "StaffId");

                    b.HasIndex("StaffId");

                    b.ToTable("shift_staff", (string)null);
                });

            modelBuilder.Entity("Pizzeria.Domain.Models.Staff", b =>
                {
                    b.Property<Guid>("StaffId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("staff_id")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(55)
                        .IsUnicode(false)
                        .HasColumnType("varchar")
                        .HasColumnName("first_name");

                    b.Property<decimal>("HourlyRate")
                        .HasColumnType("decimal(5, 2)")
                        .HasColumnName("hourly_rate");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(55)
                        .IsUnicode(false)
                        .HasColumnType("varchar")
                        .HasColumnName("last_name");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar")
                        .HasColumnName("phone_number")
                        .IsFixedLength();

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar")
                        .HasColumnName("position");

                    b.HasKey("StaffId");

                    b.ToTable("staff", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Pizzeria.Domain.Models.Customer", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Pizzeria.Domain.Models.Customer", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pizzeria.Domain.Models.Customer", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Pizzeria.Domain.Models.Customer", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Pizzeria.Domain.Models.Item", b =>
                {
                    b.HasOne("Pizzeria.Domain.Models.Recipe", "ItemNavigation")
                        .WithOne("Item")
                        .HasForeignKey("Pizzeria.Domain.Models.Item", "RecipeId")
                        .IsRequired()
                        .HasConstraintName("FK_items_recipes");

                    b.Navigation("ItemNavigation");
                });

            modelBuilder.Entity("Pizzeria.Domain.Models.Order", b =>
                {
                    b.HasOne("Pizzeria.Domain.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK_orders_customers");

                    b.HasOne("Pizzeria.Domain.Models.Address", "DeliveryAddress")
                        .WithMany("Orders")
                        .HasForeignKey("DeliveryAddressId")
                        .HasConstraintName("FK_orders_address");

                    b.HasOne("Pizzeria.Domain.Models.Staff", "Staff")
                        .WithMany("Orders")
                        .HasForeignKey("StaffId")
                        .IsRequired()
                        .HasConstraintName("FK_orders_staff");

                    b.Navigation("Customer");

                    b.Navigation("DeliveryAddress");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("Pizzeria.Domain.Models.OrderItem", b =>
                {
                    b.HasOne("Pizzeria.Domain.Models.Item", "Item")
                        .WithMany("OrderItems")
                        .HasForeignKey("ItemId")
                        .IsRequired()
                        .HasConstraintName("FK_order_items_items");

                    b.HasOne("Pizzeria.Domain.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_order_items_orders");

                    b.Navigation("Item");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Pizzeria.Domain.Models.RecipeIngredient", b =>
                {
                    b.HasOne("Pizzeria.Domain.Models.Ingredient", "Ingredient")
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_recipe_ingredients_ingredients");

                    b.HasOne("Pizzeria.Domain.Models.Recipe", "Recipe")
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_recipe_ingredients_recipes");

                    b.Navigation("Ingredient");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("Pizzeria.Domain.Models.ShiftStaff", b =>
                {
                    b.HasOne("Pizzeria.Domain.Models.Shift", "Shift")
                        .WithMany("ShiftStaff")
                        .HasForeignKey("ShiftId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_shift_staff_shift");

                    b.HasOne("Pizzeria.Domain.Models.Staff", "Staff")
                        .WithMany("ShiftStaff")
                        .HasForeignKey("StaffId")
                        .IsRequired()
                        .HasConstraintName("FK_shift_staff_staff");

                    b.Navigation("Shift");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("Pizzeria.Domain.Models.Address", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Pizzeria.Domain.Models.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Pizzeria.Domain.Models.Ingredient", b =>
                {
                    b.Navigation("RecipeIngredients");
                });

            modelBuilder.Entity("Pizzeria.Domain.Models.Item", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("Pizzeria.Domain.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("Pizzeria.Domain.Models.Recipe", b =>
                {
                    b.Navigation("Item");

                    b.Navigation("RecipeIngredients");
                });

            modelBuilder.Entity("Pizzeria.Domain.Models.Shift", b =>
                {
                    b.Navigation("ShiftStaff");
                });

            modelBuilder.Entity("Pizzeria.Domain.Models.Staff", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("ShiftStaff");
                });
#pragma warning restore 612, 618
        }
    }
}
