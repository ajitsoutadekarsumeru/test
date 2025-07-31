using System.Data;

namespace ENTiger.ENCollect
{
    public interface IDbUtility
    {
        Task<bool> ExecuteSP(ExecuteSpRequestDto request);

        Task<DataTable> GetData(GetDataRequestDto request);

        Task<int> InsertRecordsIntoIntermediateTable(InsertIntermediateTableRequestDto request);
        Task InsertIntoUnAllocationIntermediateTable(InsertIntoUnAllocationIntermediateTableRequestDto request);

        // “Treatment” methods
        Task UpdateTreatmentLoanAccounts(UpdateTreatmentRequestDto request);
    }
}