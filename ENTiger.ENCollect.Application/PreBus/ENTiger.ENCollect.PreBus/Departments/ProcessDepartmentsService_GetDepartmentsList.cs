namespace ENTiger.ENCollect.DepartmentsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessDepartmentsService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GetDepartmentsListDto>> GetDepartmentsList(GetDepartmentsListParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetDepartmentsList>().AssignParameters(@params).Fetch();
        }
    }
}