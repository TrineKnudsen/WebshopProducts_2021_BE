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
                    Name = pe.Name,
                    OwnerId = pe.OwnerId
                })
                .ToList();
        }

        public List<Product> ReadMyProducts(int userId)
        {
            return _ctx.Products.Where(p => p.OwnerId == userId)
                .Select(p => new Product()
                {
                    Id = p.Id,
                    Name = p.Name
                }).ToList();
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
        
        public void Delete(int id)
        {
            ProductEntity productEntity = _ctx.Products.FirstOrDefault(p => p.Id == id);
            _ctx.Products.Remove(productEntity);
            _ctx.SaveChanges();

        }

        public Product Update(Product productToUpdate)
        {
            var pe = _ctx.Update(new ProductEntity
                {
                    Id = productToUpdate.Id,
                    Name = productToUpdate.Name,
                    OwnerId = productToUpdate.OwnerId
                }).Entity;
                _ctx.SaveChanges();
                return new Product
                {
                    Id = pe.Id,
                    Name = pe.Name,
                    OwnerId = pe.OwnerId
                };
        }
    }
}