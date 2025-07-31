namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessAllocationService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<SearchSecondaryAllocationbyFiltersDto> SearchSecondaryAllocationbyFilters(SearchSecondaryAllocationbyFiltersParams @params)
        {
            return await _flexHost.GetFlexiQuery<SearchSecondaryAllocationbyFilters>().AssignParameters(@params).Fetch();
        }
    }
}