using System.Threading.Tasks;
using Xunit;
using Mercuryiot.Web.Controllers;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace Mercuryiot.Test.Web.ControllerTests
{
    public class HomeControllerTests
    {
        private readonly Mock<ILogger<HomeController>> _logger;

        public HomeControllerTests()
        {
            _logger = new Mock<ILogger<HomeController>>();
        }

        [Fact(DisplayName = "Home.Index method should return an instance of a ViewRestult.")]
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