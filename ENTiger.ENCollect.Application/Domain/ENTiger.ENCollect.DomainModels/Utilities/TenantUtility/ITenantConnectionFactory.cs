using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect
{
    public interface ITenantConnectionFactory
    {
        IDbConnection CreateConnection(string tenantId, string dbType);
    }
}
