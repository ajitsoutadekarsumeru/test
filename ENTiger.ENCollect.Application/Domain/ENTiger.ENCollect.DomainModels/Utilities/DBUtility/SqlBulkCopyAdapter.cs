using Microsoft.Data.SqlClient;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect
{
    public class SqlBulkCopyAdapter : ISqlBulkCopyService
    {
        private readonly ITenantConnectionFactory _tenantConnectionFactory;

        public SqlBulkCopyAdapter(ITenantConnectionFactory tenantConnectionFactory)
        {
            _tenantConnectionFactory = tenantConnectionFactory;
        }

        public async Task<int> LoadDataAsync(BulkInsertRequestDto request)
        {
            using SqlConnection connection = (SqlConnection)_tenantConnectionFactory.CreateConnection(request.TenantId, DBTypeEnum.MsSQL.Value);

            if (connection.State != System.Data.ConnectionState.Open)
                throw new InvalidOperationException("SQL connection must be open before loading data.");

            int insertedRows = request.Table.Rows.Count; // Count rows before inserting

            using (var sqlBulkCopy = new SqlBulkCopy(connection))
            {
                sqlBulkCopy.DestinationTableName = request.TableName;

                sqlBulkCopy.ColumnMappings.Add("InsertFlag", "InsertFlag");
                sqlBulkCopy.ColumnMappings.Add("IsError", "IsError");
                sqlBulkCopy.ColumnMappings.Add("Reason", "Reason");

                sqlBulkCopy.BulkCopyTimeout = 600;
                await sqlBulkCopy.WriteToServerAsync(request.Table);
            }

            return insertedRows; // Returning inserted row count
        }


    }
}
