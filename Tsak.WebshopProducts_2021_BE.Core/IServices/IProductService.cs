using System;
using System.Collections.Generic;
using Tsak.WebshopProducts_2021_BE.Core.Models;

namespace Tsak.WebshopProducts_2021_BE.Core.IServices
{
    public interface IProductService
    {
        List<Product> GetAll();
        Product Create(Product productDto);
    }
}