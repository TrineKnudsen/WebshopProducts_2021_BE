using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Tsak.WebshopProducts2021.WebApi.Dtos;

namespace Tsak.WebshopProducts2021.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<ProductsDto> ReadAll()
        {
            var dto = new ProductsDto();
            dto.List = new List<ProductDto>
            {
                new ProductDto {Id = 1, Name = "Ost"},
                new ProductDto {Id = 2, Name = "OsteKage"},
                new ProductDto {Id = 3, Name = "Brie"}
            };
            return Ok(dto);
        }
    }
}