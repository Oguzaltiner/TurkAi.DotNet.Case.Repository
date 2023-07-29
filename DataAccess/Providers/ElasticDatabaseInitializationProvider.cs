using Integration.Elastic.Constants;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Types.Interfaces.ProviderInterfaces;
using Types.Models;

namespace Integration.Elastic.Providers
{
    public class ElasticDatabaseInitializationProvider : IDatabaseInitializationProvider
    {
        private const string DatabaseName = "TurkAi";
        protected readonly ElasticClient ElasticClient;
        public ElasticDatabaseInitializationProvider(IDatabaseConfigurationProvider databaseConfigurationProvider)
        {
            ElasticClientProvider elasticClientProvider = new ElasticClientProvider(databaseConfigurationProvider);
            ElasticClient = elasticClientProvider.GetClient();
        }

        public async Task InitializeDatabase()
        {
            CreateIndexResponse response1 = await ElasticClient.Indices.CreateAsync(ElasticTypeNameConstants.Product, c => c.Map<ProductModel>(m => m.AutoMap<ProductModel>()));
        }
    }
}
