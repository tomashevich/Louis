using Louis.Entities;
using Louis.Repositories;
using Louis.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace LouisTests
{
    [TestClass]
    public class ProductServiceTests
    {
        private  ProductService _productService;
        private Mock<IProductRepository<Product>> _repositoryMock;

        public ProductServiceTests()
        {          
        }

        [TestInitialize]
        public void Setup()
        {
            _repositoryMock = new Mock<IProductRepository<Product>>();
            _productService = new ProductService(_repositoryMock.Object);
        }

        [TestMethod]
        public void ProductService_should_return_product_by_Id()
        {
            //Arrange
            var productId = Guid.NewGuid();
            var product = new Product() { Id = productId };
            _repositoryMock.Setup(r => r.GetById(It.Is<Guid>(id => id == productId))).Returns(Task.FromResult(product));

            //Act
            var result = _productService.GetById(productId).Result;

            //Assert
            Assert.AreEqual(product, result);
        }
    }
}
