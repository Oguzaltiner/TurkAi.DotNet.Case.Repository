using Business.Producer;
using Core.Utilities.Results;
using Entities.Business;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Types.Interfaces.BusinessInterfaces;
using Types.Interfaces.DataAccessInterfaces;
using Types.Models;

namespace Business
{
    public class ProductBLL : BaseBLL<ProductModel, ProductSearchModel>, IProductBLL
    {
        private readonly IProductDAL _productDal;
        private readonly IMessageProducer _messageProvider;
        private readonly IDistributedCache _distributedCache;

        public ProductBLL(IProductDAL productDAL, IMessageProducer messageProducer, IDistributedCache distributedCache) : base(productDAL)
        {
            _productDal = productDAL;
            _messageProvider = messageProducer;
            _distributedCache = distributedCache;
        }

        public async Task<IDataResult<List<ProductModel>>> GetAll()
        {
            var result = await _productDal.GetAll();
            return new SuccessDataResult<List<ProductModel>>(result);
        }
        public async Task<IDataResult<long>> RemoveAll()
        {
            var result = await _productDal.RemoveAll();
            return new SuccessDataResult<long>(result);
        }
        public async Task<IDataResult<ProductModel>> GetProductDetailById(int id)
        {
            ProductModel resultModel = new ProductModel();
            string? serializedData = null;
            var dataAsByteArray = await _distributedCache.GetAsync(id.ToString());
            if ((dataAsByteArray?.Count() ?? 0) > 0)
            {
                var message = Encoding.UTF8.GetString(dataAsByteArray);
                resultModel = JsonConvert.DeserializeObject<ProductModel>(message);
            }
            else
            {
                resultModel = await _productDal.GetProductDetailById(id);
                serializedData = JsonConvert.SerializeObject(resultModel);
                dataAsByteArray = Encoding.UTF8.GetBytes(serializedData);
                await _distributedCache.SetAsync(id.ToString(), dataAsByteArray);

            }

            return new SuccessDataResult<ProductModel>(resultModel);
        }

        public Task InsertProduct(ProductModel product)
        {
            throw new NotImplementedException();
        }

        public async Task PublishToMessageQueue(ProductModel product)
        {
            //ConnectionFactory factory = new();
            //factory.Uri = new("amqps://cckdfwce:TvTDCIHx_wEpj06stFoP2c4UrT68coqN@moose.rmq.cloudamqp.com/cckdfwce");
            //IConnection connection = factory.CreateConnection();
            //IModel channel = connection.CreateModel();
            //channel.ExchangeDeclare(exchange: "product-exchange", type: ExchangeType.Fanout);
            //var message = JsonConvert.SerializeObject(product);
            //byte[] byteMessage = Encoding.UTF8.GetBytes(message);
            //channel.BasicPublish(exchange: "product-exchange", routingKey: String.Empty, body: byteMessage);
            await _messageProvider.SendMessage(product);
            //await _productDal.Insert(product);
        }
    }
}
