using HW_21_Unit_Tests.Controllers;
using HW_21_Unit_Tests.Interfaces;
using HW_21_Unit_Tests.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace _OrderTestApp_.Tests
{
    // Packages:

    // Install-Package Microsoft.NET.Test.Sdk
    // Install-Package xunit
    // Install-Package xunit.runner.visualstudio
    // Install-Package Moq 		

    public  class OrderControllerTests
    {
        private List<Order> GetTestOrders()
        {
            var orders = new List<Order>
            {
                new Order { Id = 1, CustomerName = "Tom", TotalAmount = 100 },
                new Order { Id = 2, CustomerName = "John", TotalAmount = 200 },
                new Order { Id = 3, CustomerName = "Jim", TotalAmount = 300 },
            };
            return orders;
        }

        // Arrange
        // Act
        // Assert

        [Fact]
        public void CreateOrder_ReturnsNotNull()
        {
            // Arrange
            var mock = new Mock<IOrder>();
            var newOrder = new Order { Id = 4, CustomerName = "Jimbo", TotalAmount = 400 };
            mock.Setup(repo => repo.CreateOrder(newOrder)).Returns(newOrder);
            var controller = new OrderController(mock.Object);

            // Act
            var result = controller.CreateOrder(newOrder);

            // Assert
            Assert.NotNull(result.Result);
        }


        [Fact]
        public void DeleteOrderReturnsNoContent()
        {
            // Arrange
            var mock = new Mock<IOrder>();
            mock.Setup(repo => repo.DeleteOrder(1)).Returns(true);
            var controller = new OrderController(mock.Object);

            // Act
            var result = controller.DeleteOrder(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void GetOrdersReturnsAllOrders()
        {
            // Arrange
            var mock = new Mock<IOrder>();
            mock.Setup(repo => repo.GetAllOrders()).Returns(GetTestOrders());
            var controller = new OrderController(mock.Object);
            // Act
            var result = controller.GetOrders();
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<Order>>(okResult.Value);
            Assert.Equal(GetTestOrders().Count, model.Count());
        }

        [Fact]
        public void GetOrderReturnsSingleOrder()
        {
            // Arrange
            var mock = new Mock<IOrder>();
            mock.Setup(repo => repo.GetOrderById(2))
                .Returns(GetTestOrders().FirstOrDefault(o => o.Id == 2));
            var controller = new OrderController(mock.Object);

            // Act
            var result = controller.GetOrder(2);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedOrder = Assert.IsType<Order>(okResult.Value);
            Assert.Equal("John", returnedOrder.CustomerName);
        }

        [Fact]
        public void GetOrderReturnsNotFound()
        {
            // Arrange
            var mock = new Mock<IOrder>();
            mock.Setup(repo => repo.GetOrderById(999))
                  .Returns(null as Order);
            var controller = new OrderController(mock.Object);

            // Act
            var result = controller.GetOrder(999);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
