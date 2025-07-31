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
        public async Task<SearchPrimaryAllocationbyFiltersDto> SearchPrimaryAllocationbyFilters(SearchPrimaryAllocationbyFiltersParams @params)
        {
            return await _flexHost.GetFlexiQuery<SearchPrimaryAllocationbyFilters>().AssignParameters(@params).Fetch();
        }
    }
}