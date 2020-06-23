using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Http;
using Mercuryiot.Functions.Models;
using Mercuryiot.Functions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Mercuryiot.Functions
{
    public class ClientFunctions
    {
        private readonly IClientService _clientService;
        private readonly ILogger<ClientFunctions> _logger;

        public ClientFunctions(ILogger<ClientFunctions> logger, IClientService clientService)
        {
            _logger = logger;
            _clientService = clientService;
        }

        [FunctionName("GetClientsTrigger")]
        public async Task<IActionResult> GetClientsTrigger([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req)
        {
            string region = req.Query["region"];

            if (string.IsNullOrEmpty(region))
            {
                _logger.LogWarning($"GetClientsTrigger :: region was null or empty");
                return new BadRequestResult();
            }

            try
            {
                _logger.LogInformation("GetClientTrigger :: Getting client object from service layer.");
                var clients = await _clientService.GetClients(region);

                if (clients != null)
                {
                    _logger.LogInformation($"GetClientTrigger :: Client object found. Customer Key: { region }.");
                    return new OkObjectResult(clients);
                }

                _logger.LogWarning($"GetClientTrigger :: Query did not return an object. Customer Key: { region }.");
                return new BadRequestResult();
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"GetClientTrigger :: Attempting to get data for client threw an exception. Customer Key: { region }");
                _logger.LogError($"GetClientTrigger Error Message :: { ex.Message }");

                return new InternalServerErrorResult();
            }
        }

        [FunctionName("GetClientTrigger")]
        public async Task<IActionResult> GetClientTrigger([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            string id = req.Query["id"];
            string region = req.Query["region"];

            if (string.IsNullOrEmpty(id))
            {
                _logger.LogWarning($"GetClientTrigger :: Customer key was null or empty");
                return new BadRequestResult();
            }

            try
            {
                _logger.LogInformation("GetClientTrigger :: Getting client object from service layer.");
                var client = await _clientService.GetClient(id, region);

                if (client is Client)
                {
                    _logger.LogInformation($"GetClientTrigger :: Client object found. Client id: { id }.");
                    return new OkObjectResult(client);
                }

                _logger.LogWarning($"GetClientTrigger :: Query did not return an object. Client id: { id }.");
                return new BadRequestResult();
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"GetClientTrigger :: Attempting to get data for client threw an exception. Client id: { id }");
                _logger.LogError($"GetClientTrigger Error Message :: { ex.Message }");

                return new InternalServerErrorResult();
            }
        }

        [FunctionName("GetRegionsTrigger")]
        public async Task<IActionResult> GetRegionsTrigger([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            try
            {
                var regions = await _clientService.GetRegions();
                return new OkObjectResult(regions);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"GetRegionsTrigger :: Unable to get regions data from server.");
                _logger.LogError($"GetRegionsTrigger Error Message :: { ex.Message }");

                return new InternalServerErrorResult();
            }
        }
        [FunctionName("InsertClientTrigger")]
        public async Task<IActionResult> InsertClientTrigger([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
        {
            var client = await JsonSerializer.DeserializeAsync<Client>(req.Body);

            try
            {
                if (client is Client)
                {
                    _logger.LogInformation("InsertClientTrigger :: Inserting client object from service layer.");
                    var accepted = await _clientService.InsertClient(client);

                    if (accepted)
                    {
                        _logger.LogInformation($"InsertClientTrigger :: Insert accepted. Client id: { client.id }.");
                        return new AcceptedResult();
                    }

                    return new BadRequestResult();
                }

                _logger.LogWarning($"InsertClientTrigger :: Unable to insert client. Client id: { client.id }.");
                return new BadRequestResult();
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"InsertClientTrigger :: Attempting to get data for client threw an exception. Client id: { client.id }");
                _logger.LogError($"InsertClientTrigger Error Message :: { ex.Message }");

                return new InternalServerErrorResult();
            }
        }

        [FunctionName("UpdateClientTrigger")]
        public async Task<IActionResult> UpdateClientTrigger([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
        {
            var client = await JsonSerializer.DeserializeAsync<Client>(req.Body);

            try
            {
                if (client is Client)
                {
                    _logger.LogInformation("UpdateClientTrigger :: Updating client object from service layer.");
                    var accepted = await _clientService.UpdateClient(client);

                    if (accepted)
                    {
                        _logger.LogInformation($"UpdateClientTrigger :: Update accepted. Client id: { client.id }.");
                        return new AcceptedResult();
                    }

                    return new BadRequestResult();
                }

                _logger.LogWarning($"UpdateClientTrigger :: Unable to update client. Client id: { client.id }.");
                return new BadRequestResult();
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"UpdateClientTrigger :: Attempting to get data for client threw an exception. Client id: { client.id }");
                _logger.LogError($"UpdateClientTrigger Error Message :: { ex.Message }");

                return new InternalServerErrorResult();
            }
        }
    }
}