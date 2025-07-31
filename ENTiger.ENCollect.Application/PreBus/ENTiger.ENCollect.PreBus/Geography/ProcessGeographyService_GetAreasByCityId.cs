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
        public async Task<IEnumerable<GetAreasByCityIdDto>> GetAreasByCityId(GetAreasByCityIdParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetAreasByCityId>().AssignParameters(@params).Fetch();
        }
    }
}