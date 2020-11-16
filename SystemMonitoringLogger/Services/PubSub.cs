using Google.Cloud.PubSub.V1;
using Grpc.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SystemMonitoringLogger.DataAccess;
using SystemMonitoringLogger.Entities;

namespace SystemMonitoringLogger.Services
{
    public class PubSub
    {
        public static string projectId = "systemmonitoring-294918";
        private readonly SystemMonitoringDataAccessLayer _accessLayerContext;
        public string Topic { get { return "systeminfo"; } }
        public SubscriberClient subscriber;
        string filepath = "D:/HTL/4AHIF/NVS-Lack/SystemMonitoringLogger/systemmonitoring-294918-b4f8146cefa9.json";

        public PubSub(SystemMonitoringDataAccessLayer accessLayerContext)
        {
            _accessLayerContext = accessLayerContext;
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filepath);
        }

        public async Task<int> PullMessagesAsync(string subscriptionId, bool acknowledge)
        {
            SubscriptionName subscriptionName = SubscriptionName.FromProjectSubscription(projectId, subscriptionId);
            subscriber = await SubscriberClient.CreateAsync(subscriptionName);
            int messageCount = 0;
            Task t1 = subscriber.StartAsync((PubsubMessage message, CancellationToken cancel) =>
            {
                string text = Encoding.UTF8.GetString(message.Data.ToArray());
                var sensorValue = JsonConvert.DeserializeObject<Measurement>(text);
                sensorValue.Timestamp = sensorValue.Timestamp.ToUniversalTime();
                _accessLayerContext.AddMeasurement(sensorValue);
                Interlocked.Increment(ref messageCount);
                return Task.FromResult(acknowledge ? SubscriberClient.Reply.Ack : SubscriberClient.Reply.Nack);
            });
            //await Task.Delay(10000);
            //await subscriber.StopAsync(CancellationToken.None);
            //await t1;
            return messageCount;
        }
    }
}
