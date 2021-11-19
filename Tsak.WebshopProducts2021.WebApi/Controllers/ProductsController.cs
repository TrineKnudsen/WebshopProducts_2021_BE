using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Tsak.WebshopProducts_2021_BE.Core.IServices;
using Tsak.WebshopProducts_2021_BE.Core.Models;
using Tsak.WebshopProducts2021.WebApi.Dtos;

namespace Tsak.WebshopProducts2021.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        
        [Authorize(Policy = "ProductsReader")]
        [HttpGet]
        public ActionResult<ProductsDto> ReadAll()
        {
            try
            {
                var products = _productService.GetAll()
                    .Select(p => new ProductDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        OwnerId = p.OwnerId
                    })
                    .ToList();
                return Ok(new ProductsDto
                {
                    List = products
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public ActionResult<ProductDto> Create([FromBody] Product product)
        {
            if (string.IsNullOrEmpty(product.Name))
            {
                return BadRequest("Name is required");
            }
            _productService.Create(product);
            ProductDto productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name
            };
            return productDto;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _productService.Delete(id);
        }
        
        [Authorize(Policy = "ProductsManager")]
        [HttpPut("{id}")]
        public ActionResult<Product> Update(int id, ProductDto productDtoToUpdate)
        {
            if (id != productDtoToUpdate.Id)
            {
                return BadRequest("Ids dont match");
            }
            var product = _productService.Update(new Product
            {
                Id = productDtoToUpdate.Id,
                Name = productDtoToUpdate.Name,
                OwnerId = productDtoToUpdate.OwnerId
            });
            return Ok(productDtoToUpdate);
        }
    }
}