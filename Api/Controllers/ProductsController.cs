using Core.Utilities.Results;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Nest;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RestSharp;
using System.Diagnostics.Metrics;
using System.Text;
using System.Text.Json.Serialization;
using Types.Interfaces.BusinessInterfaces;
using Types.Models;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductBLL _productBLL;

        public ProductsController(IProductBLL productBLL)
        {
            _productBLL = productBLL;
        }

        [HttpPost]
        public async Task<IActionResult> AddProducts(ProductModel productModel)
        {
            await _productBLL.InsertProduct(productModel);
            return Ok(productModel);
        }

        [HttpGet("GetProductDetailById")]
        public async Task<IActionResult> GetProductDetailById(int id)
        {
            var result = await _productBLL.GetProductDetailById(id);
            return Ok(result);
        }
        [HttpGet("RemoveAll")]

        public async Task<IActionResult> RemoveAll()
        {
            var result = await _productBLL.RemoveAll();
            return Ok(result);
        }
        [HttpGet("GetAllProducts")]

        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _productBLL.GetAll();
            return Ok(result);
        }

        [HttpPost("AddProductsThanApi")]
        public async Task<IActionResult> AddProductsThanApi()
        {
            var dummyApi = ("https://dummyjson.com/products");

            int count = 0;
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(dummyApi))
                {
                    using (HttpContent content = res.Content)
                    {
                        var data = await content.ReadAsStringAsync();
                        ProductResponse response = JsonConvert.DeserializeObject<ProductResponse>(data);
                        count = response.products.Count;
                        var tasks = new List<Task>();
                        foreach (var product in response.products)
                        {
                            tasks.Add(Task.Run(() => _productBLL.PublishToMessageQueue(product)));
                        }
                        await Task.WhenAll(tasks);
                    }
                }
            }
            return Ok();
        }

    }
}
