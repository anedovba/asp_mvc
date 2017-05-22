using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Abstract;
using Moq;
using System.Collections.Generic;
using Domain.Entities;
using WebUI.Controllers;
using System.Linq;
using WebUI.Models;
using WebUI.HtmlHelpers;
using System.Web.Mvc;



namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            // Организация (arrange)
            Mock<ProductRepository> mock = new Mock<ProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product { ProductId = 1, Name = "Shoes1"},
                new Product { ProductId = 2, Name = "Shoes2"},
                new Product { ProductId = 3, Name = "Shoes3"},
                new Product { ProductId = 4, Name = "Shoes4"},
                new Product { ProductId = 5, Name = "Shoes5"}
            });
            ProductsController controller = new ProductsController(mock.Object);
            controller.pageSize = 3;

            // Действие (act)
            ProductsListViewModel result = (ProductsListViewModel)controller.List(null, 2).Model;

            // Утверждение (assert)
            List<Product> products = result.Products.ToList();
            Assert.IsTrue(products.Count == 2);
            Assert.AreEqual(products[0].Name, "Shoes4");
            Assert.AreEqual(products[1].Name, "Shoes5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {

            // Организация - определение вспомогательного метода HTML - это необходимо
            // для применения расширяющего метода
            HtmlHelper myHelper = null;

            // Организация - создание объекта PagingInfo
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            // Организация - настройка делегата с помощью лямбда-выражения
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Действие
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Утверждение
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            // Организация (arrange)
            Mock<ProductRepository> mock = new Mock<ProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
    {
        new Product { ProductId = 1, Name = "Shoes1"},
        new Product { ProductId = 2, Name = "Shoes2"},
        new Product { ProductId = 3, Name = "Shoes3"},
        new Product { ProductId = 4, Name = "Shoes4"},
        new Product { ProductId = 5, Name = "Shoes5"}
    });
            ProductsController controller = new ProductsController(mock.Object);
            controller.pageSize = 3;

            // Act
            ProductsListViewModel result
                = (ProductsListViewModel)controller.List(null, 2).Model;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

        [TestMethod]
        public void Can_Filter_Products()
        {
            // Организация (arrange)
            Mock<ProductRepository> mock = new Mock<ProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
    {
        new Product { ProductId = 1, Name = "Shoes1", Category="Cat1"},
        new Product { ProductId = 2, Name = "Shoes2", Category="Cat2"},
        new Product { ProductId = 3, Name = "Shoes3", Category="Cat1"},
        new Product { ProductId = 4, Name = "Shoes4", Category="Cat2"},
        new Product { ProductId = 5, Name = "Shoes5", Category="Cat3"}
    });
            ProductsController controller = new ProductsController(mock.Object);
            controller.pageSize = 3;

            // Action
            List<Product> result = ((ProductsListViewModel)controller.List("Cat2", 1).Model)
                .Products.ToList();

            // Assert
            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result[0].Name == "Shoes2" && result[0].Category == "Cat2");
            Assert.IsTrue(result[1].Name == "Shoes4" && result[1].Category == "Cat2");
        }

        [TestMethod]
        public void Can_Create_Categories()
        {
            // Организация - создание имитированного хранилища
            Mock<ProductRepository> mock = new Mock<ProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product> {
        new Product { ProductId = 1, Name = "Игра1", Category="Симулятор"},
        new Product { ProductId = 2, Name = "Игра2", Category="Симулятор"},
        new Product { ProductId = 3, Name = "Игра3", Category="Шутер"},
        new Product { ProductId = 4, Name = "Игра4", Category="RPG"},
    });

            // Организация - создание контроллера
            NavController target = new NavController(mock.Object);

            // Действие - получение набора категорий
            List<string> results = ((IEnumerable<string>)target.Menu().Model).ToList();

            // Утверждение
            Assert.AreEqual(results.Count(), 3);
            Assert.AreEqual(results[0], "RPG");
            Assert.AreEqual(results[1], "Симулятор");
            Assert.AreEqual(results[2], "Шутер");
        }

        [TestMethod]
        public void Indicates_Selected_Category()
        {
            // Организация - создание имитированного хранилища
            Mock<ProductRepository> mock = new Mock<ProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
        new Product { ProductId = 1, Name = "Игра1", Category="Симулятор"},
        new Product { ProductId = 2, Name = "Игра2", Category="Шутер"}
    });

            // Организация - создание контроллера
            NavController target = new NavController(mock.Object);

            // Организация - определение выбранной категории
            string categoryToSelect = "Шутер";

            // Действие
            string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

            // Утверждение
            Assert.AreEqual(categoryToSelect, result);
        }

        [TestClass]
        public class ImageTests
        {
            [TestMethod]
            public void Can_Retrieve_Image_Data()
            {
                // Организация - создание объекта Game с данными изображения
                Product product = new Product
                {
                    ProductId = 2,
                    Name = "Игра2",
                    ImageData = new byte[] { },
                    ImageMimeType = "image/png"
                };

                // Организация - создание имитированного хранилища
                Mock<ProductRepository> mock = new Mock<ProductRepository>();
                mock.Setup(m => m.Products).Returns(new List<Product> {
                new Product {ProductId = 1, Name = "Игра1"},
                product,
                new Product {ProductId = 3, Name = "Игра3"}
            }.AsQueryable());

                // Организация - создание контроллера
                ProductsController controller = new ProductsController(mock.Object);

                // Действие - вызов метода действия GetImage()
                ActionResult result = controller.GetImage(2);

                // Утверждение
                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(FileResult));
                Assert.AreEqual(product.ImageMimeType, ((FileResult)result).ContentType);
            }

            [TestMethod]
            public void Cannot_Retrieve_Image_Data_For_Invalid_ID()
            {
                // Организация - создание имитированного хранилища
                Mock<ProductRepository> mock = new Mock<ProductRepository>();
                mock.Setup(m => m.Products).Returns(new List<Product> {
                new Product {ProductId = 1, Name = "Игра1"},
                new Product {ProductId = 2, Name = "Игра2"}
            }.AsQueryable());

                // Организация - создание контроллера
                ProductsController controller = new ProductsController(mock.Object);

                // Действие - вызов метода действия GetImage()
                ActionResult result = controller.GetImage(10);

                // Утверждение
                Assert.IsNull(result);
            }
        }
    }
}
