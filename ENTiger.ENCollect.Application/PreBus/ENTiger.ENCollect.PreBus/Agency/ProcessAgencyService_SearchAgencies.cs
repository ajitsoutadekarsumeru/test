using Sumeru.Flex;

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
        public async Task<FlexiPagedList<SearchAgenciesDto>> SearchAgencies(SearchAgenciesParams @params)
        {
            return await _flexHost.GetFlexiQuery<SearchAgencies>().AssignParameters(@params).Fetch();
        }
    }
}