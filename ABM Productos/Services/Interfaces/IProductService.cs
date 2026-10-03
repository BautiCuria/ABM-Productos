using System.Collections.Generic;
using ABM.Models.DTOs.Requests;
using ABM.Models.DTOs.Responses;

namespace ABM.Services.Interfaces
{
    public interface IProductService
    {
        List<ProductForReadDto> GetAllProducts();
        ProductForReadDto GetProductById(int id);
        ProductForReadDto CreateProduct(ProductForCreateDto dto);
        void UpdateProduct(int id, ProductForUpdateDto dto);
        void DeleteProduct(int id);

        List<ProductForReadDto> SearchProductsByName(string name);
        ProductStatsDto GetStats();
    }
}