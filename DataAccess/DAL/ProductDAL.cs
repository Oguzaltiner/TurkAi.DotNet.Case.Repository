using Integration.Elastic.Constants;
using Integration.Elastic.Providers;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Types.Interfaces.DataAccessInterfaces;
using Types.Interfaces.ProviderInterfaces;
using Types.Models;

namespace Integration.Elastic.DAL
{
    public class ProductDAL : BaseElasticDAL<ProductModel, ProductSearchModel>, IProductDAL
    {
        protected override string TableName => ElasticTypeNameConstants.Product;
        public ProductDAL(IDatabaseConfigurationProvider databaseConfigurationProvider) : base(databaseConfigurationProvider)
        {
            ElasticClientProvider elasticClientProvider = new ElasticClientProvider(databaseConfigurationProvider);
            elasticClientProvider.GetClient();
        }
        protected override QueryContainer GetSearchResponse(ProductSearchModel searchModel, QueryContainerDescriptor<ProductModel> q)
        {
            QueryContainer queryContainer = new QueryContainer();
            if (string.IsNullOrWhiteSpace(searchModel.category) == false)
            {
                queryContainer = queryContainer && q.Term(k => k.category, searchModel.category);
            }
            return queryContainer;
        }
    }
}
