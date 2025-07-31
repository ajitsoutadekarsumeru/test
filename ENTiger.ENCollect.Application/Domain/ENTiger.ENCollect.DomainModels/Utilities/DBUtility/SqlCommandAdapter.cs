using Microsoft.Data.SqlClient;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OfficeOpenXml.ExcelErrorValue;

namespace ENTiger.ENCollect
{
    public class SqlCommandAdapter : ISqlCommandService
    {
        private readonly ITenantConnectionFactory _tenantConnectionFactory;

        public SqlCommandAdapter(ITenantConnectionFactory tenantConnectionFactory)
        {
            _tenantConnectionFactory = tenantConnectionFactory;
        }

        public async Task ExecuteStoredProcedure(ExecuteSpRequestDto request)
        {
            // Get the original connection and sanitize the connection string
            var originalConnection = _tenantConnectionFactory.CreateConnection(request.TenantId, DBTypeEnum.MsSQL.Value);
            var builder = new SqlConnectionStringBuilder(originalConnection.ConnectionString);

            // Remove unnecessary parameters
            builder.Remove("MultipleActiveResultSets");
            builder.Remove("TrustServerCertificate");

            await using var connection = new SqlConnection(builder.ToString());
            await connection.OpenAsync();

            if (connection.State != ConnectionState.Open)
                throw new InvalidOperationException("SQL connection must be open before loading data.");

            await using var cmd = new SqlCommand(request.SpName, connection)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 60
            };

            if (request.Parameters?.Count > 0)
            {
                foreach (var (key, value) in request.Parameters)
                {
                    cmd.Parameters.AddWithValue("@" + key, value ?? (object)DBNull.Value);
                }
            }

            await cmd.ExecuteNonQueryAsync();
        }


        public async Task<DataTable> ExecuteStoredProcedure(GetDataRequestDto request)
        {
            using SqlConnection connection = (SqlConnection)_tenantConnectionFactory.CreateConnection(request.TenantId, DBTypeEnum.MySQL.Value);
            DataTable dataTable = new DataTable();

            if (connection.State != System.Data.ConnectionState.Open)
                throw new InvalidOperationException("SQL connection must be open before loading data.");

            using (var sqlAdapter = new SqlDataAdapter(request.SpName, connection))
            {
                sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (request.Parameters != null)
                {
                    foreach (var kvp in request.Parameters)
                    {
                        sqlAdapter.SelectCommand.Parameters.AddWithValue("@" + kvp.Key, kvp.Value);
                    }
                }
                sqlAdapter.Fill(dataTable);
            }
            return dataTable;
        }
    }
}
