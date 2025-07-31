namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ProcessApplicationUsersService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<GetUsersByIdsDto> >GetUsersByIds(GetUsersByIdsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetUsersByIds>().AssignParameters(@params).Fetch();
        }
    }
}
