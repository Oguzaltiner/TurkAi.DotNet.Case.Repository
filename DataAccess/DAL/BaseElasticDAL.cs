using Entities.DataAccess;
using Entities.Models;
using Integration.Elastic.Providers;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Types.Interfaces.ProviderInterfaces;

namespace Integration.Elastic.DAL
{
    public abstract class BaseElasticDAL<TModel, TSearchModel> : BaseDAL<TModel, TSearchModel>
       where TModel : BaseModel, new()
       where TSearchModel : BaseSearchModel, new()
    {
        protected readonly ElasticClient ElasticClient;
        public BaseElasticDAL(IDatabaseConfigurationProvider databaseConfigurationProvider)
        {
            ElasticClientProvider elasticClientProvider = new ElasticClientProvider(databaseConfigurationProvider);
            ElasticDatabaseInitializationProvider elasticDatabaseInitializationProvider = new ElasticDatabaseInitializationProvider(databaseConfigurationProvider);
            ElasticClient = elasticClientProvider.GetClient();
            var r = elasticDatabaseInitializationProvider.InitializeDatabase();

        }
        public override async Task Insert(TModel model)
        {
            try
            {
                var result = await ElasticClient.IndexAsync(model, s => s.Index(TableName).Id(model.Id ?? Guid.NewGuid().ToString()));
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override async Task Update(TModel model)
        {
            await ElasticClient.UpdateAsync(DocumentPath<TModel>.Id(model.Id), i => i.Index(TableName).Doc(model));
        }
        public override async Task Delete(TModel model)
        {

            await ElasticClient.DeleteAsync<TModel>(model, i => i.Index(TableName));
            // await ElasticClient.DeleteAsync(new DeleteRequest(TableName, new Id(model)));

        }

        public override async Task DeleteId(string id)
        {
            await ElasticClient.DeleteAsync<TModel>(id, idx => idx.Index(TableName));

        }
        public override async Task<List<TModel>> GetAll()
        {
            var searchResponse = await ElasticClient.SearchAsync<TModel>(s => s.Index(TableName).Query(q => q.MatchAll()));
            return searchResponse.Documents.ToList();
        }
        public override async Task<long> RemoveAll()
        {
            var response = await ElasticClient.DeleteByQueryAsync<TModel>(s => s.Index(TableName).Query(q => q.MatchAll()));
            return response.Deleted;
        }
        public override async Task<TModel> GetProductDetailById(int id)
        {

            var response = await ElasticClient.GetAsync<TModel>(id, g => g.Index(TableName));
            return response.Source;
        }

        public override async Task<PagingInformationModel<TModel>> Search(TSearchModel searchModel)
        {
            ISearchResponse<TModel> searchResponse;
            if (searchModel.ReadAll)
            {
                ISearchResponse<TModel> searchResponses = await ElasticClient.SearchAsync<TModel>(s => s.Index(TableName).Query(q => GetSearchResponse(searchModel, q)).From(0).Size(10000).Scroll("1s"));
                searchResponse = searchResponses;
            }
            else
            {
                ISearchResponse<TModel> searchResponses = await ElasticClient.SearchAsync<TModel>(s => s.Index(TableName).Query(q => GetSearchResponse(searchModel, q)).From(searchModel.PageSize * (searchModel.PageNumber - 1)).Size(searchModel.PageSize));
                searchResponse = searchResponses;

            }

            ///searchsize default 10 liste oluşturarak tüm veriler eklendi.
            var fromElement = 0;
            var looping = true;
            var scrollid = searchResponse.ScrollId;
            var Results = new List<TModel>();
            if (searchResponse.Documents.Any())
            {
                Results.AddRange(searchResponse.Documents);
            }
            while (looping)
            {
                var results3 = ElasticClient.Scroll<TModel>("1s", scrollid);
                if (results3.IsValid)
                {
                    Results.AddRange(results3.Documents);
                    scrollid = results3.ScrollId;
                }
                if (results3.Documents.Count() < 10000)
                    looping = false;
                fromElement += 10000;
                var a = results3;
            }


            // List<TModel> models = searchResponse.Documents.ToList();
            List<TModel> models = Results.ToList();
            PagingInformationModel<TModel> result = new PagingInformationModel<TModel>(models, searchResponse.Total, searchModel.PageSize, searchModel.PageNumber);
            return result;
        }


        protected abstract QueryContainer GetSearchResponse(TSearchModel searchModel, QueryContainerDescriptor<TModel> q);


    }
}
