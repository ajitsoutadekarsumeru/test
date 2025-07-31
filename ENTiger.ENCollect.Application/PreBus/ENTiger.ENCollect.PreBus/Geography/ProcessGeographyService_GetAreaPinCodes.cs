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
        public async Task<IEnumerable<GetAreaPinCodesDto>> GetAreaPinCodes(GetAreaPinCodesParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetAreaPinCodes>().AssignParameters(@params).Fetch();
        }
    }
}