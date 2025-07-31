using ENTiger.ENCollect.TreatmentModule;

namespace ENTiger.ENCollect
{
    public interface ITreatmentCommonFunctions
    {
        LoanAccount AssignToAccount(string allocationId, LoanAccount acc, string TreatmentId, string allocationType);

        Task ConstructDatatable(ExecuteFragmentedTreatmentDataPacket packet, List<LoanAccount> accountToSave,
            ExecuteTreatmentDto executeTreatmentDto);

        Task<List<ElasticSearchSimulateLoanAccountDto>> FetchAccountsForAllocationAsync(
            List<ElasticSearchSimulateLoanAccountDto> loanAccounts, string allocationType, string TenantId,
            string TreatmentId, ExecuteTreatmentDto executeTreatmentDto);

        Task<List<string>> FetchAccountsBasedOnQualifyingConditionDigitalCommunicationAsync(
            List<ElasticSearchSimulateLoanAccountDto> loanAccounts, string allocationType, string TenantId,
            string DeliveryStatus, string TreatmentId, string SubTreatmentId, string SubTreatmentIdLatest,
            string newTreatmentHistoryId);

        Task<List<string>> FetchAccountsBasedOnMultipleQualifyingCondition(
            List<ElasticSearchSimulateLoanAccountDto> loanAccounts, string allocationType, string TenantId,
            List<string> DeliveryStatus, string TreatmentId, string SubTreatmentId, string newTreatmentHistoryId,
            ExecuteTreatmentDto executeTreatmentDto);

        Task<List<string>> FetchTreatmentAllocationIds(string allocationType, string product, string bucket, string region,
            string state, string city, string tenantId, List<string> designationList,
            ExecuteTreatmentDto executeTreatmentDto);
    }
}