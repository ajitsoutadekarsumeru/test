using System.Data;

namespace ENTiger.ENCollect.DomainModels.Utilities;

public interface ICsvExcelUtility
{
    string ConvertDataTableToString(DataTable dt);

    DataTable GetExcelHeaders(string file, bool hasHeader = true);

    void ConvertExcelToCsv(string source, string destination, string WorkRequestId, string Filename, string filterColumnName, List<string> headers);

    void GenerateCsvFile(GenerateCsvFile model);

    void GenerateMSSqlCsvFile(GenerateCsvFile model);

    DataTable ExcelToDataTable(string file, bool hasHeader = true, string id = "");

    void ToCSV(DataTable dtDataTable, string file);

    DataTable CsvToDataTable(string file, char delimiter, string id = "", bool hasHeader = true);

    DataTable ToDataTable<T>(List<T> items, string id = "");
    string ConvertListToCsv<T>(List<T> items);
    string ConvertListToCsvString(List<dynamic> items, bool KeyValuePair = false);
    Task<string> GenerateZipInMemoryAsync(string fileName, string destPath, string csvContent);
    Task ExportDataToCsvGenericAsync<T>(IEnumerable<T> data, string filePath, bool includeHeaders);
}