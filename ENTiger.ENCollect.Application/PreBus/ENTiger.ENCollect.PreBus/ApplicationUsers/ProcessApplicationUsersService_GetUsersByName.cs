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
        public async Task<IEnumerable<GetUsersByNameDto>> GetUsersByName(GetUsersByNameParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetUsersByName>().AssignParameters(@params).Fetch();
        }
    }
}