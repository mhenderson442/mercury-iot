using System.Collections.Generic;
using System.Threading.Tasks;
using Mercuryiot.Functions.Models;
using Mercuryiot.Functions.Repositories;
using Mercuryiot.Functions.Services;
using Moq;
using Xunit;

namespace Mercuryiot.Test.Functions.ServiceTests
{
    [Trait("Function App Tests", "Client Service Tests")]
    public class ClientServiceTests
    {
        [Fact(DisplayName = "GetClient: Given an invalid customer key, method should return null")]
        public async Task GetClient_GivenInvalidCustomerKey_ShouldReturnNull()
        {
            // Arrange
            var clientRepository = new Mock<IClientRepository>();
            var clientService = new ClientService(clientRepository.Object);

            // Act
            var sut = await clientService.GetClient(It.IsAny<string>(), It.IsAny<string>());

            // Assert
            Assert.True(sut == null);
        }

        [Fact(DisplayName = "GetClient: Given a valid customer key, GetClient repository method should be called.")]
        public async Task GetClient_GivenValidCustomerKey_ShouldCallRepositoryMethod()
        {
            // Arrange
            var clientRepository = new Mock<IClientRepository>();
            clientRepository.Setup(x => x.GetClient(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new Client());

            var clientService = new ClientService(clientRepository.Object);

            // Act
            _ = await clientService.GetClient(It.IsAny<string>(), It.IsAny<string>());

            // Assert
            clientRepository.Verify(x => x.GetClient(It.IsAny<string>(), It.IsAny<string>()));
        }

        [Fact(DisplayName = "GetClient: Given a valid customer key, method should return an object of type Client")]
        public async Task GetClient_GivenValidCustomerKey_ShouldReturnCustomerObject()
        {
            // Arrange
            var clientRepository = new Mock<IClientRepository>();
            clientRepository.Setup(x => x.GetClient(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new Client());

            var clientService = new ClientService(clientRepository.Object);

            // Act
            var sut = await clientService.GetClient(It.IsAny<string>(), It.IsAny<string>());

            // Assert
            Assert.IsType<Client>(sut);
        }

        [Fact(DisplayName = "GetRegions: Given the method is called, should return list of regions.")]
        public async Task GetRegions_Should_ReturnList()
        {
            // Arrange
            var clientRepository = new Mock<IClientRepository>();
            var clientService = new ClientService(clientRepository.Object);

            // Act
            var sut = await clientService.GetRegions();

            // Assert
            Assert.IsType<Dictionary<string, string>>(sut);
        }

        [Fact(DisplayName = "InsertClient: Given a valid client parameters, the InsertClient method should return true.")]
        public async Task InsertClient_GivenValidClientParameter_MethodShouldreturnTrue()
        {
            // Arrange
            var clientRepository = new Mock<IClientRepository>();
            clientRepository.Setup(x => x.InsertClient(It.IsAny<Client>())).ReturnsAsync(true);

            var clientService = new ClientService(clientRepository.Object);

            // Act
            var sut = await clientService.InsertClient(It.IsAny<Client>());

            // Assert
            Assert.True(sut);
            clientRepository.Verify(x => x.InsertClient(It.IsAny<Client>()));
        }

        [Fact(DisplayName = "UpdateClient: Given a valid client parameters, the UpdateClient method should return true.")]
        public async Task UpdateClient_GivenValidClientParameter_MethodShouldreturnTrue()
        {
            // Arrange
            var clientRepository = new Mock<IClientRepository>();
            clientRepository.Setup(x => x.UpdateClient(It.IsAny<Client>())).ReturnsAsync(true);

            var clientService = new ClientService(clientRepository.Object);

            // Act
            var sut = await clientService.UpdateClient(It.IsAny<Client>());

            // Assert
            Assert.True(sut);
            clientRepository.Verify(x => x.UpdateClient(It.IsAny<Client>()));
        }


        [Fact(DisplayName = "GetClients: Given a valid region parameter, the GetClients should return a list of clients")]
        public async Task GetClients_GivenRegionParameter_ReturnsList()
        {
            // Arrange
            var clientRepository = new Mock<IClientRepository>();
            clientRepository.Setup(x => x.GetClients(It.IsAny<string>())).ReturnsAsync(new List<Client>());

            var clientService = new ClientService(clientRepository.Object);

            // Act
            var sut = await clientService.GetClients(It.IsAny<string>());

            // Assert
            Assert.IsType<List<Client>>(sut);
            clientRepository.Verify(x => x.GetClients(It.IsAny<string>()));
        }

        [Fact(DisplayName = "GetClients: Given no parameter, the GetClients should return a list of clients")]
        public async Task GetClients_GivenNoParameter_ReturnsList()
        {
            // Arrange
            var clientRepository = new Mock<IClientRepository>();
            clientRepository.Setup(x => x.GetClients(It.IsAny<string>())).ReturnsAsync(new List<Client>());

            var clientService = new ClientService(clientRepository.Object);

            // Act
            var sut = await clientService.GetClients();

            // Assert
            Assert.IsType<List<Client>>(sut);
            clientRepository.Verify(x => x.GetClients(It.IsAny<string>()),Times.Exactly(8));
        }

    }
}