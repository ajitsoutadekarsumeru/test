using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;
using System.Data;
using System.Data.OleDb;

namespace ENTiger.ENCollect
{
    public class DataTableUtility : IDataTableUtility
    {
        private readonly ILogger<DataTableUtility> _logger;
        private readonly MicrosoftSettings _microsoftSettings;
        public DataTableUtility(ILogger<DataTableUtility> logger, IOptions<MicrosoftSettings> microsoftSettings)
        {
            _logger = logger;
            _microsoftSettings = microsoftSettings.Value;
        }

        public bool ValidateUnAllocationHeaders(DataTable dataTable, string unAllocationType)
        {
            if (dataTable == null || dataTable.Columns.Count == 0)
            {
                _logger.LogWarning("DataTable is empty or null.");
                return false;
            }

            // Convert DataTable column names to a case-insensitive HashSet
            var dynamicHeaders = new HashSet<string>(
                dataTable.Columns.Cast<DataColumn>().Select(col => col.ColumnName.ToLower()),
                StringComparer.OrdinalIgnoreCase
            );

            _logger.LogInformation($"Validating headers for UnAllocationType: {unAllocationType}");

            // Determine the expected static headers
            var staticHeaders = unAllocationType.Equals("customeridlevel")
                ? new List<string> { "customerid" }
                : new List<string> { "accountno" };

            // Find missing or extra headers
            var mismatchedHeaders = staticHeaders.Except(dynamicHeaders)
                .Union(dynamicHeaders.Except(staticHeaders));

            bool isCorrectFileHeader = !mismatchedHeaders.Any();
            return isCorrectFileHeader;
        }



        public void CreateCSVfile(DataTable dtable, string strFilePath)
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

        public DataTable ExcelToDataTable(string filePath, string fileName, string sheetName)
        {
            _logger.LogInformation("DBHelperUtility : ExcelToDataTable - Start");
            string provider = _microsoftSettings.ExcelProvider;
            DataTable dt = new DataTable();

            // Sanitize file path and file name
            string sanitizedFilePath = Path.GetFullPath(filePath);
            string sanitizedFileName = Path.GetFileName(fileName);
            string fileFullPath = Path.Combine(sanitizedFilePath, sanitizedFileName);
            _logger.LogInformation("DBHelperUtility : ExcelToDataTable - FilePath : " + fileFullPath);

            // Create Excel Connection              
            string conStr = $"Provider={provider};Data Source={fileFullPath};Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"";
            _logger.LogInformation("DBHelperUtility : ExcelToDataTable - Connection : " + conStr);

            // Validate the sheet name to ensure it's a valid Excel sheet name
            if (string.IsNullOrWhiteSpace(sheetName) || sheetName.IndexOfAny(new char[] { '\\', '/', ':', '*', '?', '"', '<', '>', '|' }) != -1)
            {
                throw new ArgumentException("Invalid sheet name", nameof(sheetName));
            }

            using (OleDbConnection cnn = new OleDbConnection(conStr))
            {
                cnn.Open();
                DataTable dtSheet = cnn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (dtSheet == null || dtSheet.Rows.Count == 0)
                {
                    _logger.LogError("DBHelperUtility : ExcelToDataTable - No sheets found in Excel file.");
                    throw new Exception("No sheets found in Excel file.");
                }
                // Sanitize and format the sheet name for the query
                string sanitizedSheetName = sheetName.Replace("'", "''");
                // Load the DataTable with Sheet Data using parameterized query
                string query = "SELECT * FROM [" + sanitizedSheetName + "$]";
                _logger.LogInformation("DBHelperUtility : ExcelToDataTable - Query : " + query);
                using (OleDbCommand oconn = new OleDbCommand(query, cnn))
                {
                    using (OleDbDataAdapter adp = new OleDbDataAdapter(oconn))
                    {
                        adp.Fill(dt);
                    }
                }
            }

            _logger.LogInformation("DBHelperUtility : ExcelToDataTable - End");
            return dt;
        }
    }
}