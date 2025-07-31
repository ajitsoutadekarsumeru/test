namespace ENTiger.ENCollect.GeoMasterModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessGeoMasterService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GetAreasDto>> GetAreas(GetAreasParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetAreas>().AssignParameters(@params).Fetch();
        }
    }
}