using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MySqlConnector;
using Sumeru.Flex;
using System.Data;
using System.IO.Abstractions;

namespace ENTiger.ENCollect
{
    public class MsSqlUtility : IDbUtility
    {
        private readonly ILogger<MsSqlUtility> _logger;
        private readonly int _commandTimeout;
        private readonly ITenantConnectionFactory _tenantConnectionFactory;
        private readonly ISqlBulkCopyService _sqlBulkCopyService;
        private readonly ISqlCommandService _sqlCommandService;

        private string dbType = DBTypeEnum.MsSQL.Value;
        public MsSqlUtility(ILogger<MsSqlUtility> logger, 
            int commandTimeout = 60,
            ITenantConnectionFactory tenantConnectionFactory = null,
            ISqlBulkCopyService sqlBulkCopyService = null,
            ISqlCommandService sqlCommandService = null)
        {
            _logger = logger;
            _commandTimeout = commandTimeout;
            _tenantConnectionFactory = tenantConnectionFactory;
            this._sqlBulkCopyService = sqlBulkCopyService;
            _sqlCommandService = sqlCommandService;
        }

        public async Task<bool> ExecuteSP(ExecuteSpRequestDto request)
        {
            string? spName = request.SpName;
            string? tenantId = request.TenantId;
            string? WorkRequestId = request.WorkRequestId;
            string? uploadType = request.UploadType;
            string? userId = request.UserId;
            string? ActionType = request.ActionType;

            Dictionary<string, string> values = request.Parameters;
            try
            {
                _logger.LogInformation("MSSQL: Executing SP {spName}", spName);

                if (string.IsNullOrWhiteSpace(spName) ||
                   spName.IndexOfAny(new char[] { ';', '\'', '-', '@', ' ' }) != -1 ||
                   spName.Contains("/*") || spName.Contains("*/"))
                {
                    _logger.LogError("Invalid stored procedure name.");
                    throw new ArgumentException("Invalid stored procedure name", nameof(spName));
                }

                await _sqlCommandService.ExecuteStoredProcedure(request);

                _logger.LogInformation("MSSQL: SP {spName} executed successfully", spName);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "MSSQL: Error executing SP {spName}", spName);
                throw;
            }
        }

        public async Task<int> InsertRecordsIntoIntermediateTable(InsertIntermediateTableRequestDto request)
        {
            string? tableName = request.TableName;
            DataTable table = request.Table;
            
            _logger.LogInformation("MSSQL: Bulk inserting into {tableName}", tableName);
            try
            {
                BulkInsertRequestDto dto = new BulkInsertRequestDto
                {
                    TableName = tableName,
                    Table = table,
                    Timeout = 600,
                    TenantId = request.TenantId
                };

                int recordCount = await _sqlBulkCopyService.LoadDataAsync(dto);
                _logger.LogInformation("MSSQL: Bulk insert successful");
                return recordCount;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "MSSQL: Error during bulk insert");
                throw;
            }
        }

        public async Task<DataTable> GetData(GetDataRequestDto request)
        {
            string? spName = request.SpName;            
            var dataTable = new DataTable();
            _logger.LogInformation("SQL: GetData for SP {spName}", spName);

            try
            {
                if (string.IsNullOrWhiteSpace(spName) ||
                   spName.IndexOfAny(new char[] { ';', '\'', '-', '@', ' ' }) != -1 ||
                   spName.Contains("/*") || spName.Contains("*/"))
                {
                    _logger.LogError("Invalid stored procedure name.");
                    throw new ArgumentException("Invalid stored procedure name", nameof(spName));
                }

                dataTable = await _sqlCommandService.ExecuteStoredProcedure(request);
                _logger.LogInformation("MSSQL: Data retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "MSSQL: Error in GetData for SP {spName}", spName);
                throw;
            }

            return dataTable;
        }

        public async Task UpdateTreatmentLoanAccounts(UpdateTreatmentRequestDto request)
        {
           
            DataTable dt = request.Data;
            string workRequestId = request.WorkRequestId;
            string storedProcedure = request.StoredProcedure;

            _logger.LogInformation("MSSQL: Updating treatment loan accounts for WorkRequest {workRequestId}", workRequestId);
            try
            {
                using (var connection = (SqlConnection)_tenantConnectionFactory.CreateConnection(request.TenantId, dbType))
                {
                    // Example logic:
                    using (SqlCommand truncateCmd = new SqlCommand("TRUNCATE TABLE treatmentupdateintermediate", connection))
                    {
                        truncateCmd.ExecuteNonQuery();
                    }

                    // Insert DataTable records
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TreatmentUpdateIntermediate", connection);
                    SqlCommandBuilder cb = new SqlCommandBuilder(da);
                    da.Fill(dt);
                    da.Update(dt);

                    // Call stored procedure
                    using (SqlCommand cmd = new SqlCommand(storedProcedure, connection))
                    {
                        cmd.CommandTimeout = 180;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@WorkRequestId", workRequestId);
                        cmd.ExecuteNonQuery();
                    }
                }

                _logger.LogInformation("MSSQL: Treatment loan accounts updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "MSSQL: Error updating treatment loan accounts");
                throw;
            }
        }

        public async Task InsertIntoUnAllocationIntermediateTable(InsertIntoUnAllocationIntermediateTableRequestDto request)
        {
            _logger.LogInformation("DBHelperUtility : InsertIntoIntermediate - Start : DBType - MsSql : WorkrequestId = " + request.WorkRequestId + " :: table =" + request.TableName);
            IFlexTenantRepository<FlexTenantBridge> _repoTenantFactory = FlexContainer.ServiceProvider.GetRequiredService<IFlexTenantRepository<FlexTenantBridge>>();
            string sqlConnectionString = _repoTenantFactory.FindAll<FlexTenantBridge>()
                .Where(x => x.Id == request.TenantId)
                .Select(x => x.DefaultWriteDbConnectionString).FirstOrDefault();

            try
            {
                var data = request.Table;
                var userId = request.UserId;
                var table = request.TableName;
                var dt = new DataTable();

                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(sqlConnectionString);

                sqlConnectionString = builder.ConnectionString;
                _logger.LogInformation("DBHelperUtility : ConnectionString - " + sqlConnectionString);

                dt.Columns.Add("AccountNo", typeof(string));
                dt.Columns.Add("CreatedBy", typeof(string));
                dt.Columns.Add("CreatedDate", typeof(DateTime));
                dt.Columns.Add("IsError", typeof(bool));
                dt.Columns.Add("Reason", typeof(string));
                dt.Columns.Add("WorkRequestId", typeof(string));

                data.AsEnumerable()
                    //.Skip(1) // skip headers
                    .Where(dr => Convert.ToString(dr[0]).Length > 0)
                    .ToList()
                    .ForEach(dr => dt.Rows.Add(dr[0], userId, DateTime.Now, false, string.Empty, request.WorkRequestId));

                using (var sqlCon = new SqlConnection(sqlConnectionString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlCon))
                    {
                        sqlBulkCopy.DestinationTableName = table;
                        sqlBulkCopy.ColumnMappings.Add("AccountNo", "AccountNo");
                        sqlBulkCopy.ColumnMappings.Add("CreatedBy", "CreatedBy");
                        sqlBulkCopy.ColumnMappings.Add("CreatedDate", "CreatedDate");
                        sqlBulkCopy.ColumnMappings.Add("IsError", "IsError");
                        sqlBulkCopy.ColumnMappings.Add("Reason", "Reason");
                        sqlBulkCopy.ColumnMappings.Add("WorkRequestId", "WorkRequestId");

                        sqlCon.Open();
                        sqlBulkCopy.BulkCopyTimeout = 0;
                        sqlBulkCopy.WriteToServer(dt);
                        sqlCon.Close();
                    }
                }


            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in DBHelperUtility : InsertIntoIntermediate - " + ex);
                throw;
            }
            _logger.LogInformation("DBHelperUtility : InsertIntoIntermediate - End");
        }
        private void CreateCSVfile(DataTable dtable, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
            int icolcount = dtable.Columns.Count;
            foreach (DataRow drow in dtable.Rows)
            {
                for (int i = 0; i < icolcount; i++)
                {
                    if (!Convert.IsDBNull(drow[i]))
                    {
                        sw.Write(drow[i].ToString());
                    }
                    if (i < icolcount - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
            sw.Dispose();
        }
    }
}