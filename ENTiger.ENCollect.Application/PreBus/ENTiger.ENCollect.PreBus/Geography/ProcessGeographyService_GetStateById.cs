namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessGeographyService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<GetStateByIdDto> GetStateById(GetStateByIdParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetStateById>().AssignParameters(@params).Fetch();
        }
    }
}