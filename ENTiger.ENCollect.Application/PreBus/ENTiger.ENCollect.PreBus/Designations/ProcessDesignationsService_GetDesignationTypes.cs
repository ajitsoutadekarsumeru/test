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
        public async Task<IEnumerable<GetDesignationTypesDto>> GetDesignationTypes(GetDesignationTypesParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetDesignationTypes>().AssignParameters(@params).Fetch();
        }
    }
}