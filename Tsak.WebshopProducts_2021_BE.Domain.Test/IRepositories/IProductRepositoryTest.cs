using System.Collections.Generic;
using Moq;
using Tsak.WebshopProducts_2021_BE.Core.Models;
using Tsak.WebshopProducts_2021_BE.Domain.IRepositories;
using Xunit;

namespace Tsak.WebshopProducts_2021_BE.Domain.Test.IRepositories
{
    public class IProductRepositoryTest
    {
        [Fact]
        public void IProductRepository_Exists()
        {
            var repoMock = new Mock<IProductRepository>();
            Assert.NotNull(repoMock.Object);
        }

        [Fact]
        public void ReadAll_WithNoParams_ReturnsListOfProducts()
        {
            var repoMock = new Mock<IProductRepository>();
            repoMock
                .Setup(r => r.ReadAll())
                .Returns(new List<Product>());
            Assert.NotNull(repoMock.Object.ReadAll());
        }
    }
}