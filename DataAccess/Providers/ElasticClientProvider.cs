using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Types.Interfaces.ProviderInterfaces;

namespace Integration.Elastic.Providers
{
    public class ElasticClientProvider
    {
        private readonly IDatabaseConfigurationProvider _dataBaseConfigurationProvider;

        public ElasticClientProvider(IDatabaseConfigurationProvider dataBaseConfigurationProvider)
        {
            _dataBaseConfigurationProvider = dataBaseConfigurationProvider;
        }
        public ElasticClient GetClient()
        {
            var str = _dataBaseConfigurationProvider.GetLogDatabaseConfiguration().ConnectionString;
            var connectionString = new ConnectionSettings(new Uri(str))
                .CertificateFingerprint("CD:40:0C:B3:43:D8:2D:FB:49:83:77:3A:DB:48:50:DE:98:5D:90:DB:E3:57:11:6B:4B:7A:F2:CD:D6:4F:6C:F0")
                .DisablePing()
                .SniffOnStartup(false)
                .EnableApiVersioningHeader()
                .SniffOnConnectionFault(false);
            if (!string.IsNullOrEmpty(_dataBaseConfigurationProvider.GetLogDatabaseConfiguration().UserName) && !string.IsNullOrEmpty(_dataBaseConfigurationProvider.GetLogDatabaseConfiguration().PassWord))
            {
                connectionString.BasicAuthentication(_dataBaseConfigurationProvider.GetLogDatabaseConfiguration().UserName, _dataBaseConfigurationProvider.GetLogDatabaseConfiguration().PassWord);
            }
            return new ElasticClient(connectionString);

        }


    }
}
