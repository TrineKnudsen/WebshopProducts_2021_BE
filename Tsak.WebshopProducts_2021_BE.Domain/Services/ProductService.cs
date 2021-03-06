using System;
using System.Collections.Generic;
using System.IO;
using Tsak.WebshopProducts_2021_BE.Core.IServices;
using Tsak.WebshopProducts_2021_BE.Core.Models;
using Tsak.WebshopProducts_2021_BE.Domain.IRepositories;

namespace Tsak.WebshopProducts_2021_BE.Domain.Services
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            if (productRepository == null)
            {
                throw new InvalidDataException("ProductRepository cannot be null");
            }

            _productRepository = productRepository;
        }

        public List<Product> GetMyProducts(int userId)
        {
            return _productRepository.ReadMyProducts(userId);
        }

        public List<Product> GetAll()
        {
            return _productRepository.ReadAll();
        }

        public Product Create(Product product)
        {
            return _productRepository.Create(product);
        }
        
        public void Delete(int id)
        {
            _productRepository.Delete(id);
        }
        

        public Product Update(Product productToUpdate)
        {
            return _productRepository.Update(productToUpdate);
        }
    }
}