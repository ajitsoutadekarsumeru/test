using System.Data;
using System.Data.OleDb;
using System.IO.Abstractions;
using System.IO.Compression;
using System.Reflection;
using System.Text;
using ENTiger.ENCollect.DomainModels.Utilities.File_Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySqlConnector;
using OfficeOpenXml;
using Sumeru.Flex;

namespace ENTiger.ENCollect.DomainModels.Utilities;

public class CsvExcelUtility : ICsvExcelUtility
{
    private readonly ILogger<CsvExcelUtility> _logger;
    private readonly CsvExcelSettings _settings;
    private readonly IFileSystem _fileSystem;
    private readonly ITenantConnectionFactory _connectionProvider;
    public CsvExcelUtility(ILogger<CsvExcelUtility> logger,
        IOptions<CsvExcelSettings> settings,
        IFileSystem fileSystem,
        ITenantConnectionFactory connectionProvider
        )
    {
        _logger = logger;
        _settings = settings.Value;
        _fileSystem = fileSystem;
        _connectionProvider = connectionProvider;
    }

    public string ConvertDataTableToString(DataTable dt)
    {
        StringBuilder sb = new StringBuilder();

        //Header
        var header = dt.Columns.Cast<DataColumn>().Select(n => n.ColumnName)
            .Aggregate((a, b) => a + _settings.delimiter + b);
        sb.Append(header);
        sb.Append(Environment.NewLine);
        //Rows
        var rows = string.Join(Environment.NewLine,
            dt.Rows.OfType<DataRow>().Select(x => string.Join(",", x.ItemArray)));
        sb.Append(rows);

        return sb.ToString();
    }

    public DataTable GetExcelHeaders(string file, bool hasHeader = true)
    {
        DataTable dt = new DataTable();
        try
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (var pck = new OfficeOpenXml.ExcelPackage())
            {
                using (var stream = _fileSystem.File.OpenRead(file))
                {
                    pck.Load(stream);
                }

                var ws = pck.Workbook.Worksheets.First();
                foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                {
                    dt.Columns.Add(hasHeader
                        ? firstRowCell.Text
                        : string.Format("Column {0}", firstRowCell.Start.Column));
                }

                return dt;
            }
        }
        catch (Exception ex)
        {
            //Console.ForegroundColor = ConsoleColor.Red;
            System.Diagnostics.Trace.WriteLine("Exception in GetExcelHeaders : " + ex);
            throw;
        }
    }

    public void ConvertExcelToCsv(string source, string destination, string WorkRequestId, string Filename, string filterValue, List<string> headers)
    {
        try
        {
            string SourceFolderPath = source;
            string DestinationFolderPath = destination;
            string FileDelimiter = @",";

            string excelFullPath = _fileSystem.Path.Combine(SourceFolderPath, Filename + ".xlsx");

            //Create Excel Connection
            string ConStr;
            string HDR;
            HDR = "YES";
            ConStr = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=" + excelFullPath +
                     ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=1\"";

            OleDbConnection cnn = new OleDbConnection(ConStr);
            cnn.Open();

            DataTable dtSheet = cnn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            //Load the DataTable with Sheet Data

            // Default to SELECT * if no headers are provided
            string columnsToSelect = "*";

            // Check if headers are provided and not empty
            if (headers != null && headers.Count > 0)
            {
                columnsToSelect = string.Join(",", headers.Select(col => $"[{col}]"));
            }

            string whereClause = $"WHERE [{filterValue}] IS NOT NULL AND [{filterValue}] <> ''";
            string query = $"SELECT {columnsToSelect} FROM [Sheet1$] {whereClause}";
            OleDbCommand oconn = new OleDbCommand(query, cnn);
            OleDbDataAdapter adp = new OleDbDataAdapter(oconn);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            //Create CSV File and load data to it from Sheet
            var csvFullPath = _fileSystem.Path.Combine(DestinationFolderPath, Filename + ".csv");
            using (var fs = _fileSystem.File.Create(csvFullPath))
            {
                using (var sw = new StreamWriter(fs))
                {
                    int ColumnCount = dt.Columns.Count;

                    // Write the Header Row to File
                    for (int i = 0; i < ColumnCount; i++)
                    {
                        sw.Write(dt.Columns[i]); // To Do write async
                        if (i < ColumnCount - 1)
                        {
                            sw.Write(FileDelimiter);// To Do write async
                        }
                    }

                    sw.Write(FileDelimiter); // To Do write async
                    sw.Write("Id");
                    sw.Write(FileDelimiter);
                    sw.Write("WorkRequestId");
                    sw.Write(FileDelimiter);
                    sw.Write("CreatedDate");
                    sw.Write(FileDelimiter);
                    sw.Write("LastModifiedDate");
                    sw.Write(sw.NewLine);

                    // Write All Rows to the File
                    foreach (DataRow dr in dt.Rows)
                    {
                        for (int i = 0; i < ColumnCount; i++)
                        {
                            if (!Convert.IsDBNull(dr[i]))
                            {
                                if (i != 2)
                                {
                                    sw.Write(dr[i].ToString());
                                }
                                else
                                {
                                    if (dr[i] != null)
                                    {
                                        sw.Write(Convert.ToDateTime(dr[i]).ToString("yyyy-MM-dd")); // Needs to check 
                                    }
                                }
                            }

                            if (i < ColumnCount - 1)
                            {
                                sw.Write(FileDelimiter);
                            }
                        }

                        sw.Write(FileDelimiter);
                        sw.Write(SequentialGuid.NewGuidString());
                        sw.Write(FileDelimiter);
                        sw.Write(WorkRequestId);
                        sw.Write(FileDelimiter);
                        sw.Write(DateTime.Now);
                        sw.Write(FileDelimiter);
                        sw.Write(DateTime.Now);
                        sw.Write(sw.NewLine);
                    }

                    cnn.Close();
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception in CoreUtilities : ConvertExcelToCsv - " + ex + ex?.Message + ex?.StackTrace +
                             ex?.InnerException + ex?.InnerException?.Message + ex?.InnerException?.StackTrace);
            throw;
        }
        //_logger.LogInformation("CoreUtilities : ConvertExcelToCsv - End");
    }

    public void GenerateCsvFile(GenerateCsvFile model)
    {
        //string MyConnectionString;
        //MyConnectionString = MyConnectionString; //"Data Source = " + model.DataSource + "; Database = " + model.Database + "; User ID = " + model.UserId + "; Password = " + model.Password + ";";
        MySqlConnection connection = (MySqlConnection)_connectionProvider.CreateConnection(model.TenantId, DBTypeEnum.MySQL.Value);
        string? ActionType = model.ActionType;
        DataTable dt = new DataTable();
        using (MySqlCommand cmd = new MySqlCommand(model.StoredProcedure, connection))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@WorkRequestId", model.WorkRequestId);
            if (!string.IsNullOrEmpty(ActionType))
                cmd.Parameters.AddWithValue("@AllocationType", ActionType);
            using (var da = new MySqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
            }
        }

        int ColumnCount = dt.Columns.Count;
        string FileDelimited = @",";

        //Create CSV File and load data to it from Sheet
        var csvFullPath =
            _fileSystem.Path.Combine(model.Destination + ".csv"); //TODO: Rajesh -> There is no folder here??
        using (var fs = _fileSystem.File.Create(csvFullPath))
        {
            using (var sw = new StreamWriter(fs))
            {
                for (int i = 0; i < ColumnCount; i++)
                {
                    sw.Write(dt.Columns[i]);
                    if (i < ColumnCount - 1)
                    {
                        sw.Write(FileDelimited);
                    }
                }

                sw.Write(sw.NewLine);
                foreach (DataRow dr in dt.Rows)
                {
                    for (int i = 0; i < ColumnCount; i++)
                    {
                        if (!Convert.IsDBNull(dr[i]))
                        {
                            sw.Write(dr[i].ToString());
                        }

                        if (i < ColumnCount - 1)
                        {
                            sw.Write(FileDelimited);
                        }
                    }

                    sw.Write(sw.NewLine);
                }
            }
        }
    }
    public void GenerateMSSqlCsvFile(GenerateCsvFile model)
    {
        string MyConnectionString;
        DataTable dt = new DataTable();
        using (SqlConnection sqlcon = new SqlConnection(model.ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand(model.StoredProcedure, sqlcon))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@WorkRequestId", model.WorkRequestId);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
        }

        int ColumnCount = dt.Columns.Count;
        string FileDelimited = @",";
        //Create CSV File and load data to it from Sheet
        var csvFullPath =
            _fileSystem.Path.Combine(model.Destination + ".csv"); //TODO: Rajesh -> There is no folder here?
        using (var fs = _fileSystem.File.Create(csvFullPath))
        {
            using (var sw = new StreamWriter(fs))
            {
                for (int i = 0; i < ColumnCount; i++)
                {
                    sw.Write(dt.Columns[i]);
                    if (i < ColumnCount - 1)
                    {
                        sw.Write(FileDelimited);
                    }
                }

                sw.Write(sw.NewLine);
                foreach (DataRow dr in dt.Rows)
                {
                    for (int i = 0; i < ColumnCount; i++)
                    {
                        if (!Convert.IsDBNull(dr[i]))
                        {
                            sw.Write(dr[i].ToString());
                        }

                        if (i < ColumnCount - 1)
                        {
                            sw.Write(FileDelimited);
                        }
                    }

                    sw.Write(sw.NewLine);
                }
            }
        }
    }

    public DataTable ExcelToDataTable(string file, bool hasHeader = true, string id = "")
    {
        //define a datatable to hold all the values
        DataTable dt = new DataTable();
        try
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (var pck = new OfficeOpenXml.ExcelPackage())
            {
                using (var stream = _fileSystem.File.OpenRead(file))
                {
                    pck.Load(stream);
                }

                var ws = pck.Workbook.Worksheets.First();
                foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                {
                    dt.Columns.Add(hasHeader
                        ? firstRowCell.Text
                        : string.Format("Column {0}", firstRowCell.Start.Column));
                }

                dt.Columns.Add("IsInsert", typeof(bool)); //initial value will be 1
                dt.Columns.Add("IsError", typeof(bool)); //initial value will be 0
                dt.Columns.Add("Remarks", typeof(string)); //initial value will be blank
                dt.Columns.Add("WorkRequestId", typeof(string)); //initial value will be blank

                var startRow = hasHeader ? 2 : 1;
                for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                    DataRow row = dt.Rows.Add();
                    foreach (var cell in wsRow)
                    {
                        row[cell.Start.Column - 1] = cell.Text;
                    }

                    row["IsInsert"] = true;
                    row["IsError"] = false;
                    row["Remarks"] = "";
                    row["WorkRequestId"] = id.Trim();
                }

                return dt;
            }
        }
        catch (Exception ex)
        {
            //Console.ForegroundColor = ConsoleColor.Red;
            System.Diagnostics.Trace.WriteLine("Exception in ExcelToDataTable : " + ex.Message);
            throw;
        }
        //-----------------------------------------
    }

    public void ToCSV(DataTable dtDataTable, string file)
    {
        try
        {
            var csvFullPath = _fileSystem.Path.Combine(file); // TODO: Rajesh -> There is no folder here
            using (var fs = _fileSystem.File.Create(csvFullPath))
            {
                using (var sw = new StreamWriter(fs))
                {
                    //headers
                    for (int i = 0; i < dtDataTable.Columns.Count; i++)
                    {
                        sw.Write(dtDataTable.Columns[i]);
                        if (i < dtDataTable.Columns.Count - 1)
                        {
                            sw.Write(_settings.delimiter);
                        }
                    }

                    sw.Write(sw.NewLine);
                    foreach (DataRow dr in dtDataTable.Rows)
                    {
                        for (int i = 0; i < dtDataTable.Columns.Count; i++)
                        {
                            if (!Convert.IsDBNull(dr[i]))
                            {
                                string value = dr[i].ToString();
                                if (value.Contains(_settings.delimiter))
                                {
                                    value = string.Format("\"{0}\"", value);
                                    sw.Write(value);
                                }
                                else
                                {
                                    sw.Write(dr[i].ToString());
                                }
                            }

                            if (i < dtDataTable.Columns.Count - 1)
                            {
                                sw.Write(_settings.delimiter);
                            }
                        }

                        //sw.Write(sw.NewLine);
                        sw.Write("\n");
                    }

                    sw.Close();
                }
            }
        }
        catch (Exception ex)
        {
            //Console.ForegroundColor = ConsoleColor.Red;
            System.Diagnostics.Trace.WriteLine("Exception in ToCSV : " + ex.Message);
            throw;
        }
    }

    public DataTable CsvToDataTable(string file, char delimiter, string id = "", bool hasHeader = true)
    {
        DataTable dt = new DataTable();
        try
        {
            using (var fs = _fileSystem.File.OpenRead(file))
            {
                using (var sr = new StreamReader(fs))
                {
                    string[] headers = sr.ReadLine().Split(_settings.delimiter);
                    foreach (string header in headers)
                    {
                        dt.Columns.Add(header);
                    }

                    dt.Columns.Add("IsInsert", typeof(bool)); //initial value will be 1
                    dt.Columns.Add("IsError", typeof(bool)); //initial value will be 0
                    dt.Columns.Add("Remarks", typeof(string)); //initial value will be blank
                    dt.Columns.Add("WorkRequestId", typeof(string));

                    while (!sr.EndOfStream)
                    {
                        string[] rows = sr.ReadLine().Split(_settings.delimiter);
                        DataRow dr = dt.NewRow();
                        int count = 0;
                        for (int i = 0; i < headers.Length; i++)
                        {
                            dr[i] = rows[i];
                            count = i;
                        }

                        count++;
                        dr[count] = false;
                        count++;
                        dr[count] = false;
                        count++;
                        dr[count] = "";
                        count++;
                        dr[count] = id;
                        dt.Rows.Add(dr);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //Console.ForegroundColor = ConsoleColor.Red;
            System.Diagnostics.Trace.WriteLine("Exception in CsvToDataTable : " + ex?.Message);
            throw;
        }

        return dt;
    }

    public DataTable ToDataTable<T>(List<T> items, string id = "")
    {
        DataTable dataTable = new DataTable(typeof(T).Name);
        //Get all the properties
        PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (PropertyInfo prop in Props)
        {
            //Setting column names as Property names
            dataTable.Columns.Add(prop.Name);
        }

        dataTable.Columns.Add("IsInsert", typeof(bool)); //initial value will be 0
        dataTable.Columns.Add("IsError", typeof(bool)); //initial value will be 0
        dataTable.Columns.Add("Remarks", typeof(string)); //initial value will be blank
        dataTable.Columns.Add("WorkRequestId", typeof(string));
        foreach (T item in items)
        {
            var values = new object[Props.Length + 4];
            int count = 0;
            for (int i = 0; i < Props.Length; i++)
            {
                //inserting property values to datatable rows
                values[i] = Props[i].GetValue(item, null);
                count = i;
            }

            count++;
            values[count] = false;
            count++;
            values[count] = false;
            count++;
            values[count] = "";
            count++;
            values[count] = id;
            dataTable.Rows.Add(values);
        }

        //put a breakpoint here and check datatable
        return dataTable;
    }

    public string ConvertListToCsvString(List<dynamic> items, bool KeyValuePair = false)
    {
        _logger.LogInformation("ConvertListToCsvString : Start");

        StringBuilder csvContent = new StringBuilder();
        if (items != null && items.Count() > 0)
        {
            // Write the header (assumes the first element of the list contains all possible fields)
            var firstItem = items[0];
            foreach (var property in firstItem)
            {
                if (KeyValuePair)
                {
                    csvContent.Append(property.Key).Append(_settings.delimiter);
                }
                else
                {
                    csvContent.Append(property.Name).Append(_settings.delimiter);
                }
            }

            // Remove the last comma
            csvContent.Length--;

            // New line after the header
            csvContent.AppendLine();

            // Write data rows
            foreach (var item in items)
            {
                foreach (var property in item)
                {
                    // Add each value, wrap in quotes if it contains commas or quotes
                    string value = property.Value?.ToString().Replace("\n", "").Replace("\t", "").Replace("\r", "").Replace(",", " ");
                    //if (value.Contains(","))
                    //{
                    //    value = "\"" + value + "\"";
                    //}
                    csvContent.Append(value).Append(_settings.delimiter);
                }

                // Remove the last comma and add a new line
                csvContent.Length--;
                csvContent.AppendLine();
            }
        }

        _logger.LogInformation("ConvertListToCsvString : End");
        return csvContent.ToString();
    }

    public string ConvertListToCsv<T>(List<T> items)
    {
        _logger.LogInformation("ConvertListToCsvString : Start");

        StringBuilder csvContent = new StringBuilder();

        if (items != null && items.Count > 0)
        {
            var properties = typeof(T).GetProperties();

            // Write header
            foreach (var prop in properties)
            {
                csvContent.Append(prop.Name).Append(_settings.delimiter);
            }

            // Remove last delimiter and add newline
            csvContent.Length--;
            csvContent.AppendLine();

            // Write data rows
            foreach (var item in items)
            {
                foreach (var prop in properties)
                {
                    var value = prop.GetValue(item)?.ToString() ?? string.Empty;
                    value = value.Replace("\n", "")
                                 .Replace("\t", "")
                                 .Replace("\r", "")
                                 .Replace(",", " "); // optional: remove if you want to wrap commas in quotes

                    csvContent.Append(value).Append(_settings.delimiter);
                }

                // Remove last delimiter and add newline
                csvContent.Length--;
                csvContent.AppendLine();
            }
        }

        _logger.LogInformation("ConvertListToCsvString : End");
        return csvContent.ToString();
    }


    public async Task<string> GenerateZipInMemoryAsync(string fileName, string destPath, string csvContent)
    {
        string zipFilePath = _fileSystem.Path.Combine(destPath, $"{fileName}.zip");

        _fileSystem.Directory.CreateDirectory(destPath); // Safe if exists

        using (Stream zipToOpen = new FileStream(zipFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
        using (var archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create, leaveOpen: false))
        {
            var entry = archive.CreateEntry($"{fileName}.csv");

            await using (Stream entryStream = entry.Open())
            await using (var writer = new StreamWriter(entryStream))
            {
                await writer.WriteAsync(csvContent);
            }
        }

        return zipFilePath;
    }

    /// <summary>
    /// Exports data of any type T to a CSV file including a header row, using reflection to extract property names and values.
    /// </summary>
    /// <typeparam name="T">The type of data objects.</typeparam>
    /// <param name="data">A collection of data objects.</param>
    /// <param name="filePath">The CSV file path.</param>
    public async Task ExportDataToCsvGenericAsync<T>(IEnumerable<T> data, string filePath, bool includeHeaders)
    {
        // Open a FileStream with asynchronous I/O enabled.
        await using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true);
        await using var writer = new StreamWriter(stream, Encoding.UTF8);

        // Get public instance properties for type T.
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        if (includeHeaders)
        {
            // Write header row using property names.
            var header = string.Join(",", properties.Select(p => EscapeCsv(p.Name)));
            await writer.WriteLineAsync(header);
        }

        // Write each object's property values as a CSV row.
        foreach (var item in data)
        {
            var values = properties.Select(p =>
            {
                var value = p.GetValue(item);
                return EscapeCsv(value?.ToString() ?? string.Empty);
            });
            var line = string.Join(",", values);
            await writer.WriteLineAsync(line);
        }
    }
    /// <summary>
    /// Escapes a CSV field if it contains commas, quotes, or line breaks.
    /// </summary>
    /// <param name="value">The field text to escape.</param>
    /// <returns>The escaped CSV field.</returns>
    private static string EscapeCsv(string value)
    {
        if (value.Contains(",") || value.Contains("\"") || value.Contains("\r") || value.Contains("\n"))
        {
            // Double any quotes and wrap the field in quotes.
            value = value.Replace("\"", "\"\"");
            return $"\"{value}\"";
        }
        return value;
    }
}