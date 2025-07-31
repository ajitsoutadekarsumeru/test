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
        public async Task<IEnumerable<GetIdentificationTypesDto>> GetIdentificationTypes(GetIdentificationTypesParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetIdentificationTypes>().AssignParameters(@params).Fetch();
        }
    }
}