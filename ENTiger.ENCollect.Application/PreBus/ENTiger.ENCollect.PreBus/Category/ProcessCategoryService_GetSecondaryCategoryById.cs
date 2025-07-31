namespace ENTiger.ENCollect.CategoryModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessCategoryService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<GetSecondaryCategoryByIdDto> GetSecondaryCategoryById(GetSecondaryCategoryByIdParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetSecondaryCategoryById>().AssignParameters(@params).Fetch();
        }
    }
}