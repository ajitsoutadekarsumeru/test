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
        public async Task<GetByIdDto> GetById(GetByIdParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetById>().AssignParameters(@params).Fetch();
        }
    }
}