using System;
using System.Threading.Tasks;
using System.Web.Http;
using Mercuryiot.Functions;
using Mercuryiot.Functions.Models;
using Mercuryiot.Functions.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Mercuryiot.Test.Functions.FunctionTests
{
    public partial class ClientFunctionTests
    {
        [Fact(DisplayName = "GetClientTrigger: Given a client key parameter, The GetClientTrigger should return an OK Result with an object of type Client.")]
        public async Task ClientTriggerReturnsOkWithClientObject()
        {
            // Arrange
            var clientService = new Mock<IClientService>();
            clientService.Setup(x => x.GetClient(_customerKey)).ReturnsAsync(new Client());

            var request = await TestFactory.CreateHttpRequest("customerKey", _customerKey);
            var clientFunctions = new ClientFunctions(_nullLogger, clientService.Object);

            // Act
            var response = (OkObjectResult)await clientFunctions.GetClientTrigger(request);
            var sut = (Client)response.Value;

            // Assert
            Assert.IsType<Client>(sut);
        }

        [Fact(DisplayName = "GetClientTrigger: Given an invalid parameter, The GetClientTrigger should return a bad request response.")]
        public async Task GetClientTrigger_GivenInvalidParameter_ShouldRetrunBadRequest()
        {
            // Arrange
            var clientService = new Mock<IClientService>();
            clientService.Setup(x => x.GetClient(_customerKey)).ReturnsAsync(new Client());

            var request = await TestFactory.CreateHttpRequest("customerKey", null);
            var clientFunctions = new ClientFunctions(_nullLogger, clientService.Object);

            // Act
            var sut = (BadRequestResult)await clientFunctions.GetClientTrigger(request);

            // Assert
            Assert.IsType<BadRequestResult>(sut);
        }

        [Fact(DisplayName = "GetClientTrigger: Given the service layer returns null, the GetClientTrigger should return a bad request response.")]
        public async Task GetClientTrigger_GivenServiceReturnsNull_ShouldReturnBadRequest()
        {
            // Arrange
            var clientService = new Mock<IClientService>();
            clientService.Setup(x => x.GetClient(_customerKey)).ReturnsAsync(new Client());

            var request = await TestFactory.CreateHttpRequest("customerKey", _badCustomerKey);
            var clientFunctions = new ClientFunctions(_nullLogger, clientService.Object);

            // Act
            var sut = (BadRequestResult)await clientFunctions.GetClientTrigger(request);

            // Assert
            Assert.IsType<BadRequestResult>(sut);
        }

        [Fact(DisplayName = "GetClientTrigger: Given the service layer throws an exception, The GetClientTrigger should return an internal service error response.")]
        [Trait("Category", "Client Function Tests")]
        public async Task GetClientTrigger_GivenServiceThrowsException_ShouldReturnInternalServerError()
        {
            // Arrange
            var clientService = new Mock<IClientService>();
            clientService.Setup(x => x.GetClient(_errorInput)).Throws(new Exception());

            var request = await TestFactory.CreateHttpRequest("customerKey", _errorInput);
            var clientFunctions = new ClientFunctions(_nullLogger, clientService.Object);

            // Act
            var sut = (InternalServerErrorResult)await clientFunctions.GetClientTrigger(request);

            // Assert
            Assert.IsType<InternalServerErrorResult>(sut);
        }

        [Fact(DisplayName = "GetClientTrigger: Given a client key parameter, The GetClientTrigger should call the client service method GetClient.")]
        public async Task GetClientTrigger_GivenValidParamer_CallsServiceMethod()
        {
            // Arrange
            var clientService = new Mock<IClientService>();
            clientService.Setup(x => x.GetClient(_customerKey)).ReturnsAsync(new Client());

            var request = await TestFactory.CreateHttpRequest("customerKey", _customerKey);
            var clientFunctions = new ClientFunctions(_nullLogger, clientService.Object);

            // Act
            _ = (OkObjectResult)await clientFunctions.GetClientTrigger(request);

            // Assert
            clientService.Verify(x => x.GetClient(_customerKey));
        }
    }
}