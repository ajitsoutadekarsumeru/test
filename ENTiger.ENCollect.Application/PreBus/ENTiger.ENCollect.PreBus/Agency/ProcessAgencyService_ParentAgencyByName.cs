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
        public async Task<IEnumerable<ParentAgencyByNameDto>> ParentAgencyByName(ParentAgencyByNameParams @params)
        {
            return await _flexHost.GetFlexiQuery<ParentAgencyByName>().AssignParameters(@params).Fetch();
        }
    }
}