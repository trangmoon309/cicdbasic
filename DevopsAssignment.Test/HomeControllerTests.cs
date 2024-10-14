using DevopsAssignment.Controllers;
using DevopsAssignment.Database;
using DevopsAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace DevopsAssignment.Tests
{
    public class HomeControllerTests
    {
        private readonly ILogger<HomeController> _mockLogger;
        private readonly HomeController _controller;
        private readonly MyDbContext _dbContext;

        public HomeControllerTests()
        {
            // Set up the in-memory database
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dbContext = new MyDbContext(options);
            _mockLogger = new LoggerFactory().CreateLogger<HomeController>();

            // Instantiate the controller with the in-memory DbContext
            _controller = new HomeController(_mockLogger, _dbContext);
        }

        [Fact]
        public void Index_ReturnsViewWithAllProducts_When20OrMoreProducts()
        {
            // Arrange
            for (int i = 1; i <= 20; i++)
            {
                _dbContext.Products.Add(new Product { Name = $"Product {i}", ImageUrl = $"images/product{i}.png" });
            }
            _dbContext.SaveChanges();

            // Act
            var result = _controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = Assert.IsAssignableFrom<List<Product>>(result.Model);
            Assert.Equal(20, model.Count); // Should contain 20 products
            Assert.DoesNotContain(model, p => p.Name == "dog"); // Should not include the added dog product
        }

        [Fact]
        public void Privacy_ReturnsView()
        {
            // Act
            var result = _controller.Privacy() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Error_ReturnsViewWithErrorViewModel()
        {
            // Act
            var result = _controller.Error() as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = Assert.IsAssignableFrom<ErrorViewModel>(result.Model);
            Assert.NotNull(model.RequestId);
        }
    }
}
