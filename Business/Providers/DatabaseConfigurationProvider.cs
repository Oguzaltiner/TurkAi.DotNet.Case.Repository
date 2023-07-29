using Entities.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Types.Interfaces.ProviderInterfaces;

namespace Business.Providers
{
    public class DatabaseConfigurationProvider : IDatabaseConfigurationProvider
    {
        public IConfiguration _configuration;

        public DatabaseConfigurationProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public LogDataBaseConfigurationModel GetLogDatabaseConfiguration()
        {
            LogDataBaseConfigurationModel logDataBaseConfigurationModel = new LogDataBaseConfigurationModel()
            {
                ConnectionString = _configuration.GetSection("ElasticSearch:ConnectionString:HostUrls").Value,
                UserName = _configuration.GetSection("ElasticSearch:ConnectionString:UserName").Value,
                PassWord = _configuration.GetSection("ElasticSearch:ConnectionString:Password").Value
            };
            return logDataBaseConfigurationModel;
        }
    }
}
