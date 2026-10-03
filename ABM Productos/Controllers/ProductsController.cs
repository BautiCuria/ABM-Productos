using ABM.Models.DTOs.Requests;
using ABM.Models.DTOs.Responses;
using ABM.Services.Implementations;
using ABM.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ABM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _service.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _service.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet("search")]
        public IActionResult SearchProductsByName([FromQuery] string name)
        {
            var results = _service.SearchProductsByName(name);
            return Ok(results);
        }

        [HttpGet("stats")]
        public IActionResult GetStats()
        {
            var stats = _service.GetStats();
            return Ok(stats);
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductForCreateDto dto)
        {
            try
            {
                var createdProduct = _service.CreateProduct(dto);
                return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductForUpdateDto dto)
        {
            var existingProduct = _service.GetProductById(id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            try
            {
                _service.UpdateProduct(id, dto);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var existingProduct = _service.GetProductById(id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            _service.DeleteProduct(id);
            return NoContent();
        }
    }
}