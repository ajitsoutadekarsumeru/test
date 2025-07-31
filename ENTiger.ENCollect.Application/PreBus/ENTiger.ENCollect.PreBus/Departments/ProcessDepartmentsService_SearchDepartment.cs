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
        public async Task<IEnumerable<SearchDepartmentDto>> SearchDepartment(SearchDepartmentParams @params)
        {
            return await _flexHost.GetFlexiQuery<SearchDepartment>().AssignParameters(@params).Fetch();
        }
    }
}