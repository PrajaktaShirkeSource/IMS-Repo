using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.Plugins.InMemory
{
    public class ProductRepository: IProductRepository
    {
        private List<Product> _products;
        public ProductRepository()
        {
            _products = new List<Product>()
            {
                new Product{ProductId=1, ProductName="Bike Seat", Quantity=15, Price=2},
                new Product{ProductId=2, ProductName="Bike Body", Quantity=15, Price=15},
                new Product{ProductId=3, ProductName="Bike Wheels", Quantity=30, Price=5},
                new Product{ProductId=4, ProductName="Bike Pedals", Quantity=40, Price=3},
                new Product{ProductId=5, ProductName="Bike Bell", Quantity=20, Price=2},
                new Product{ProductId=6, ProductName="Bike Stopper", Quantity=15, Price=2},
                new Product{ProductId=7, ProductName="Bike Chain", Quantity=23, Price=5}
            };
        }

        /// <summary>
        /// Get all the inventories that contains the entered name
        /// </summary>
        /// <param name="productName">Name entered by user</param>
        /// <returns>List of all the inventories matching with the name entered</returns>
        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
                return await Task.FromResult(_products);

            return _products.Where(x=>x.ProductName.Contains(productName, StringComparison.OrdinalIgnoreCase));
        }


        public Task AddProductAsync(Product product)
        {
            if (_products.Any(i => i.ProductName.Equals(product.ProductName, StringComparison.OrdinalIgnoreCase)))
            {
                return Task.CompletedTask;
            }

            var maxProductId = _products.Max(i => i.ProductId);
            product.ProductId = maxProductId + 1;

            _products.Add(product);

            return Task.CompletedTask;
        }


        public Task UpdateProductAsync(Product product)
        {
            if (_products.Any(x => x.ProductId != product.ProductId && x.ProductName.Contains(product.ProductName, StringComparison.OrdinalIgnoreCase)))
            {
                return Task.CompletedTask;
            }

            var invToUpdate = _products.FirstOrDefault(i => i.ProductId == product.ProductId);
            if (invToUpdate != null)
            {
                invToUpdate.ProductName = product.ProductName;
                invToUpdate.Quantity = product.Quantity;
                invToUpdate.Price = product.Price;
            }
            return Task.CompletedTask;
        }


        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await Task.FromResult(_products.First(x => x.ProductId == productId));
        }
    }
}
