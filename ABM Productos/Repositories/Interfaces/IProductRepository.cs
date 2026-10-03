using System.Collections.Generic;
using ABM.Entities;

namespace ABM.Repositories.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
        List<Product> SearchProductsByName(string name);
    }
}