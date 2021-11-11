using System.Collections.Generic;
using System.Linq;
using Tsak.WebshopProducts_2021_BE.Core.Models;
using Tsak.WebshopProducts_2021_BE.Domain.IRepositories;

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
    }
}