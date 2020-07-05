﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Mercuryiot.Functions.Models;

namespace Mercuryiot.Functions.Repositories
{
    public interface IClientRepository
    {
        Task<Client> GetClient(string id, string region);

        Task<List<Client>> GetClients(string region);

        Task<bool> InsertClient(Client client);

        Task<bool> UpdateClient(Client client);
    }
}