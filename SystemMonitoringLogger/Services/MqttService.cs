using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemMonitoringLogger.Data;
using SystemMonitoringLogger.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using SystemMonitoringLogger.DataAccess;

namespace SystemMonitoringLogger.Services
{
    public class MqttService
    {
        private readonly SystemMonitoringDataAccessLayer _accessLayerContext;

        public string Topic { get; }

        public MqttService(SystemMonitoringDataAccessLayer accessLayerContext, string topic)
        {
            _accessLayerContext = accessLayerContext;
            Topic = topic;
        }

        public void Listen()
        {
            var client = new MqttClient("185.239.238.179");
            client.Connect(Guid.NewGuid().ToString());
            client.MqttMsgPublishReceived += OnReceiveMessage;
            client.Subscribe(new []{Topic},new []{ MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
        } 
        private void OnReceiveMessage(object sender, MqttMsgPublishEventArgs e)
        {
            var message = Encoding.Default.GetString(e.Message);
            var sensorValue = JsonConvert.DeserializeObject<Measurement>(message);
            _accessLayerContext.AddMeasurement(sensorValue);
        }
    }
}
