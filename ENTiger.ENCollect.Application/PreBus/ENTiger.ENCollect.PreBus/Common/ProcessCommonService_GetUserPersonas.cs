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
        public async Task<IEnumerable<GetUserPersonasDto>> GetUserPersonas(GetUserPersonasParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetUserPersonas>().AssignParameters(@params).Fetch();
        }
    }
}