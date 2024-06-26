﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Domain.Dto.StatisticsDto;
using Pizzeria.Domain.Models;

namespace Pizzeria.Domain;

public partial class PizzeriaDbContext : IdentityDbContext<Customer, IdentityRole<Guid>, Guid>
{
    public PizzeriaDbContext()
    {
    }

    public PizzeriaDbContext(DbContextOptions<PizzeriaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<RecipeIngredient> RecipeIngredients { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }
    public virtual DbSet<Shift> Shifts { get; set; }
    public virtual DbSet<ShiftStaff> ShiftStaff { get; set; }

    public virtual DbSet<Log> Logs { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Shift>(entity =>
        {
            entity.ToTable("shifts");

            entity.Property(e => e.ShiftId)
                .HasDefaultValueSql("NEWID()")
                .HasColumnName("shift_id");
            entity.Property(e => e.ShiftDate)
                .IsRequired()
                .HasColumnName("shift_date");

            entity.Property(e => e.ShiftStartTime)
                .HasDefaultValue(Constants.Constants.ShiftStartTime)
                .HasColumnName("shift_start_time");
            entity.Property(e => e.ShiftEndTime)
                .HasDefaultValue(Constants.Constants.ShiftEndTime)
                .HasColumnName("shift_end_time");

            // Indexes
            entity.HasIndex(e => e.ShiftDate)
                .IncludeProperties(p => new { p.ShiftId, p.ShiftStartTime, p.ShiftEndTime })
                .IsUnique(false);
        });

        modelBuilder.Entity<ShiftStaff>(entity =>
        {
            entity.HasKey(e => new { e.ShiftId, e.StaffId });

            entity.ToTable("shift_staff");

            entity.Property(e => e.ShiftId).HasColumnName("shift_id");
            entity.Property(e => e.StaffId).HasColumnName("staff_id");

            entity.HasOne(d => d.Shift).WithMany(p => p.ShiftStaff)
                .HasForeignKey(d => d.ShiftId)
                .HasConstraintName("FK_shift_staff_shift")
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Staff).WithMany(p => p.ShiftStaff)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK_shift_staff_staff")
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("address");

            entity.Property(e => e.AddressId)
                .HasDefaultValueSql("NEWID()")
                .HasColumnName("address_id");
            entity.Property(e => e.Address1)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("address1");
            entity.Property(e => e.Address2)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("address2");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Zipcode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("zipcode");

            // Indexes
            entity.HasIndex(e => e.City)
                .IncludeProperties(p => new { p.AddressId, p.Address1, p.Address2, p.Zipcode })
                .IsUnique(false);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("customers");

            entity.Property(e => e.FirstName)
                .HasMaxLength(55)
                .IsFixedLength()
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(55)
                .IsFixedLength()
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(25)
                .IsFixedLength()
                .HasColumnName("phone_number");

            // Indexes
            entity.HasIndex(e => new { e.FirstName, e.LastName })
                .IncludeProperties(p => new { p.Id, p.PhoneNumber })
                .IsUnique(false);
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.ToTable("ingredients");

            entity.Property(e => e.IngredientId)
                .HasDefaultValueSql("NEWID()")
                .HasColumnName("ingredient_id");
            entity.Property(e => e.IngredientName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ingredient_name");
            entity.Property(e => e.IngredientPrice)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("ingredient_price");
            entity.Property(e => e.IngredientWeightMeasure)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ingredient_weight_measure");

            entity.Property(e => e.QuantityInStock)
                .HasColumnName("quantity_in_stock");

            // Indexes
            entity.HasIndex(e => e.IngredientName)
                .IncludeProperties(p => new { p.IngredientId, p.IngredientPrice, p.IngredientWeightMeasure, p.QuantityInStock });
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.ToTable("items");

            entity.Property(e => e.ItemId)
                .HasDefaultValueSql("NEWID()")
                .HasColumnName("item_id");
            entity.Property(e => e.ItemCategory)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("item_category");
            entity.Property(e => e.ItemName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("item_name");
            entity.Property(e => e.ItemPrice)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("item_price");
            entity.Property(e => e.ItemSize)
                .HasMaxLength(55)
                .IsUnicode(false)
                .HasColumnName("item_size");
            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");
            entity.Property(e => e.ImagePath).HasColumnName("image_path");

            entity.HasOne(d => d.ItemNavigation).WithOne(p => p.Item)
                .HasForeignKey<Item>(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_items_recipes");

            // Indexes
            entity.HasIndex(e => e.ItemName)
                .IncludeProperties(p => new { p.ItemId, p.ItemCategory, p.ItemSize, p.ItemPrice, p.ImagePath });

            entity.HasIndex(e => e.ItemCategory)
                .IncludeProperties(p => new { p.ItemId, p.ItemName, p.ItemSize, p.ItemPrice, p.ImagePath })
                .IsUnique(false);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("orders",
                tb => tb.UseSqlOutputClause(false));

            entity.Property(e => e.OrderId)
                .HasDefaultValueSql("NEWID()")
                .HasColumnName("order_id");
            entity.Property(e => e.CustomerId)
                .HasColumnName("customer_id")
                .HasDefaultValue(null);
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Delivery).HasColumnName("delivery");
            entity.Property(e => e.DeliveryAddressId).HasColumnName("delivery_address_id");
            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("order_total");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_orders_customers")
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.DeliveryAddress).WithMany(p => p.Orders)
                .HasForeignKey(d => d.DeliveryAddressId)
                .HasConstraintName("FK_orders_address")
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.Staff).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK_orders_staff")
                .OnDelete(DeleteBehavior.SetNull);

            // Indexes
            entity.HasIndex(e => e.Date)
            .IncludeProperties(p => new { p.OrderId, p.CustomerId, p.StaffId })
            .IsUnique(false);

            entity.HasIndex(e => e.StaffId)
            .IncludeProperties(p => new { p.OrderId, p.Date, p.CustomerId })
            .IsUnique(false);

            entity.HasIndex(e => e.CustomerId)
                .IncludeProperties(p => new { p.OrderId, p.Date, p.StaffId })
                .IsUnique(false);

        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ItemId });

            entity.ToTable("order_items",
                tb => tb.UseSqlOutputClause(false));

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Item).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK_order_items_items")
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_order_items_orders")
                .OnDelete(DeleteBehavior.Cascade);

            //Indexes
            entity.HasIndex(e => e.ItemId)
                .IncludeProperties(p => new { p.OrderId, p.Quantity });
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.ToTable("recipes");

            entity.Property(e => e.RecipeId)
                .HasDefaultValueSql("NEWID()")
                .HasColumnName("recipe_id");
            entity.Property(e => e.CookingTime)
                .HasColumnName("cooking_time");
            entity.Property(e => e.RecipeName)
                .HasMaxLength(55)
                .IsUnicode(false)
                .HasColumnName("recipe_name");

            //Indexes
            entity.HasIndex(e => e.RecipeName)
                .IncludeProperties(p => new { p.RecipeId, p.CookingTime });
        });

        modelBuilder.Entity<RecipeIngredient>(entity =>
        {
            entity.HasKey(e => new { e.RecipeId, e.IngredientId });

            entity.ToTable("recipe_ingredients");

            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");
            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");
            entity.Property(e => e.IngredientWeight).HasColumnName("ingredient_weight");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.RecipeIngredients)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_recipe_ingredients_ingredients")
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeIngredients)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_recipe_ingredients_recipes")
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.ToTable("staff");

            entity.Property(e => e.StaffId)
                .HasDefaultValueSql("NEWID()")
                .HasColumnName("staff_id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(55)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.HourlyRate)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("hourly_rate");
            entity.Property(e => e.LastName)
                .HasMaxLength(55)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Position)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("position");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(25)
                .IsFixedLength()
                .HasColumnName("phone_number");

            //Indexes
            entity.HasIndex(e => new { e.FirstName, e.LastName })
                .IncludeProperties(p => new { p.StaffId, p.Position, p.HourlyRate })
                .IsUnique(false);
        });

        OnModelCreatingPartial(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public IEnumerable<StaffPayrollResult> CalculateStaffPayroll(DateTime startDate, DateTime endDate)
    {
        // Create parameters for the procedure
        FormattableString sqlQuery = $"EXEC CalculateStaffPayroll {startDate}, {endDate}";

        // Execute the stored procedure
        return this.Database.SqlQuery<StaffPayrollResult>(sqlQuery);
    }

    public IEnumerable<TotalSales> GetTotalSalesRevenueByDay(DateTime startDate, DateTime endDate)
    {
        FormattableString sqlQuery = $"EXEC GetTotalSalesRevenueByDay {startDate.Date}, {endDate.Date}";

        return this.Database.SqlQuery<TotalSales>(sqlQuery);
    }

    public IEnumerable<AverageOrderTotalValue> GetAverageOrderValueByDays(DateTime startDate, DateTime endDate)
    {
        FormattableString sqlQuery = $"EXEC GetAverageOrderValueByDay {startDate.Date}, {endDate.Date}";

        return this.Database.SqlQuery<AverageOrderTotalValue>(sqlQuery);
    }

    public IEnumerable<AverageOrderTotalValue> GetAverageOrderValueByMonth(DateTime startDate, DateTime endDate)
    {
        FormattableString sqlQuery = $"EXEC GetAverageOrderValueByMonth {startDate.Date}, {endDate.Date}";

        return this.Database.SqlQuery<AverageOrderTotalValue>(sqlQuery);
    }
    public IEnumerable<AverageOrderTotalValue> GetAverageOrderValueByYear(DateTime startDate, DateTime endDate)
    {
        FormattableString sqlQuery = $"EXEC GetAverageOrderValueByYear {startDate.Date}, {endDate.Date}";

        return this.Database.SqlQuery<AverageOrderTotalValue>(sqlQuery);
    }
    
    public IEnumerable<TotalSales> GetSalesDistributionByCategoryAndDay(string itemCategory, DateTime startDate, DateTime endDate)
    {
        FormattableString sqlQuery = $"EXEC GetSalesDistributionByCategoryAndDay {itemCategory}, {startDate.Date}, {endDate.Date}";

        return this.Database.SqlQuery<TotalSales>(sqlQuery);
    }
    
    public ProductOfMonth? GetProductOfMonth(int year, int month)
    {
        FormattableString sqlQuery = $"EXEC GetProductOfMonth {year}, {month}";

        return this.Database.SqlQuery<ProductOfMonth>(sqlQuery).AsEnumerable().FirstOrDefault();
    }
}