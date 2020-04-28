using System.Threading.Tasks;
using Mercuryiot.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Mercuryiot.Test.Web.ControllerTests.ControllerTests
{
    public class AccountControllerTests
    {
        private readonly Mock<ILogger<AccountController>> _logger;
        public AccountControllerTests()
        {
            _logger = new Mock<ILogger<AccountController>>();
        }
     
        [Fact]
        public async Task LoginShouldReturnView()
        {
            // Arrange
            var accountController = new AccountController(_logger.Object);
            
            // Act
            var sut = await accountController.LoginAsync();

            // Assert
            Assert.IsType<ViewResult>(sut);
        }
    }
}