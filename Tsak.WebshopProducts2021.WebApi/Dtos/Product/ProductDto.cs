using System.Collections.Generic;

namespace Tsak.WebshopProducts2021.WebApi.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
    }
}