using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.Plugins.InMemory
{
    public class InventoryRepository: IInventoryRepository
    {
        private List<Inventory> _inventories;
        public InventoryRepository()
        {
            _inventories = new List<Inventory>()
            {
                new Inventory{InventoryId=1, InventoryName="Bike Seat", Quantity=15, Price=2},
                new Inventory{InventoryId=2, InventoryName="Bike Body", Quantity=15, Price=15},
                new Inventory{InventoryId=3, InventoryName="Bike Wheels", Quantity=30, Price=5},
                new Inventory{InventoryId=4, InventoryName="Bike Pedals", Quantity=40, Price=3},
                new Inventory{InventoryId=5, InventoryName="Bike Bell", Quantity=20, Price=2},
                new Inventory{InventoryId=6, InventoryName="Bike Stopper", Quantity=15, Price=2},
                new Inventory{InventoryId=7, InventoryName="Bike Chain", Quantity=23, Price=5}
            };
        }

        /// <summary>
        /// Get all the inventories that contains the entered name
        /// </summary>
        /// <param name="inventoryName">Name entered by user</param>
        /// <returns>List of all the inventories matching with the name entered</returns>
        public async Task<IEnumerable<Inventory>> GetInventoriesByNameAsync(string inventoryName)
        {
            if (string.IsNullOrWhiteSpace(inventoryName))
                return await Task.FromResult(_inventories);

            return _inventories.Where(x=>x.InventoryName.Contains(inventoryName, StringComparison.OrdinalIgnoreCase));
        }

        public Task AddInventoryAsync(Inventory inventory)
        {
            if(_inventories.Any(i=>i.InventoryName.Equals (inventory.InventoryName, StringComparison.OrdinalIgnoreCase)))
            {
                return Task.CompletedTask;
            }

            var maxInventoryId = _inventories.Max(i => i.InventoryId);
            inventory.InventoryId = maxInventoryId + 1 ;

            _inventories.Add(inventory);

            return Task.CompletedTask;
        }

       
        public Task UpdateInventoryAsync(Inventory inventory)
        {
            if(_inventories.Any(x=>x.InventoryId !=  inventory.InventoryId && x.InventoryName.Contains(inventory.InventoryName, StringComparison.OrdinalIgnoreCase)))
            {
                return Task.CompletedTask;
            }

            var invToUpdate = _inventories.FirstOrDefault(i => i.InventoryId == inventory.InventoryId);
            if (invToUpdate != null)
            {
                invToUpdate.InventoryName = inventory.InventoryName;
                invToUpdate.Quantity = inventory.Quantity;
                invToUpdate.Price = inventory.Price;
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Get the inventory by inventory id asynchronously
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns></returns>
        public async Task<Inventory> GetInventoryByIdAsync(int inventoryId)
        {
            return await Task.FromResult(_inventories.First(x => x.InventoryId == inventoryId));
        }

        /// <summary>
        /// Delete the inventory by inventory id
        /// </summary>
        /// <param name="invntoryId"></param>
        /// <returns></returns>
        public Task DeleteInventoryByIdAsync(int invntoryId)
        {
            var inventory = _inventories.FirstOrDefault(x=>x.InventoryId == invntoryId);
            if(inventory != null)
            {
                _inventories.Remove(inventory);
            }
            return Task.CompletedTask;
        }
    }
}
