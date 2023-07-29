using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Types.Interfaces.DataAccessInterfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Types.Models;
using Newtonsoft.Json;
using Business.Producer;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IHostApplicationLifetime _appLifeTime;
        private readonly IProductDAL _productDal;

        private IMessageConsumer _messageConsumer;
        public Worker(ILogger<Worker> logger, IHostApplicationLifetime appLifeTime, IProductDAL productDAL, IMessageConsumer messageConsumer)
        {
            _logger = logger;
            _appLifeTime = appLifeTime;
            _productDal = productDAL;
            _messageConsumer = messageConsumer;
        }


        private void consumerReceivedQueue(EventingBasicConsumer consumer, List<string> listMessage)
        {
            consumer.Received += (sender, e) =>
            {
                string message = Encoding.UTF8.GetString(e.Body.Span);

                ProductModel product = JsonConvert.DeserializeObject<ProductModel>(message);
                _productDal.Insert(product);

            };
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
         
            //ConnectionFactory factory = new();
            //factory.Uri = new("amqps://cckdfwce:TvTDCIHx_wEpj06stFoP2c4UrT68coqN@moose.rmq.cloudamqp.com/cckdfwce");
            //IConnection connection = factory.CreateConnection();
            //IModel channel = connection.CreateModel();


            //string _queuqNmae = "product-exchange";
            //channel.QueueDeclare(queue: _queuqNmae, exclusive: false);
            //channel.QueueBind(queue: _queuqNmae, exchange: "product-exchange", routingKey: string.Empty);


            //EventingBasicConsumer consumer = new(channel);
            //channel.BasicConsume(queue: _queuqNmae, autoAck: true, consumer: consumer);
            //List<string> listMessage = new List<string>();


            while (!stoppingToken.IsCancellationRequested)
            {
                //_messageConsumer.ReceivedMessage();
                await Task.Delay(1, stoppingToken);
            }
        }
      
    }
}