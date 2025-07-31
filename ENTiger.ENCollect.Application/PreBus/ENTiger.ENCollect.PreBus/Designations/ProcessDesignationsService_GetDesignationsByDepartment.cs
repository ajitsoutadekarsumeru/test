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
        public async Task<IEnumerable<GetDesignationsByDepartmentDto>> GetDesignationsByDepartment(GetDesignationsByDepartmentParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetDesignationsByDepartment>().AssignParameters(@params).Fetch();
        }
    }
}