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
        public async Task<GetDepartmentByIdDto> GetDepartmentById(GetDepartmentByIdParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetDepartmentById>().AssignParameters(@params).Fetch();
        }
    }
}