namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessAgencyService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ParentAgenciesListDto>> ParentAgenciesList(ParentAgenciesListParams @params)
        {
            return await _flexHost.GetFlexiQuery<ParentAgenciesList>().AssignParameters(@params).Fetch();
        }
    }
}