using System.Collections.Generic;
using System.Threading.Tasks;
using Mercuryiot.Functions.Models;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace Mercuryiot.Functions.Services
{
    public interface IClientService
    {
        Task<Client> GetClient(string customerKey);

        Task<List<Client>> GetClients(string region);

        Task<List<Region>> GetRegions();

        Task<bool> InsertClient(Client client);
    }
}