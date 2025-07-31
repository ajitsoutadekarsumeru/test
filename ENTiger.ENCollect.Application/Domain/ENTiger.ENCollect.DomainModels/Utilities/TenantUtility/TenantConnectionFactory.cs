using Microsoft.Data.SqlClient;
using MySqlConnector;
using Sumeru.Flex;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect
{
    public class TenantConnectionFactory : ITenantConnectionFactory
    {
        private readonly IFlexTenantRepository<FlexTenantBridge> _repoTenantFactory;
        

        public TenantConnectionFactory(
            IFlexTenantRepository<FlexTenantBridge> repoTenantFactory)
                                        
        {
            _repoTenantFactory = repoTenantFactory;
          
        }

        private string GetConnectionString(string tenantId)
        {

            string? connectionString = _repoTenantFactory.FindAll<FlexTenantBridge>()
                .Where(x => x.Id == tenantId)
                .Select(x => x.DefaultWriteDbConnectionString)
                .FirstOrDefault();

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException($"No connection string found for TenantId: {tenantId}");
            }

            return connectionString;
        }

        public IDbConnection CreateConnection(string tenantId, string dbType)
        {
            string tenantConnectionString = GetConnectionString(tenantId);

            if (string.IsNullOrEmpty(tenantConnectionString))
            {
                throw new Exception($"No connection string found for Tenant ID: {tenantId}");
            }

            // Return an open connection for the tenant
            IDbConnection connection = dbType.ToLower() switch
            {
                "mysql" => new MySqlConnection(tenantConnectionString),
                "mssql" => new SqlConnection(tenantConnectionString),
                _ => throw new NotSupportedException($"Unsupported database provider: {dbType}")
            };

            connection.Open();
            return connection;
        }
    }
}
