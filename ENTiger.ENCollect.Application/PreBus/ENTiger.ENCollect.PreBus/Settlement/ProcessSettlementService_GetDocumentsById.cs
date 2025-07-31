using Sumeru.Flex;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ProcessSettlementService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual async Task<GetDocumentsByIdDto> GetDocumentsById(GetDocumentsByIdParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetDocumentsById>().AssignParameters(@params).Fetch();
        }
    }
}
