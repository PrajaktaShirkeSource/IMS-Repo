using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Plugins.EFCoreSqlServer
{
    public class ProductEFCoreRepository:IProductRepository
    {
        public IDbContextFactory<IMSContext> _contextFactory;
        public ProductEFCoreRepository(IDbContextFactory<IMSContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task AddProductAsync(Product product)
        {
            using var db = _contextFactory.CreateDbContext();
            db.Products?.Add(product);
            FlagInventoryUnchanged(product, db);
            await db.SaveChangesAsync();
        }

        public async Task DeleteProductByIdAsync(int productId)
        {
            using var db = _contextFactory.CreateDbContext();
            var product = db.Products?.Find(productId);
            if (product is null) return;

            db.Products?.Remove(product);
            await db.SaveChangesAsync();
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            using var db = _contextFactory.CreateDbContext();
            var product = await db.Products.FindAsync(productId);
            return product;
        }

        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string productName)
        {
            var db = _contextFactory.CreateDbContext();
            return await db.Products.Where(x=>x.ProductName.ToLower().IndexOf(productName.ToLower()) >=0).ToListAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            var db = _contextFactory.CreateDbContext();
            var prod = await db.Products.FindAsync(product.ProductId);

            if (prod is not null)
            {
                prod.ProductName = product.ProductName;
                prod.Price = product.Price;
                prod.Quantity = product.Quantity;
                prod.ProductInventories = product.ProductInventories;
                FlagInventoryUnchanged(prod, db);
                await db.SaveChangesAsync();
            }
        }

        private void FlagInventoryUnchanged(Product product, IMSContext db)
        {
            if(product?.ProductInventories != null &&
                product.ProductInventories.Count > 0)
            {
                foreach(var prodInv in product.ProductInventories)
                {
                    if(prodInv.Inventory is not null)
                    {
                        db.Entry(prodInv.Inventory).State = EntityState.Unchanged;
                    }
                }
            }
        }
    }
}
