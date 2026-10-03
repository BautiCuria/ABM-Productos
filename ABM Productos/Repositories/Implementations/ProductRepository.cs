using ABM.Repositories.Interfaces;
using ABM.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ABM.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private static List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Mouse inalámbrico", Price = 3999.99m }
    };

        public List<Product> GetAllProducts()
        {
            return _products;
        }

        public Product? GetProductById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public void AddProduct(Product product)
        {
            if (_products.Count == 0)
            {
                product.Id = 1;
            }
            else
            {
                product.Id = _products.Max(p => p.Id) + 1;
            }

            _products.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            var existingProduct = GetProductById(product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
            }
        }

        public void DeleteProduct(int id)
        {
            var product = GetProductById(id);
            if (product != null)
            {
                _products.Remove(product);
            }
        }

        public List<Product> SearchProductsByName(string name)
        {
            return _products.Where(p => p.Name.Contains(name, System.StringComparison.OrdinalIgnoreCase)).ToList();
        }

    } 
} 