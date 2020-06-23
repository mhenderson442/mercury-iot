﻿using System;
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
        [Fact(DisplayName = "InsertClient: Given an invalid client parameter, the InsertlientTrigger should return an BadRequest response.")]
        public async Task InsertClientTrigger_GivenInvalidParameter_ShouldReturnBadRequest()
        {
            // Arrange
            var client = await TestFactory.CreateMockClientAsync();
            var clientService = new Mock<IClientService>();
            clientService.Setup(x => x.InsertClient(client)).ReturnsAsync(false);

            var request = await TestFactory.CreateMockRequestAsync(client);
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
            var client = await TestFactory.CreateMockClientAsync();
            var clientService = new Mock<IClientService>();
            clientService.Setup(x => x.InsertClient(It.IsAny<Client>())).ReturnsAsync(true);

            var request = await TestFactory.CreateMockRequestAsync(client);
            var clientFunctions = new ClientFunctions(_nullLogger, clientService.Object);

            // Act
            var sut = (AcceptedResult)await clientFunctions.InsertClientTrigger(request.Object);

            // Assert
            Assert.IsType<AcceptedResult>(sut);
        }

        [Fact(DisplayName = "InsertClientTrigger: Given the service layer throws an exception, The InsertClientTrigger should return an internal service error response.")]
        public async Task InsertClientTrigger_GivenServiceThrowsException_ShouldReturnInternalServerError()
        {
            // Arrange
            var client = await TestFactory.CreateMockClientAsync();
            var clientService = new Mock<IClientService>();
            clientService.Setup(x => x.InsertClient(It.IsAny<Client>())).Throws(new Exception());

            var request = await TestFactory.CreateMockRequestAsync(client);
            var clientFunctions = new ClientFunctions(_nullLogger, clientService.Object);

            // Act
            var sut = (InternalServerErrorResult)await clientFunctions.InsertClientTrigger(request.Object);

            // Assert
            Assert.IsType<InternalServerErrorResult>(sut);
        }
    }
}