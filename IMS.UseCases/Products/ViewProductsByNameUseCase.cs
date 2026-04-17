using IMS.CoreBusiness;
using IMS.UseCases.Products.Interfaces;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases.Products
{
    public class ViewProductsByNameUseCase : IViewProductsByNameUseCase
    {
        private readonly IProductRepository _productRepository;
        public ViewProductsByNameUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> ExecuteAsync(string productName = "")
        {
            return await _productRepository.GetProductsByNameAsync(productName);
        }
    }
}
