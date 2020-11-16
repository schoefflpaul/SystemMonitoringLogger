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



        static async Task DoIt()
        {
            var subscriptionName = new SubscriptionName(projectId, "adminpull");
            while (true)
            {
                var subscriber = await SubscriberClient.CreateAsync(subscriptionName);
                try
                {
                    await subscriber.StartAsync((msg, ct) =>
                    {
                        Console.WriteLine(msg.Data.ToStringUtf8());
                        return Task.FromResult(SubscriberClient.Reply.Ack);
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception: {e.Message} Inner Exception: {e.InnerException}");
                }
            }
        }

        public PubSub(SystemMonitoringDataAccessLayer accessLayerContext)
        {
            _accessLayerContext = accessLayerContext;
            DoIt().Wait();
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
            await Task.Delay(10000);
            await subscriber.StopAsync(CancellationToken.None);
            await t1;
            return messageCount;
        }
        
        /*

        public int PullMessagesSync(string subscriptionId, bool acknowledge)
        {
            SubscriptionName subscriptionName = SubscriptionName.FromProjectSubscription(projectId, subscriptionId);
            SubscriberServiceApiClient subscriberClient = SubscriberServiceApiClient.Create();
            int messageCount = 0;
            try
            {
                // Pull messages from server,
                // allowing an immediate response if there are no messages.
                PullResponse response = subscriberClient.Pull(subscriptionName, returnImmediately: false, maxMessages: 20);
                // Print out each received message.
                foreach (ReceivedMessage msg in response.ReceivedMessages)
                {
                    string text = Encoding.UTF8.GetString(msg.Message.Data.ToArray());
                    Console.WriteLine($"Message {msg.Message.MessageId}: {text}");
                    Interlocked.Increment(ref messageCount);
                }
                // If acknowledgement required, send to server.
                if (acknowledge && messageCount > 0)
                {
                    subscriberClient.Acknowledge(subscriptionName, response.ReceivedMessages.Select(msg => msg.AckId));
                }
            }
            catch (RpcException ex) when (ex.Status.StatusCode == StatusCode.Unavailable)
            {
                // UNAVAILABLE due to too many concurrent pull requests pending for the given subscription.
            }
            return messageCount;
        }
        */
    }
}
