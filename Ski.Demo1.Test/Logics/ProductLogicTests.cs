using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ski.Demo1.Data;
using Ski.Demo1.Domain;
using System.Collections.Generic;

namespace Ski.Demo1.Business.Tests
{
    [TestClass()]
    public class ProductLogicTests
    {
        private Product product1 = null;
        private Product product2 = null;
        private List<Product> products = null;

        private ProductLogic productService = null;
        private DummyUnitOfWorks uow = null;

        public ProductLogicTests()
        {
            // Lets create some sample object
            product1 = new Product
            {
                id = "0001",
                title = "A1",
                category = "CAR",
                origin_price = 1000,
                price = 800,
                unit = "元",
                imageUrl = "",
                description = "TEST1",
                content = "TEST1",
                is_enabled = 1
            };
            product2 = new Product
            {
                id = "0002",
                title = "A2",
                category = "CAR",
                origin_price = 300,
                price = 200,
                unit = "元",
                imageUrl = "",
                description = "TEST2",
                content = "TEST2",
                is_enabled = 1
            };

            products = new List<Product>
            {
                product1,
                product2
            };

            // Let us now create the Unit of work with our dummy UnitOfWork
            uow = new DummyUnitOfWorks(products);
            productService = new ProductLogic(uow);
        }

        [TestMethod]
        public void Create_InputEntity_ReturnTrue()
        {
            // arrange
            var request = new ProductRequest
            {
                data = new ProductDTO
                {
                    title = "A3",
                    category = "CAR",
                    origin_price = 200,
                    price = 100,
                    unit = "元",
                    imageUrl = "",
                    imagesUrl = new List<string>() { "Url1", "Url2" },
                    description = "TEST3",
                    content = "TEST3",
                    is_enabled = 1
                }
            };

            // act
            var actual = productService.Create(request);

            // assert
            Assert.IsTrue(actual.success);
        }

        [TestMethod]
        public void Delete_Input0001_ReturnTrue()
        {
            // arrange
            var id = "0001";

            // act
            var actual = productService.Delete(id);

            // assert
            Assert.IsTrue(actual.success);
        }

        [TestMethod]
        public void Update_Input0001_ReturnTrue()
        {
            // arrange
            var id = "0001";
            var request = new ProductRequest
            {
                data = new ProductDTO
                {
                    title = "A3",
                    category = "CAR",
                    origin_price = 200,
                    price = 100,
                    unit = "元",
                    imageUrl = "",
                    imagesUrl = new List<string>() { "Url1", "Url2" },
                    description = "TEST3",
                    content = "TEST3",
                    is_enabled = 1
                }
            };

            // act
            var actual = productService.Update(request, id);

            // assert
            Assert.IsTrue(actual.success);
        }

        [TestMethod]
        public void GetList_nothing_ReturnTrue()
        {
            // arrange

            // act
            var actual = productService.GetList();

            // assert
            Assert.IsTrue(actual.success);
        }

        [TestMethod()]
        public void GetList_Input1_ReturnTrue()
        {
            // arrange
            var page = 1;

            // act
            var actual = productService.GetList(page);

            // assert
            Assert.IsTrue(actual.success);
        }

        [TestMethod()]
        public void Get_Input0001_ReturnTrue()
        {
            // arrange
            var id = "0001";

            // act
            var actual = productService.Get(id);

            // assert
            Assert.IsTrue(actual.success);
        }
    }
}