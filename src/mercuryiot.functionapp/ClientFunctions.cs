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

        [FunctionName("GetClientTrigger")]
        public async Task<IActionResult> GetClientTrigger([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            string customerKey = req.Query["customerKey"];

            if (string.IsNullOrEmpty(customerKey))
            {
                _logger.LogWarning($"GetClientTrigger :: Customer key was null or empty");
                return new BadRequestResult();
            }

            try
            {
                _logger.LogInformation("GetClientTrigger :: Getting client object from service layer.");
                var client = await _clientService.GetClient(customerKey);

                if (client is Client)
                {
                    _logger.LogInformation($"GetClientTrigger :: Client object found. Customer Key: { customerKey }.");
                    return new OkObjectResult(client);
                }

                _logger.LogWarning($"GetClientTrigger :: Query did not return an object. Customer Key: { customerKey }.");
                return new BadRequestResult();
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"GetClientTrigger :: Attempting to get data for client threw an exception. Customer Key: { customerKey }");
                _logger.LogError($"GetClientTrigger Error Message :: { ex.Message }");

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
                        _logger.LogInformation($"InsertClientTrigger :: Insert accepted. Customer Key: { client.Key }.");
                        return new AcceptedResult();
                    }

                    return new BadRequestResult();
                }

                _logger.LogWarning($"InsertClientTrigger :: Unable to insert client. Customer Key: { client.Key }.");
                return new BadRequestResult();
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"InsertClientTrigger :: Attempting to get data for client threw an exception. Customer Key: { client.Key }");
                _logger.LogError($"InsertClientTrigger Error Message :: { ex.Message }");

                return new InternalServerErrorResult();
            }
        }
    }
}