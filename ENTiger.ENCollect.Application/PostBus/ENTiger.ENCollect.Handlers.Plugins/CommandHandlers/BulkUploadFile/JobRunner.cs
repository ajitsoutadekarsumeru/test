using System.Data;
using ENTiger.ENCollect.FeedbackModule;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace ENTiger.SQLJob
{
    public class SsisPackageExecutor
    {
        protected readonly ILogger<BulkTrailUploadPlugin> _logger;
        private readonly string _connectionString;

        public SsisPackageExecutor(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task ExecuteSsisPackageAsync(string folderName, string projectName, string packageName, Dictionary<string, object> parameters)
        {
            _logger.LogInformation("_connectionString: " + _connectionString);
            _logger.LogInformation("folderName: " + folderName);
            _logger.LogInformation("projectName: " + projectName);
            _logger.LogInformation("packageName: " + packageName);
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                // Create an execution instance
                var cmdCreateExec = new SqlCommand("SSISDB.catalog.create_execution", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmdCreateExec.Parameters.AddRange(new[]
                {
                new SqlParameter("@folder_name", folderName),
                new SqlParameter("@project_name", projectName),
                new SqlParameter("@package_name", packageName),
                new SqlParameter("@execution_id", SqlDbType.BigInt) { Direction = ParameterDirection.Output }
                });
                await cmdCreateExec.ExecuteNonQueryAsync();

                var executionId = (long)cmdCreateExec.Parameters["@execution_id"].Value;
                _logger.LogInformation("executionId: " + executionId);
                // Set the parameters
                foreach (var param in parameters)
                {
                    var cmdSetParam = new SqlCommand("SSISDB.catalog.set_execution_parameter_value", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmdSetParam.Parameters.AddRange(new[]
                    {
                        new SqlParameter("@execution_id", executionId),
                        new SqlParameter("@object_type", 30), // 30 is for package parameters
                        new SqlParameter("@parameter_name", param.Key),
                        new SqlParameter("@parameter_value", param.Value)
                    });
                    await cmdSetParam.ExecuteNonQueryAsync();
                }

                // Start the execution
                var cmdStartExec = new SqlCommand("SSISDB.catalog.start_execution", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmdStartExec.Parameters.AddWithValue("@execution_id", executionId);
                await cmdStartExec.ExecuteNonQueryAsync();
            }
        }
    }
}