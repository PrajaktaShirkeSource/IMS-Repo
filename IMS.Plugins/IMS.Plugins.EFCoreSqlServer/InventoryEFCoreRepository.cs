using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Plugins.EFCoreSqlServer
{
    public class InventoryEFCoreRepository: IInventoryRepository
    {
        private readonly IDbContextFactory<IMSContext> _contextFactory;
        
        public InventoryEFCoreRepository(IDbContextFactory<IMSContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task AddInventoryAsync(Inventory inventory)
        {
            using var db = this._contextFactory.CreateDbContext();
            db.Inventories?.Add(inventory);
            await db.SaveChangesAsync();
        }

        public async Task DeleteInventoryByIdAsync(int inventoryId)
        {
            using var db = this._contextFactory.CreateDbContext();
            var inventory = db.Inventories?.Find(inventoryId);
            if (inventory is null) return;

            db.Inventories?.Remove(inventory);
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Inventory>> GetInventoriesByNameAsync(string inventoryName)
        {
            using var db = _contextFactory.CreateDbContext();
            return await db.Inventories.Where(x => x.InventoryName.ToLower().IndexOf(inventoryName.ToLower()) >= 0).ToListAsync();
        }

        public async Task<Inventory> GetInventoryByIdAsync(int inventoryId)
        {
            using var db = _contextFactory.CreateDbContext();
            var inventory = await db.Inventories.FindAsync(inventoryId);
            return inventory;
        }

        public async Task UpdateInventoryAsync(Inventory inventory)
        {
            using var db = _contextFactory.CreateDbContext();
            var inv = await db.Inventories.FindAsync(inventory.InventoryId);
            if (inv is not null)
            {
                inv.InventoryName = inventory.InventoryName;
                inv.Quantity = inventory.Quantity;
                inv.Price = inventory.Price;
                await db.SaveChangesAsync();
            }
        }
    }
}
