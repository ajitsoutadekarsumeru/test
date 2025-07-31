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
        public async Task<IEnumerable<GetGroupMastersDto>> GetGroupMasters(GetGroupMastersParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetGroupMasters>().AssignParameters(@params).Fetch();
        }
    }
}