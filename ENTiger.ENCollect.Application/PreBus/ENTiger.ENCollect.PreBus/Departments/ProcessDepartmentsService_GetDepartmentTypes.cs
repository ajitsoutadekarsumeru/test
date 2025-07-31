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
        public async Task<IEnumerable<GetDepartmentTypesDto>> GetDepartmentTypes(GetDepartmentTypesParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetDepartmentTypes>().AssignParameters(@params).Fetch();
        }
    }
}