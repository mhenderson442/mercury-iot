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
    [Trait("Function App Tests", "Client Function Tests")]
    public partial class ClientFunctionTests
    {

        [Fact(DisplayName = "UpdateClient: Given an invalid client parameter, the UpdateClientTrigger should return an BadRequest response.")]
        public async Task UpdateClientTrigger_GivenInvalidParameter_ShouldReturnBadRequest()
        {
            // Arrange
            var client = await TestFactory.CreateMockClientAsync();
            var clientService = new Mock<IClientService>();
            clientService.Setup(x => x.UpdateClient(client)).ReturnsAsync(false);

            var request = await TestFactory.CreateMockRequestAsync(client);
            var clientFunctions = new ClientFunctions(_nullLogger, clientService.Object);

            // Act
            var sut = (BadRequestResult)await clientFunctions.UpdateClientTrigger(request.Object);

            // Assert
            Assert.IsType<BadRequestResult>(sut);
        }

        [Fact(DisplayName = "UpdateClient: Given a valid client parameter, the UpdateClientTrigger should return an Accepted response.")]
        public async Task UpdateClientTrigger_GivenValidClientParameter_ShouldReturnOkResponse()
        {
            // Arrange
            var client = await TestFactory.CreateMockClientAsync();
            var clientService = new Mock<IClientService>();
            clientService.Setup(x => x.UpdateClient(It.IsAny<Client>())).ReturnsAsync(true);

            var request = await TestFactory.CreateMockRequestAsync(client);
            var clientFunctions = new ClientFunctions(_nullLogger, clientService.Object);

            // Act
            var sut = (AcceptedResult)await clientFunctions.UpdateClientTrigger(request.Object);

            // Assert
            Assert.IsType<AcceptedResult>(sut);
        }

        [Fact(DisplayName = "UpdateClient: Given the service layer throws an exception, The UpdateClientTrgger should return an internal service error response.")]
        public async Task UpdateClientTrigger_GivenServiceThrowsException_ShouldReturnInternalServerError()
        {
            // Arrange
            var client = await TestFactory.CreateMockClientAsync();
            var clientService = new Mock<IClientService>();
            clientService.Setup(x => x.UpdateClient(It.IsAny<Client>())).Throws(new Exception());

            var request = await TestFactory.CreateMockRequestAsync(client);
            var clientFunctions = new ClientFunctions(_nullLogger, clientService.Object);

            // Act
            var sut = (InternalServerErrorResult)await clientFunctions.UpdateClientTrigger(request.Object);

            // Assert
            Assert.IsType<InternalServerErrorResult>(sut);
        }
    }
}