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
        public virtual async Task<IEnumerable<GetDesignationsWithSchemeDetailsDto>> GetDesignationsWithSchemeDetails(GetDesignationsWithSchemeDetailsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetDesignationsWithSchemeDetails>().AssignParameters(@params).Fetch();
        }
    }
}
