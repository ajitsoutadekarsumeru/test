namespace ENTiger.ENCollect.DesignationsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessDesignationsService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GetDesignationsDto>> GetDesignations(GetDesignationsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetDesignations>().AssignParameters(@params).Fetch();
        }
    }
}