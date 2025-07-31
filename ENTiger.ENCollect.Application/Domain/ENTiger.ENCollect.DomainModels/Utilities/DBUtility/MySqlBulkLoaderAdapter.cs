using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect
{
    public class MySqlBulkLoaderAdapter : IMySqlBulkLoaderService
    {
        private readonly ITenantConnectionFactory _tenantConnectionFactory;

        public MySqlBulkLoaderAdapter(ITenantConnectionFactory tenantConnectionFactory)
        {
            _tenantConnectionFactory = tenantConnectionFactory;
        }

        public async Task<int> LoadDataAsync(BulkInsertRequestDto request)
        {
            using MySqlConnection connection = (MySqlConnection) _tenantConnectionFactory.CreateConnection(request.TenantId,DBTypeEnum.MySQL.Value);

            if (connection.State != System.Data.ConnectionState.Open)
                throw new InvalidOperationException("MySQL connection must be open before loading data.");

            var bulkLoader = new MySqlBulkLoader(connection)
            {
                TableName = request.TableName,
                Timeout = request.Timeout,
                FieldTerminator = request.FieldTerminator,
                LineTerminator = request.LineTerminator,
                FileName = request.FileName,
                NumberOfLinesToSkip = request.NumberOfLinesToSkip
            };

            return await bulkLoader.LoadAsync(); // Executes bulk insert
        }

    }
}
