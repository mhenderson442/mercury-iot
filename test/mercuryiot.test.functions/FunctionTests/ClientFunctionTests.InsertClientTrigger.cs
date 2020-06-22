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
        [Fact(DisplayName = "InsertClient: Given an invalid client parameter, the InsertlientTrigger should return an BadRequest response.")]
        public async Task InsertClientTrigger_GivenInvalidParameter_ShouldReturnBadRequest()
        {
            // Arrange
            var clientService = new Mock<IClientService>();
            clientService.Setup(x => x.InsertClient(It.IsAny<Client>())).ReturnsAsync(false);

            var request = await TestFactory.CreateMockRequest(_badClient);
            var clientFunctions = new ClientFunctions(_nullLogger, clientService.Object);

            // Act
            var sut = (BadRequestResult)await clientFunctions.InsertClientTrigger(request.Object);

            // Assert
            Assert.IsType<BadRequestResult>(sut);
        }

        [Fact(DisplayName = "InsertClient: Given a valid client parameter, the InsertlientTrigger should return an Accepted response.")]
        public async Task InsertClientTrigger_GivenValidClientParameter_ShouldReturnOkResponse()
        {
            // Arrange
            var clientService = new Mock<IClientService>();
            clientService.Setup(x => x.InsertClient(It.IsAny<Client>())).ReturnsAsync(true);

            var request = await TestFactory.CreateMockRequest(_goodClient);
            var clientFunctions = new ClientFunctions(_nullLogger, clientService.Object);

            // Act
            var sut = (AcceptedResult)await clientFunctions.InsertClientTrigger(request.Object);

            // Assert
            Assert.IsType<AcceptedResult>(sut);
        }

        [Fact(DisplayName = "InsertClientTrigger: Given the service layer throws an exception, The InsertClientTrigger should return an internal service error response.")]
        [Trait("Category", "Client Function Tests")]
        public async Task InsertClientTrigger_GivenServiceThrowsException_ShouldReturnInternalServerError()
        {
            // Arrange
            var clientService = new Mock<IClientService>();
            clientService.Setup(x => x.InsertClient(It.IsAny<Client>())).Throws(new Exception());

            var request = await TestFactory.CreateMockRequest(_badClient);
            var clientFunctions = new ClientFunctions(_nullLogger, clientService.Object);

            // Act
            var sut = (InternalServerErrorResult)await clientFunctions.InsertClientTrigger(request.Object);

            // Assert
            Assert.IsType<InternalServerErrorResult>(sut);
        }
    }
}