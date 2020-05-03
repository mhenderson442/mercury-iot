using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Mercuryiot.Functions
{
    public static class AlarmConditionDequeue
    {

        [FunctionName("AlarmConditionDequeue")]
        public static void Run([ServiceBusTrigger("myqueue", Connection = "SensorMonitoringConnection")] string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
