using IMS.CoreBusiness;
using Microsoft.EntityFrameworkCore;

namespace IMS.Plugins.EFCoreSqlServer
{
    // This class is going to represent the database itself
    public class IMSContext : DbContext
    {
        public IMSContext(DbContextOptions<IMSContext> options): base(options)
        {
            
        }
        public DbSet<Inventory>? Inventories { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<ProductInventory>? ProductInventories { get; set; }
        public DbSet<InventoryTransaction>? InventoryTransactions { get; set; }
        public DbSet<ProductTransaction>? ProductTransactions { get; set; }

        /// <summary>
        /// Build relationships between tables
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductInventory>()
                .HasKey(pi => new { pi.ProductId, pi.InventoryId });

            modelBuilder.Entity<ProductInventory>()
            .HasOne(pi => pi.Product)
            .WithMany(p => p.ProductInventories)
            .HasForeignKey(pi => pi.ProductId);

            modelBuilder.Entity<ProductInventory>()
                .HasOne(pi => pi.Inventory)
                .WithMany(i => i.ProductInventories)
                .HasForeignKey(pi => pi.InventoryId);

            // seeding data
            modelBuilder.Entity<Inventory>().HasData(
                 new Inventory { InventoryId = 1, InventoryName = "Bike Seat", Quantity = 15, Price = 2 },
                new Inventory { InventoryId = 2, InventoryName = "Bike Body", Quantity = 15, Price = 15 },
                new Inventory { InventoryId = 3, InventoryName = "Bike Wheels", Quantity = 30, Price = 5 },
                new Inventory { InventoryId = 4, InventoryName = "Bike Pedals", Quantity = 40, Price = 3 },
                new Inventory { InventoryId = 5, InventoryName = "Bike Bell", Quantity = 20, Price = 2 },
                new Inventory { InventoryId = 6, InventoryName = "Bike Stopper", Quantity = 15, Price = 2 },
                new Inventory { InventoryId = 7, InventoryName = "Bike Chain", Quantity = 23, Price = 5 }
                );

            modelBuilder.Entity<Product>().HasData(
                 new Product { ProductId = 1, ProductName = "Bike", Quantity = 15, Price = 2 },
                new Product { ProductId = 2, ProductName = "Car", Quantity = 15, Price = 15 },
                new Product { ProductId = 3, ProductName = "Toy Car", Quantity = 30, Price = 5 },
                new Product { ProductId = 4, ProductName = "Toy Bike", Quantity = 40, Price = 3 },
                new Product { ProductId = 5, ProductName = "Bike Bell", Quantity = 20, Price = 2 },
                new Product { ProductId = 6, ProductName = "Tricycle", Quantity = 15, Price = 2 },
                new Product { ProductId = 7, ProductName = "Toy Tricycle", Quantity = 23, Price = 5 }
                );

            modelBuilder.Entity<ProductInventory>().HasData(
                new ProductInventory { ProductId = 1, InventoryId = 1, InventoryQuantity = 1 }, // seat
                 new ProductInventory { ProductId = 1, InventoryId = 2, InventoryQuantity = 1 }, // body
                  new ProductInventory { ProductId = 1, InventoryId = 3, InventoryQuantity = 2 }, // wheels
                   new ProductInventory { ProductId = 1, InventoryId = 4, InventoryQuantity = 2 } // pedal
                );
        }
    }
}
