using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Business.Producer
{
    public class RabbitMQProducer : IMessageProducer
    {
        private IConnection _connection;
        private IModel _channel;
        public RabbitMQProducer()
        {
            ConnectionFactory factory = new();
            factory.Uri = new("amqps://cckdfwce:TvTDCIHx_wEpj06stFoP2c4UrT68coqN@moose.rmq.cloudamqp.com/cckdfwce");
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "product-exchange", type: ExchangeType.Fanout);
        }
        public async Task SendMessage<T>(T message)
        {
            var newMessage = JsonConvert.SerializeObject(message);
            byte[] byteMessage = Encoding.UTF8.GetBytes(newMessage);
            _channel.BasicPublish(exchange: "product-exchange", routingKey: String.Empty, body: byteMessage);
        }
    }
}
