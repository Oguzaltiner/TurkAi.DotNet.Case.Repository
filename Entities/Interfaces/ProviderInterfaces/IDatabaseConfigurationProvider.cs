using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Types.Interfaces.ProviderInterfaces
{
    public interface IDatabaseConfigurationProvider
    {
        LogDataBaseConfigurationModel GetLogDatabaseConfiguration();
    }
}