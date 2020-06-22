using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Mercuryiot.Functions.Models;
using Mercuryiot.Functions.Repositories;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace Mercuryiot.Functions.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Client> GetClient(string customerKey) => await _clientRepository.GetClient(customerKey);

        public async Task<List<Region>> GetRegions()
        {
            await Task.Yield();

            var regions = new List<Region>
            {
                Region.USCentral,
                Region.USEast,
                Region.USEast2,
                Region.USNorthCentral, 
                Region.USSouthCentral,
                Region.USWest,
                Region.USWest2,
                Region.USWestCentral
            };

            return regions;
        }

        public async Task<bool> InsertClient(Client client) => await _clientRepository.InsertClient(client);
    }
}