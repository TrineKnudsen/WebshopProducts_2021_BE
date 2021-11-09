﻿using System.Collections.Generic;
using Tsak.WebshopProducts_2021_BE.Core.Models;

namespace Tsak.WebshopProducts_2021_BE.Domain.IRepositories
{
    public interface IProductRepository
    {
        List<Product> ReadAll();
    }
}