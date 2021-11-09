using System.Collections.Generic;
using Moq;
using Tsak.WebshopProducts_2021_BE.Core.IServices;
using Tsak.WebshopProducts_2021_BE.Core.Models;
using Xunit;

namespace Tsak.WebshopProducts_2021_BE.Core.Test.IServices
{
    public class IProductServiceTest
    {
        [Fact]
        public void IProductService_Exists()
        {
            var serviceMock = new Mock<IProductService>();
            Assert.NotNull(serviceMock.Object);
        }

        [Fact]
        public void GetAll_WithNoParams_ReturnsListOfProducts()
        {
            var serviceMock = new Mock<IProductService>();
            serviceMock
                .Setup(s => s.GetAll())
                .Returns(new List<Product>());
            Assert.NotNull(serviceMock.Object.GetAll());
        }
    }
}