using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySqlConnector;
using Sumeru.Flex;
using System.Data;
using System.IO.Abstractions;

namespace ENTiger.ENCollect
{
    public class MySqlUtility : IDbUtility
    {
        private readonly ILogger<MySqlUtility> _logger;
        private readonly FilePathSettings _fileSettings;
        private readonly IFileSystem _fileSystem;
        private readonly int _commandTimeout;
        private readonly IMySqlBulkLoaderService _mySqlBulkLoaderService;
        private readonly IMySqlCommandService _mySqlCommandService;
        private readonly ITenantConnectionFactory _tenantConnectionFactory;
        private string dbType = DBTypeEnum.MySQL.Value;

        public MySqlUtility(ILogger<MySqlUtility> logger,
            IOptions<FilePathSettings> fileSettings,
            IFileSystem fileSystem,
            IMySqlBulkLoaderService mySqlBulkLoaderService,
            ITenantConnectionFactory tenantConnectionFactory,
            IMySqlCommandService mySqlCommandService,
            int commandTimeout = 60)
        {
            _logger = logger;
            _fileSettings = fileSettings.Value;
            _fileSystem = fileSystem;
            _commandTimeout = commandTimeout;
            _mySqlBulkLoaderService = mySqlBulkLoaderService;
            _mySqlCommandService = mySqlCommandService;
            _tenantConnectionFactory = tenantConnectionFactory;
        }

        public async Task<bool> ExecuteSP(ExecuteSpRequestDto request)
        {
            string? spName = request.SpName;

            try
            {
                _logger.LogInformation("MySQL: Executing SP {spName}", spName);

                if (string.IsNullOrWhiteSpace(spName) ||
                   spName.IndexOfAny(new char[] { ';', '\'', '-', '@', ' ' }) != -1 ||
                   spName.Contains("/*") || spName.Contains("*/"))
                {
                    _logger.LogError("Invalid stored procedure name.");
                    throw new ArgumentException("Invalid stored procedure name", nameof(spName));
                }

                await _mySqlCommandService.ExecuteStoredProcedure(request);

                _logger.LogInformation("MySQL: SP {spName} executed successfully", spName);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "MySQL: Error executing SP {spName}", spName);
                throw;
            }
        }

        public async Task<int> InsertRecordsIntoIntermediateTable(InsertIntermediateTableRequestDto request)
        {
            string? tableName = request.TableName;
            string? fileName = request.FileName;
            string? delimiter = request.Delimiter;

            _logger.LogInformation("MySQL: Bulk loading file {fileName} into {tableName}", fileName, tableName);
            try
            {
                BulkInsertRequestDto dto = new BulkInsertRequestDto
                {
                    TableName = tableName,
                    Timeout = 600,
                    FieldTerminator = delimiter,
                    LineTerminator = "\n",
                    FileName = fileName,
                    NumberOfLinesToSkip = 1,
                    TenantId = request.TenantId
                };

                int recordCount = await _mySqlBulkLoaderService.LoadDataAsync(dto);

                _logger.LogInformation("MySQL: Inserted {recordCount} records into {tableName}", recordCount, tableName);

                return recordCount;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "MySQL: Error bulk loading data");
                throw;
            }
        }
        public async Task<DataTable> GetData(GetDataRequestDto request)
        {
            string? spName = request.SpName;

            string? WorkRequestId = request.WorkRequestId;
            Dictionary<string, string> values = request.Parameters;
            string tenantId = request.TenantId;
            var dataTable = new DataTable();
            _logger.LogInformation("MySQL: GetData for SP {spName}", spName);

            try
            {
                if (string.IsNullOrWhiteSpace(spName) ||
                   spName.IndexOfAny(new char[] { ';', '\'', '-', '@', ' ' }) != -1 ||
                   spName.Contains("/*") || spName.Contains("*/"))
                {
                    _logger.LogError("Invalid stored procedure name.");
                    throw new ArgumentException("Invalid stored procedure name", nameof(spName));
                }
                dataTable = await _mySqlCommandService.ExecuteStoredProcedure(request);

                _logger.LogInformation("MySQL: Data retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "MySQL: Error in GetData for SP {spName}", spName);
                throw;
            }

            return dataTable;
        }

        public async Task UpdateTreatmentLoanAccounts(UpdateTreatmentRequestDto request)
        {
            DataTable dt = request.Data;
            string workRequestId = request.WorkRequestId;
            string tenantId = request.TenantId;
            _logger.LogInformation("MySQL: Updating treatment loan accounts for WorkRequest {workRequestId}", workRequestId);
            try
            {
                using (var connection = (MySqlConnection)_tenantConnectionFactory.CreateConnection(tenantId, dbType))
                {
                    using (MySqlCommand truncateCmd = new MySqlCommand("TRUNCATE TABLE treatmentupdateintermediate", connection))
                    {
                        truncateCmd.CommandTimeout = 300;
                        truncateCmd.ExecuteNonQuery();
                    }

                    // Insert DataTable records
                    MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM TreatmentUpdateIntermediate", connection);
                    MySqlCommandBuilder cb = new MySqlCommandBuilder(da);
                    da.Fill(dt);
                    da.Update(dt);

                    // Call stored procedure
                    using (MySqlCommand cmd = new MySqlCommand("TreatmentAllocation", connection))
                    {
                        cmd.CommandTimeout = 300;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@WorkRequestId", workRequestId);
                        cmd.ExecuteNonQuery();
                    }
                }

                _logger.LogInformation("MySQL: Treatment loan accounts updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "MySQL: Error updating treatment loan accounts");
                throw;
            }
        }
        public async Task InsertIntoUnAllocationIntermediateTable(InsertIntoUnAllocationIntermediateTableRequestDto request)
        {
            _logger.LogInformation("DBHelperUtility : InsertIntoIntermediate - Start : DBType - MySql : WorkrequestId = " + request.WorkRequestId + " :: table =" + request.TableName);

            try
            {
                var data = request.Table;
                var userId = request.UserId;
                var table = request.TableName;
                var dt = new DataTable();
                string tenantId = request.TenantId;
                string tempPath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath, _fileSettings.TemporaryPath);


                dt.Columns.Add("Id", typeof(string));
                dt.Columns.Add("WorkRequestId", typeof(string));
                dt.Columns.Add("AccountNo", typeof(string));
                dt.Columns.Add("CreatedBy", typeof(string));
                dt.Columns.Add("CreatedDate", typeof(DateTime));
                dt.Columns.Add("IsError", typeof(bool));
                dt.Columns.Add("Reason", typeof(string));

                data.AsEnumerable()
                    //.Skip(1) // skip headers
                    .Where(dr => Convert.ToString(dr[0]).Length > 0)
                    .ToList()
                    .ForEach(dr => dt.Rows.Add(Guid.NewGuid().ToString().Replace("-", ""), request.WorkRequestId, dr[0], userId, DateTime.Now, false, string.Empty));

                string strFile = tempPath + "/TempFolder/MySQL" + DateTime.Now.Ticks.ToString() + ".csv";

                //Create directory if not exist... Make sure directory has required rights..
                if (!Directory.Exists(tempPath))
                    Directory.CreateDirectory(tempPath);

                //If file does not exist then create it and right data into it..
                if (!File.Exists(strFile))
                {
                    FileStream fs = new FileStream(strFile, FileMode.Create, FileAccess.Write);
                    fs.Close();
                    fs.Dispose();
                }

                //Generate csv file from where data read
                CreateCSVfile(dt, strFile);

                using (var connection = (MySqlConnection)_tenantConnectionFactory.CreateConnection(tenantId, dbType))
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    MySqlBulkLoader bcp1 = new MySqlBulkLoader(connection)
                    {
                        TableName = table,
                        FieldTerminator = ",",
                        LineTerminator = "\r\n",
                        FileName = strFile,
                        NumberOfLinesToSkip = 0
                    };
                    bcp1.Load();

                    try
                    {
                        File.Delete(strFile);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Exception in DBHelperUtility : InsertIntoIntermediate - File Delete : " + ex);
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