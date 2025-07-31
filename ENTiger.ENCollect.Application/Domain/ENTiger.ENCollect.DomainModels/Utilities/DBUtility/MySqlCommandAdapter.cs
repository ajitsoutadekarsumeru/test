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
    public class MySqlCommandAdapter : IMySqlCommandService
    {
        private readonly ITenantConnectionFactory _tenantConnectionFactory;

        public MySqlCommandAdapter(ITenantConnectionFactory tenantConnectionFactory)
        {
            _tenantConnectionFactory = tenantConnectionFactory;
        }

        public async Task ExecuteStoredProcedure(ExecuteSpRequestDto request)
        {
            using MySqlConnection connection = (MySqlConnection)_tenantConnectionFactory.CreateConnection(request.TenantId, DBTypeEnum.MySQL.Value);

            if (connection.State != System.Data.ConnectionState.Open)
                throw new InvalidOperationException("MySQL connection must be open before loading data.");
            using (MySqlCommand cmd = new MySqlCommand(request.SpName, connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 60;

                if (request.Parameters != null)
                {
                    foreach (var kvp in request.Parameters)
                    {
                        cmd.Parameters.AddWithValue("@" + kvp.Key, kvp.Value);
                    }
                }

                cmd.ExecuteNonQuery();
            }
        }

        public async Task<DataTable> ExecuteStoredProcedure(GetDataRequestDto request)
        {
            using MySqlConnection connection = (MySqlConnection)_tenantConnectionFactory.CreateConnection(request.TenantId, DBTypeEnum.MySQL.Value);
            DataTable dataTable = new DataTable();

            if (connection.State != System.Data.ConnectionState.Open)
                throw new InvalidOperationException("MySQL connection must be open before loading data.");
            using (MySqlCommand cmd = new MySqlCommand(request.SpName, connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 60;

                if (request.Parameters != null)
                {
                    foreach (var kvp in request.Parameters)
                    {
                        cmd.Parameters.AddWithValue("@" + kvp.Key, kvp.Value);
                    }
                }

                var dataAdapter = new MySqlDataAdapter(cmd);
                var dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                dataTable = dataSet.Tables[0];
            }

            return dataTable;
        }
    }
}
