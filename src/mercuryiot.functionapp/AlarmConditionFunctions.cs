using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Mercuryiot.Functions
{
    public static class AlarmConditionFunctions
    {

        [FunctionName("AlarmConditionDequeue")]
        public static void Run([ServiceBusTrigger("alarm-condition-queue", Connection = "SensorMonitoringConnection")] string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
