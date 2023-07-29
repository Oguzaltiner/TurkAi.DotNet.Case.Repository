using Entities.Interfaces.BusinessInterfaces;
using Entities.Interfaces.DataAccessInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Types.Models;

namespace Types.Interfaces.DataAccessInterfaces
{

    public interface IProductDAL : IBaseDAL<ProductModel, ProductSearchModel>

    {

    }
}
