using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Interfaces.DataAccessInterfaces
{
    public interface IBaseDAL
    {

    }
    public interface IBaseDAL<TModel, TSearchModel>
        where TModel : BaseModel, new()
        where TSearchModel : BaseSearchModel, new()
    {
        Task Insert(TModel model);
        Task Update(TModel model);
        Task Delete(TModel model);
        Task DeleteId(string id);
        Task<List<TModel>> GetAll();
        Task<TModel> GetProductDetailById(int id);
        Task<PagingInformationModel<TModel>> Search(TSearchModel Searchmodel);
        Task<long> RemoveAll();

    }

}
