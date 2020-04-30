using System.Threading.Tasks;
using Mercuryiot.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Mercuryiot.Web.Models;

namespace Mercuryiot.Test.Web.ControllerTests.ControllerTests
{
    public class AccountControllerTests
    {
        private readonly Mock<ILogger<AccountController>> _logger;
        public AccountControllerTests()
        {
            _logger = new Mock<ILogger<AccountController>>();
        }
     
        [Theory(DisplayName = "Given a return url, or a null string, Login should return a view.")]
        [InlineData("https://loremipsum.com")]
        [InlineData(null)]
        public async Task GivenReturnUrlParameter_LoginShouldReturnView(string returnUrl)
        {
            // Arrange
            var accountController = new AccountController(_logger.Object);
            
            // Act
            var sut = await accountController.LoginAsync(returnUrl);

            // Assert
            Assert.IsType<ViewResult>(sut);
        }

        [Fact(DisplayName = "Given a LoginViewModel is posted, Login should return a view.")]
        public async Task GivenLoginViewModelIsPosted_LoginShouldReturnView()
        {
            // Arrange
            var returnUrl = "https://loremipsum.com";
            var model = new LoginViewModel();

            var accountController = new AccountController(_logger.Object);
            
            // Act
            var sut = await accountController.LoginAsync(model, returnUrl);

            // Assert
            Assert.IsType<ViewResult>(sut);
        }
    }
}