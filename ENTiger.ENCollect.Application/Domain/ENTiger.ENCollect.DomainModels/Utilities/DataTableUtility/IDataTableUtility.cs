using System.Data;

namespace ENTiger.ENCollect
{
    public interface IDataTableUtility
    {
        bool ValidateUnAllocationHeaders(DataTable dataTable,string unAllocationType);

        void CreateCSVfile(DataTable dtable, string strFilePath);

        DataTable ExcelToDataTable(string filePath, string fileName, string sheetName);
    }
}