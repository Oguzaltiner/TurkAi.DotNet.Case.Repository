using Entities.Interfaces.BusinessInterfaces;
using Entities.Interfaces.DataAccessInterfaces;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Business
{
    public abstract class BaseBLL : IBaseBLL
    {
        public BaseBLL()
        {

        }

    }
    public abstract class BaseBLL<TModel, TSearchModel> : BaseBLL, IBaseBLL<TModel, TSearchModel>
      where TModel : BaseModel, new()
      where TSearchModel : BaseSearchModel, new()
    {
        protected readonly IBaseDAL<TModel, TSearchModel> DAL;
        public BaseBLL(IBaseDAL<TModel, TSearchModel> dal)
        {
            DAL = dal;
        }

        public async Task Insert(TModel model)
        {
            await DAL.Insert(model);

        }
        public async Task Delete(TModel model)
        {
            await DAL.Delete(model);

        }
        public async Task DeleteId(string id)
        {
            await DAL.DeleteId(id);

        }

        public async Task Update(TModel model)
        {
            await DAL.Update(model);
        }

        public async Task<PagingInformationModel<TModel>> Search(TSearchModel searchModel)
        {
            var searchResponse = await DAL.Search(searchModel);
            return searchResponse;
        }

        public async Task<PagingInformationModel<TModel>> Select(TSearchModel searchModel, int pageNumber = 1, int pageSize = 20, bool readAll = false)
        {
            var response = await DAL.Search(searchModel);

            return response;
        }



    }
}
