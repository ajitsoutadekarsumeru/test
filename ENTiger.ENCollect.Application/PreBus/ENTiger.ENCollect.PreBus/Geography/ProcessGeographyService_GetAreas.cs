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
        public async Task<IEnumerable<GetAreaDto>> GetAreas(GetAreasParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetAreas>().AssignParameters(@params).Fetch();
        }
    }
}