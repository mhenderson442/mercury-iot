using System;
using System.IO;
using System.Threading.Tasks;
using Mercuryiot.Functions.DataAccess;
using Mercuryiot.Functions.Models;
using Mercuryiot.Functions.Repositories;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Mercuryiot.Test.Functions.RepositoryTests
{
    [Trait("Function App Tests", "Client Repository Tests")]
    public class ClientRepositoryTests
    {
        private readonly IClientRepository _ClientRepository;
        private readonly string _clientKey = "78a68d5a-5b83-4e73-8194-9ebb35da6b0a";
            
        public ClientRepositoryTests()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.development.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();

            var accountEndpoint = configuration.GetSection("AccountEndpoint").Value;
            var accountKey = configuration.GetSection("AccountKey").Value;
            var databaseName = configuration.GetSection("DatabaseName").Value;
            var containerName = configuration.GetSection("ConatainerName").Value;

            var clientContext = new ClientContext(accountEndpoint, accountKey, databaseName, containerName);

            _ClientRepository = new ClientRepository(clientContext);
        }

        [Fact(DisplayName = "Given a valid client object, InsertClient should return true")]
        public async Task InsertClient_GivenClientObjectParameter_InsertClientReturnTrue()
        {
            // Arrange
            var client = await TestFactory.CreateMockClientAsync();

            // Act
            var sut = await _ClientRepository.InsertClient(client);

            // Assert
            Assert.True(sut);
        }


        [Fact]
        public async Task GetClient_GivenValidClientKeyParameter_ClientObjectReturned()
        {
            // Arrange
            // Act
            var sut = await _ClientRepository.GetClient(_clientKey);

            // Assert
            Assert.IsType<Client>(sut);
        }

    }
}