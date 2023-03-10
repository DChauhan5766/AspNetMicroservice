﻿using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository productRepository, ILogger<CatalogController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult< IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepository.GetProducts();
            return Ok(products);

        }

        [HttpGet("{id:length(24)}", Name="GetProduct")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsById(string id)
        {
            var product = await _productRepository.GetProduct(id);
            if(product == null)
            {
                _logger.LogError($"Product with id:{id}, not found");
                return NotFound();
            }
            return Ok(product);

        }

        [Route("[action]/{category}", Name= "GetProductsByCategory")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string name)
        {
            var product = await _productRepository.GetProductByCategory(name);
            if (product == null)
            {
                _logger.LogError($"Product with CategoryName:{name}, not found");
                return NotFound();
            }
            return Ok(product);

        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> CreateProduct([FromBody] Product product)
        {
            await _productRepository.CreateProduct(product);
            return CreatedAtRoute("GetProduct", new {id= product.Id }, product );

        }

        [HttpPut]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            return Ok( await _productRepository.UpdateProduct(product));

        }

        [HttpDelete("{id:length(24)}",Name = "DeleteProduct")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProductById(string id)
        {
            return Ok(await _productRepository.DeleteProduct(id));

        }


    }
}
