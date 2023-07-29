using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Types.Interfaces.DataAccessInterfaces;
using Types.Models;

namespace Business.Producer
{
    public class RabbitMQConsumer : IMessageConsumer
    {
        private IConnection _connection;
        private IModel _channel;
        private EventingBasicConsumer _consumer;
        private readonly IProductDAL _productDal;
        public RabbitMQConsumer(IProductDAL productDAL)
        {
            _productDal = productDAL;
            ConnectionFactory factory = new();
            factory.Uri = new("amqps://cckdfwce:TvTDCIHx_wEpj06stFoP2c4UrT68coqN@moose.rmq.cloudamqp.com/cckdfwce");
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            string _queuqNmae = "product-exchange";
            _channel.QueueDeclare(queue: _queuqNmae, exclusive: false);
            _channel.QueueBind(queue: _queuqNmae, exchange: "product-exchange", routingKey: string.Empty);

            _consumer = new(_channel);
            _channel.BasicConsume(queue: _queuqNmae, autoAck: true, consumer: _consumer);
        }
        public void ReceivedMessage()
        {

            _consumer.Received += (sender, e) =>
            {
                string message = Encoding.UTF8.GetString(e.Body.Span);

                ProductModel product = JsonConvert.DeserializeObject<ProductModel>(message);
                _productDal.Insert(product);
            };
        }
    }
}
