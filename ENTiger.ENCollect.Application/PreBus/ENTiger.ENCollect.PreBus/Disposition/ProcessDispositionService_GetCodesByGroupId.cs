namespace ENTiger.ENCollect.DispositionModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessDispositionService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GetCodesByGroupIdDto>> GetCodesByGroupId(GetCodesByGroupIdParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetCodesByGroupId>().AssignParameters(@params).Fetch();
        }
    }
}