using System.Threading.Tasks;
using Mercuryiot.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Mercuryiot.Test.Web.ControllerTests
{
    public class HomeControllerTests
    {
        private readonly Mock<ILogger<HomeController>> _logger;

        public HomeControllerTests()
        {
            _logger = new Mock<ILogger<HomeController>>();
        }

        [Fact(DisplayName = "Home.Index method should return an instance of a ViewResult.")]
        public async Task IndexReturnsView()
        {
            // Arrange
            var homeController = new HomeController(_logger.Object);

            // Act
            var sut = await homeController.Index();

            // Assert
            Assert.IsType<ViewResult>(sut);
        }
    }
}