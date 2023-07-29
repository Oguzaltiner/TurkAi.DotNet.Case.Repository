using Entities.Interfaces.DataAccessInterfaces;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataAccess
{
    public abstract class BaseDAL : IBaseDAL
    {
        public BaseDAL()
        {

        }
    }
    public abstract class BaseDAL<TModel, TSearchModel> : BaseDAL, IBaseDAL<TModel, TSearchModel>
        where TModel : BaseModel, new()
        where TSearchModel : BaseSearchModel, new()
    {
        public BaseDAL()
        {
        }

        protected abstract string TableName { get; }
        public abstract Task Insert(TModel model);
        public abstract Task Update(TModel model);
        public abstract Task Delete(TModel model);
        public abstract Task DeleteId(string id);
        public abstract Task<List<TModel>> GetAll();
        public abstract Task<TModel> GetProductDetailById(int id);
        public abstract Task<PagingInformationModel<TModel>> Search(TSearchModel model);
        public abstract Task<long> RemoveAll();

    }
}
