namespace ENTiger.ENCollect.GeoTagModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessGeoTagService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<IEnumerable<MyTripsDto>> MyTrips(MyTripsParams @params)
        {
            return await _flexHost.GetFlexiQuery<MyTrips>().AssignParameters(@params).Fetch();
        }
    }
}