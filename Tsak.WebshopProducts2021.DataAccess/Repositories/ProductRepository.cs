using System.Collections.Generic;
using System.Linq;
using Tsak.WebshopProducts_2021_BE.Core.Models;
using Tsak.WebshopProducts_2021_BE.Domain.IRepositories;
using Tsak.WebshopProducts2021.DataAccess.Entities;

namespace Tsak.WebshopProducts2021.DataAccess.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly MainDBContext _ctx;
        public ProductRepository(MainDBContext ctx)
        {
            _ctx = ctx;
        }
        public List<Product> ReadAll()
        {
            return _ctx.Products
                .Select(pe => new Product
                {
                    Id = pe.Id,
                    Name = pe.Name
                })
                .ToList();
        }

        public Product Create(Product product)
        {
            ProductEntity productEntity = new ProductEntity
            {
                Id = product.Id,
                Name = product.Name
            };
            _ctx.Products.Add(productEntity);

            return new Product
            {
                Id = productEntity.Id,
                Name = productEntity.Name
            };
        }
    }
}