using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Interfaces.BusinessInterfaces
{
    public interface IBaseBLL
    {

    }
    public interface IBaseBLL<TModel, TSearchModel> : IBaseBLL
      where TModel : BaseModel, new()
      where TSearchModel : BaseSearchModel, new()
    {
        Task Insert(TModel model);
        Task Update(TModel model);
        Task Delete(TModel model);
        Task DeleteId(string id);

        Task<PagingInformationModel<TModel>> Search(TSearchModel searchModel);
        Task<PagingInformationModel<TModel>> Select(TSearchModel searchModel, int pageNumber = 1, int pageSize = 20, bool readAll = false);
    }
}
