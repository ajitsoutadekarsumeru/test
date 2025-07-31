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
        public async Task<GetCityByIdDto> GetCityById(GetCityByIdParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetCityById>().AssignParameters(@params).Fetch();
        }
    }
}