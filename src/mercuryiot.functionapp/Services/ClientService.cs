using System;
using System.Collections.Generic;
using System.Data;
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

        public async Task<Client> GetClient(string id, string region) => await _clientRepository.GetClient(id, region);

        public async Task<List<Client>> GetClients(string region)
        {
            var clients = await _clientRepository.GetClients(region);

            return clients;
        }

        public async Task<List<Client>> GetClients()
        {
            var clients = new List<Client>();

            foreach (var region in await GetRegions())
            {
                var regionClients = await _clientRepository.GetClients(region.Value);
                clients.AddRange(regionClients);
            }

            return clients;
        }

        public async Task<Dictionary<string, string>> GetRegions()
        {
            await Task.Yield();

            var regions = new Dictionary<string, string>
            {
                { Region.USCentral.Name, "US Central" },
                { Region.USEast.Name, "US East" },
                { Region.USEast2.Name, "US East" },
                { Region.USNorthCentral.Name, "US North Central" },
                { Region.USSouthCentral.Name, "US South Central" },
                { Region.USWest.Name, "US West" },
                { Region.USWest2.Name, "US West 2" },
                { Region.USWestCentral.Name, "US West Central" },
            };

            return regions;
        }

        public async Task<bool> InsertClient(Client client) => await _clientRepository.InsertClient(client);

        public async Task<bool> UpdateClient(Client client) => await _clientRepository.UpdateClient(client);
    }
}