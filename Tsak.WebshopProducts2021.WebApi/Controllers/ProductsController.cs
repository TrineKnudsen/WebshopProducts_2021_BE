using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Tsak.WebshopProducts_2021_BE.Core.IServices;
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
        
        [HttpGet]
        public ActionResult<ProductsDto> ReadAll()
        {
            try
            {
                var products = _productService.GetAll()
                    .Select(p => new ProductDto
                    {
                        Id = p.Id,
                        Name = p.Name
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
    }
}