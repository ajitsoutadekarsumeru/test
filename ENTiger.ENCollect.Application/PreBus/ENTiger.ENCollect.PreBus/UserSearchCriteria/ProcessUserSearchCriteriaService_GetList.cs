namespace ENTiger.ENCollect.UserSearchCriteriaModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessUserSearchCriteriaService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<IEnumerable<USGetListDto>> GetList(GetListParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetList>().AssignParameters(@params).Fetch();
        }
    }
}