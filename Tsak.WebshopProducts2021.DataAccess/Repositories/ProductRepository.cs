using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Tsak.WebshopProducts_2021_BE.Core.Models;
using Tsak.WebshopProducts_2021_BE.Domain.IRepositories;
using Tsak.WebshopProducts2021.DataAccess.Entities;

namespace Tsak.WebshopProducts2021.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
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
            _ctx.SaveChanges();
            
            return new Product
            {
                Id = productEntity.Id,
                Name = productEntity.Name
            };
            
        }


        public void Delete(Product product)
        {
            ProductEntity productEntity = _ctx.Products.FirstOrDefault(p => p.Id == product.Id);
            _ctx.Products.Remove(productEntity);
            _ctx.SaveChanges();

        }

        public Product Update(Product productToUpdate)
        {
            ProductEntity productEntity = _ctx.Products.SingleOrDefault(p => p.Id == productToUpdate.Id);

            if (productEntity != null)
            {
                productEntity.Id = productToUpdate.Id;
                productEntity.Name = productToUpdate.Name;
                _ctx.SaveChanges();
            }

            return productToUpdate;
        }
    }
}