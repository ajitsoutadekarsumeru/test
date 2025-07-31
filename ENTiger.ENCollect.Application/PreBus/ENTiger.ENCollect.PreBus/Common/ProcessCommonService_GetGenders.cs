namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessCommonService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GetGendersDto>> GetGenders(GetGendersParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetGenders>().AssignParameters(@params).Fetch();
        }
    }
}