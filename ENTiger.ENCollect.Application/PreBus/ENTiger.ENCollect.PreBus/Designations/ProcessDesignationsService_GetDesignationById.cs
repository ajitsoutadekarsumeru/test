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
        public async Task<GetDesignationByIdDto> GetDesignationById(GetDesignationByIdParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetDesignationById>().AssignParameters(@params).Fetch();
        }
    }
}