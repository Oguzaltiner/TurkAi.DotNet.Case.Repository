using Core.Utilities.Results;
using Entities.Interfaces.BusinessInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Types.Models;

namespace Types.Interfaces.BusinessInterfaces
{

    public interface IProductBLL : IBaseBLL<ProductModel, ProductSearchModel>
    {
        Task InsertProduct(ProductModel product);
        public Task PublishToMessageQueue(ProductModel product);
        public Task<IDataResult<List<ProductModel>>> GetAll();
        public Task<IDataResult<ProductModel>> GetProductDetailById(int id);
        public Task<IDataResult<long>> RemoveAll();
    }
}
