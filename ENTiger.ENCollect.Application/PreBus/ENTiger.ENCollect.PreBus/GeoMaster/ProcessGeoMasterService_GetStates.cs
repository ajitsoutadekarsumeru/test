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
        public async Task<IEnumerable<GetStatesDto>> GetStates(GetStatesParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetStates>().AssignParameters(@params).Fetch();
        }
    }
}