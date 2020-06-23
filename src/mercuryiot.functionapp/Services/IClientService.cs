using System.Collections.Generic;
using System.Threading.Tasks;
using Mercuryiot.Functions.Models;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Mercuryiot.Functions.Services
{
    public interface IClientService
    {
        Task<Client> GetClient(string customerKey);

        Task<List<Client>> GetClients(string region);

        Task<Dictionary<string, string>> GetRegions();

        Task<bool> InsertClient(Client client);
    }
}