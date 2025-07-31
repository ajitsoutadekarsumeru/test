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
        public async Task<IEnumerable<GetSecondaryCategoryByParentIdDto>> GetSecondaryCategoryByParentId(GetSecondaryCategoryByParentIdParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetSecondaryCategoryByParentId>().AssignParameters(@params).Fetch();
        }
    }
}