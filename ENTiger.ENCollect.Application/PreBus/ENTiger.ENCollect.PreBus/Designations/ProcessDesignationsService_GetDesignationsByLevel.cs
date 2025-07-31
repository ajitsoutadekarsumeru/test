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
        public async Task<IEnumerable<GetDesignationsByLevelDto>> GetDesignationsByLevel(GetDesignationsByLevelParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetDesignationsByLevel>().AssignParameters(@params).Fetch();
        }
    }
}