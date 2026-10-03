using ABM.Entities;
using ABM.Models.DTOs.Requests;
using ABM.Models.DTOs.Responses;
using ABM.Repositories.Implementations;
using ABM.Repositories.Interfaces;
using ABM.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ABM.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }
        public List<ProductForReadDto> GetAllProducts()
        {
            var products = _repository.GetAllProducts();
            return products.Select(p => new ProductForReadDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            }).ToList();
        }

        public ProductForReadDto GetProductById(int id)
        {
            var product = _repository.GetProductById(id);
            if (product == null)
            {
                return null;
            }

            return new ProductForReadDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }

        public ProductForReadDto CreateProduct(ProductForCreateDto dto)
        {
            var nameExists = _repository.GetAllProducts()
                .Any(p => p.Name.Equals(dto.Name, StringComparison.OrdinalIgnoreCase));

            if (nameExists)
            {
                throw new InvalidOperationException("Ya existe un producto con ese nombre.");
            }

            var newProduct = new Product
            {
                Name = dto.Name,
                Price = dto.Price
            };

            _repository.AddProduct(newProduct);

            return new ProductForReadDto
            {
                Id = newProduct.Id,
                Name = newProduct.Name,
                Price = newProduct.Price
            };
        }

        public void UpdateProduct(int id, ProductForUpdateDto dto)
        {
            var product = _repository.GetProductById(id);

            if (product != null)
            {
                var nameExists = _repository.GetAllProducts()
                    .Any(p => p.Id != id && p.Name.Equals(dto.Name, StringComparison.OrdinalIgnoreCase));

                if (nameExists)
                {
                    throw new InvalidOperationException("Ya existe otro producto con ese nombre.");
                }

                product.Name = dto.Name;
                product.Price = dto.Price;

                _repository.UpdateProduct(product);
            }
        }

        public void DeleteProduct(int id)
        {
            _repository.DeleteProduct(id);
        }

        public List<ProductForReadDto> SearchProductsByName(string name)
        {
            var products = _repository.SearchProductsByName(name);

            return products.Select(p => new ProductForReadDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            }).ToList();
        }

        public ProductStatsDto GetStats()
        {
            var allProducts = _repository.GetAllProducts();

            if (allProducts.Count == 0)
            {
                return new ProductStatsDto
                {
                    Total = 0,
                    AveragePrice = 0,
                    MostExpensiveName = "Sin productos"
                };
            }

            return new ProductStatsDto
            {
                Total = allProducts.Count,
                AveragePrice = allProducts.Average(p => p.Price),
                MostExpensiveName = allProducts.OrderByDescending(p => p.Price).First().Name
            };
        }
    }
}