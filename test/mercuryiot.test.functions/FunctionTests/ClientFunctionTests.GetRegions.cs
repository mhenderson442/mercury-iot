using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Mercuryiot.Functions;
using Mercuryiot.Functions.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Mercuryiot.Test.Functions.FunctionTests
{
    [Trait("Function App Tests", "Client Function Tests")]
    public partial class ClientFunctionTests
    {
        [Fact(DisplayName = "GetRegionsTrigger: Given the service layer throws an exception, The GetRegions should return an internal service error response.")]
        public async Task GetRegionsTrigger_GivenServiceThrowsException_ShouldReturnInternalServerError()
        {
            // Arrange
            var clientService = new Mock<IClientService>();
            clientService.Setup(x => x.GetRegions()).Throws(new Exception());

            var request = await TestFactory.CreateMockRequestAsync();
            var clientFunctions = new ClientFunctions(_nullLogger, clientService.Object);

            // Act
            var sut = (InternalServerErrorResult)await clientFunctions.GetRegionsTrigger(request.Object);

            // Assert
            Assert.IsType<InternalServerErrorResult>(sut);
        }

        [Fact(DisplayName = "GetRegionsTrigger: Given a function is called, should return an OK Result with a list of type Region.")]
        public async Task GetRegionsTriggerReturnsOkWithRegionsObject()
        {
            // Arrange
            var clientService = new Mock<IClientService>();
            clientService.Setup(x => x.GetRegions()).ReturnsAsync(new Dictionary<string, string>());

            var request = await TestFactory.CreateMockRequestAsync();
            var clientFunctions = new ClientFunctions(_nullLogger, clientService.Object);

            // Act
            var response = (OkObjectResult)await clientFunctions.GetRegionsTrigger(request.Object);
            var sut = response.Value as Dictionary<string, string>;

            // Assert
            Assert.IsType<Dictionary<string, string>>(sut);
        }
    }
}