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
        private readonly string _id = "ffda6978-72d1-4713-ad1d-76e9b62f7741";
            
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
            client.id = _id;
            client.ttl = -1;

            // Act
            var sut = await _ClientRepository.InsertClient(client);

            // Assert
            Assert.True(sut);
        }

        [Fact(DisplayName = "Given a valid client object, UpdateClient should return true")]
        public async Task UpdateClient_GivenClientObjectParameter_InsertClientReturnTrue()
        {
            // Arrange
            var client = await TestFactory.CreateMockClientAsync();
            client.id = _id;
            client.Name = $"{ client.Name } : { DateTime.UtcNow.Hour }:{ DateTime.UtcNow.Minute }";

            // Act
            var sut = await _ClientRepository.UpdateClient(client);

            // Assert
            Assert.True(sut);
        }

        [Fact(DisplayName = "Given a valid client object, GetClient should return an object od type client.")]
        public async Task GetClient_GivenValidClientKeyParameter_ClientObjectReturned()
        {
            // Arrange
            var region = "West US";

            // Act
            var sut = await _ClientRepository.GetClient(_id, region);

            // Assert
            Assert.IsType<Client>(sut);
        }

    }
}