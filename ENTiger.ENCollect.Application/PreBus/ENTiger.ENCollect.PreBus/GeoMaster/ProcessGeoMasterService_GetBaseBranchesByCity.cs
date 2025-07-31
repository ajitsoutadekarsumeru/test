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
        public async Task<IEnumerable<GetBaseBranchesByCityDto>> GetBaseBranchesByCity(GetBaseBranchesByCityParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetBaseBranchesByCity>().AssignParameters(@params).Fetch();
        }
    }
}