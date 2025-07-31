namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessCommonService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GetBucketListDto>> GetBucketList(GetBucketListParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetBucketList>().AssignParameters(@params).Fetch();
        }
    }
}