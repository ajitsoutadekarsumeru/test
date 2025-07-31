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
        public async Task<IEnumerable<GetAreaPinCodesByAreaIdDto>> GetAreaPinCodesByAreaId(GetAreaPinCodesByAreaIdParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetAreaPinCodesByAreaId>().AssignParameters(@params).Fetch();
        }
    }
}