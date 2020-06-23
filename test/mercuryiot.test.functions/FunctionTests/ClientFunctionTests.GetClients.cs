using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Mercuryiot.Functions;
using Mercuryiot.Functions.Models;
using Mercuryiot.Functions.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Moq;
using Xunit;

namespace Mercuryiot.Test.Functions.FunctionTests
{
    public partial class ClientFunctionTests
    {
        [Fact(DisplayName = "GetClientsTrigger: Given a region parameter, The GetClientsTrigger should return an OK Result with a list of type Client.")]
        public async Task ClientsTriggerReturnsOkWithClientObject()
        {
            // Arrange
            var region = Region.USNorthCentral.Name;
            var clientService = new Mock<IClientService>();
            clientService.Setup(x => x.GetClients(region)).ReturnsAsync(new List<Client>());

            var request = await TestFactory.CreateHttpRequest("region", region);
            var clientFunctions = new ClientFunctions(_nullLogger, clientService.Object);

            // Act
            var response = (OkObjectResult)await clientFunctions.GetClientsTrigger(request);
            var sut = response.Value as List<Client>;

            // Assert
            Assert.IsType<List<Client>>(sut);
        }

        [Fact(DisplayName = "GetClientsTrigger: Given an invalid parameter, The GetClientsTrigger should return a bad request response.")]
        public async Task GetClientsTrigger_GivenInvalidParameter_ShouldRetrunBadRequest()
        {
            // Arrange
            var region = Region.USNorthCentral.Name;
            var clientService = new Mock<IClientService>();
            clientService.Setup(x => x.GetClients(region)).ReturnsAsync(new List<Client>());

            var request = await TestFactory.CreateHttpRequest("region", null);
            var clientFunctions = new ClientFunctions(_nullLogger, clientService.Object);

            // Act
            var sut = (BadRequestResult)await clientFunctions.GetClientTrigger(request);

            // Assert
            Assert.IsType<BadRequestResult>(sut);
        }

        [Fact(DisplayName = "GetClientsTrigger: Given the service layer returns null, the GetClientsTrigger should return a bad request response.")]
        public async Task GetClientsTrigger_GivenServiceReturnsNull_ShouldReturnBadRequest()
        {
            // Arrange
            var region = Region.USNorthCentral.Name;
            var clientService = new Mock<IClientService>();

            var request = await TestFactory.CreateHttpRequest("region", region);
            var clientFunctions = new ClientFunctions(_nullLogger, clientService.Object);

            // Act
            var sut = (BadRequestResult)await clientFunctions.GetClientTrigger(request);

            // Assert
            Assert.IsType<BadRequestResult>(sut);
        }

        [Fact(DisplayName = "GetClientTrigger: Given the service layer throws an exception, The GetClientTrigger should return an internal service error response.")]
        [Trait("Category", "Client Function Tests")]
        public async Task GetClientsTrigger_GivenServiceThrowsException_ShouldReturnInternalServerError()
        {
            // Arrange
            var region = Region.USNorthCentral.Name;
            var clientService = new Mock<IClientService>();
            clientService.Setup(x => x.GetClients(region)).Throws(new Exception());

            var request = await TestFactory.CreateHttpRequest("region", region);
            var clientFunctions = new ClientFunctions(_nullLogger, clientService.Object);

            // Act
            var sut = (InternalServerErrorResult)await clientFunctions.GetClientsTrigger(request);

            // Assert
            Assert.IsType<InternalServerErrorResult>(sut);
        }

        [Fact(DisplayName = "GetClientsTrigger: Given a client key parameter, The GetClientsTrigger should call the client service method GetClients.")]
        public async Task GetClientsTrigger_GivenValidParamer_CallsServiceMethod()
        {
            // Arrange
            var region = Region.USNorthCentral.Name;
            var clientService = new Mock<IClientService>();
            clientService.Setup(x => x.GetClients(region)).ReturnsAsync(new List<Client>());

            var request = await TestFactory.CreateHttpRequest("region", region);
            var clientFunctions = new ClientFunctions(_nullLogger, clientService.Object);

            // Act
            _ = (OkObjectResult)await clientFunctions.GetClientsTrigger(request);

            // Assert
            clientService.Verify(x => x.GetClients(region));
        }
    }
}