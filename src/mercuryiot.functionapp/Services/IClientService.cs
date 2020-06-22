using System.Collections.Generic;
using System.Threading.Tasks;
using Mercuryiot.Functions.Models;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace Mercuryiot.Functions.Services
{
    public interface IClientService
    {
        Task<Client> GetClient(string customerKey);

        Task<bool> InsertClient(Client client);

        Task<List<Region>> GetRegions();
    }
}