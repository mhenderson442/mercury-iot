using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mercuryiot.Functions.Models;
using Mercuryiot.Functions.Repositories;
using Mercuryiot.Functions.Services;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Moq;
using Xunit;

namespace Mercuryiot.Test.Functions.ServiceTests
{
    [Trait("Function App Tests", "Client Service Tests")]
    public class ClientServiceTests
    {
        private readonly string _badCustomerKey;
        private readonly Mock<IClientRepository> _clientRepository;
        private readonly ClientService _clientService;
        private readonly string _customerKey;

        public ClientServiceTests()
        {
            _customerKey = Guid.NewGuid().ToString();
            _badCustomerKey = "Bad Customer Key";

            _clientRepository = new Mock<IClientRepository>();
            _clientRepository.Setup(x => x.GetClient(_customerKey)).ReturnsAsync(new Client());
            _clientRepository.Setup(x => x.InsertClient(It.IsAny<Client>())).ReturnsAsync(true);

            _clientService = new ClientService(_clientRepository.Object);
        }

        [Fact(DisplayName = "GetClient: Given an invalid customer key, method should return null")]
        public async Task GetClient_GivenInvalidCustomerKey_ShouldReturnNull()
        {
            // Arrange
            // Act
            var sut = await _clientService.GetClient(_badCustomerKey);

            // Assert
            Assert.True(sut == null);
        }

        [Fact(DisplayName = "GetClient: Given a valid customer key, GetClient repository method should be called.")]
        public async Task GetClient_GivenValidCustomerKey_ShouldCallRepositoryMethod()
        {
            // Arrange

            // Act
            _ = await _clientService.GetClient(_customerKey);

            // Assert
            _clientRepository.Verify(x => x.GetClient(_customerKey));
        }

        [Fact(DisplayName = "GetClient: Given a valid customer key, method should return an object of type Client")]
        public async Task GetClient_GivenValidCustomerKey_ShouldReturnCustomerObject()
        {
            // Arrange

            // Act
            var sut = await _clientService.GetClient(_customerKey);

            // Assert
            Assert.IsType<Client>(sut);
        }

        [Fact(DisplayName = "GetRegions: Given the method is called, should return list of regions.")]
        public async Task GetRegions_Should_ReturnList()
        {
            // Arrange
            // Act
            var sut = await _clientService.GetRegions();

            // Assert
            Assert.IsType<Dictionary<string, string>>(sut);
        }

        [Fact(DisplayName = "InsertClient: Given a valid client parameters, the InsertClient method should return true.")]
        public async Task InsertClient_GivenValidClientParameter_MethodShouldreturnTrue()
        {
            // Arrange
            var client = await TestFactory.CreateMockClientAsync();

            // Act
            var sut = await _clientService.InsertClient(client);

            // Assert
            Assert.True(sut);
            _clientRepository.Verify(x => x.InsertClient(It.IsAny<Client>()));
        }
    }
}