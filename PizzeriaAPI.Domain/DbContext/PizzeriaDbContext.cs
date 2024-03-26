using Microsoft.EntityFrameworkCore;
using PizzeriaAPI.Domain.Entities;

namespace PizzeriaAPI.Domain.DbContext
{
    public class PizzeriaDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public PizzeriaDbContext(DbContextOptions<PizzeriaDbContext> options)
            : base(options) { }
            
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Rota> Rota { get; set; }
        public DbSet<Staff> Staff {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Rota)
                .WithMany()
                .HasForeignKey(o => o.CreatedAt)
                .HasPrincipalKey(r => r.Date);
        }
    }
}
